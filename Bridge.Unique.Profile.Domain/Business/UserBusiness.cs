using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Web;
using Bridge.Commons.Extension;
using Bridge.Commons.Notification.Mail.Contracts;
using Bridge.Commons.Notification.Sms.Contracts;
using Bridge.Commons.Notification.Sms.Models;
using Bridge.Commons.System.Contracts.Filters;
using Bridge.Commons.System.Enums;
using Bridge.Commons.System.Exceptions;
using Bridge.Commons.System.Models;
using Bridge.Commons.System.Models.Results;
using Bridge.Commons.Validation.Contracts;
using Bridge.Unique.Profile.Communication.Enums;
using Bridge.Unique.Profile.Communication.Resources;
using Bridge.Unique.Profile.Domain.Business.Contracts;
using Bridge.Unique.Profile.Domain.Models;
using Bridge.Unique.Profile.Domain.Models.Providers;
using Bridge.Unique.Profile.Domain.Models.Providers.Apple;
using Bridge.Unique.Profile.Domain.Models.Providers.Facebook;
using Bridge.Unique.Profile.Domain.Models.Providers.Google;
using Bridge.Unique.Profile.Domain.Repositories.Contracts;
using Bridge.Unique.Profile.System.Enums;
using Bridge.Unique.Profile.System.Helpers;
using Bridge.Unique.Profile.System.Models;
using Bridge.Unique.Profile.System.Resources;
using Bridge.Unique.Profile.System.Settings;
using Newtonsoft.Json;

namespace Bridge.Unique.Profile.Domain.Business
{
    public class UserBusiness : BaseBusiness, IUserBusiness
    {
        private readonly IApiClientRepository _apiClientRepository;
        private readonly IApiRepository _apiRepository;
        private readonly IAuthenticationBusiness _authenticationBusiness;
        private readonly IClientRepository _clientRepository;
        private readonly string _emailExternalValidationPath;
        private readonly string _emailValidationPath;
        private readonly HttpClient _httpClient;
        private readonly IMailService _mailService;
        private readonly ISmsService _smsService;
        private readonly IUserRepository _userRepository;

        public UserBusiness(IAuthenticationBusiness authenticationBusiness, IApiClientRepository apiClientRepository,
            IUserRepository userRepository, IMailService mailService, ISmsService smsService,
            IApiRepository apiRepository,
            IBaseValidator<User> userValidator, AppSettings settings, IHttpClientFactory httpClientFactory,
            IClientRepository clientRepository)
            : base(userValidator)
        {
            _apiClientRepository = apiClientRepository;
            _authenticationBusiness = authenticationBusiness;
            _userRepository = userRepository;
            _mailService = mailService;
            _smsService = smsService;
            _apiRepository = apiRepository;
            _emailValidationPath = settings.EmailValidationPath;
            _httpClient = httpClientFactory.CreateClient();
            _emailExternalValidationPath = settings.EmailExternalValidationPath;
            _clientRepository = clientRepository;
        }

        public async Task<bool> AcceptTerms(int id, int apiClientId)
        {
            return await _userRepository.AcceptTerms(new IdentifiableInt(id), apiClientId);
        }

        public async Task<User> Get(int id, int apiClientId)
        {
            return await Execute(_userRepository.Get, new IdentifiableInt(id), apiClientId);
        }

        public async Task<User> Get(int id, string apiCode, int clientId)
        {
            var apiClientId = await _apiClientRepository.GetApiClientId(apiCode, clientId);
            return await Get(id, apiClientId);
        }

        public async Task<User> GetAdmin(int id)
        {
            return await Execute(_userRepository.GetAdmin, new IdentifiableInt(id));
        }

        public async Task<User> GetByApiClientAdmin(int id, string apiClientCode)
        {
            return await Execute(_userRepository.GetByApiClientAdmin, new IdentifiableInt(id), apiClientCode);
        }

        public async Task<User> Login(Identity identity)
        {
            identity.Password = identity.Password.ToHash(HashType);

            var apiClientId = identity.ApiClientId;
            var user = await Execute(_userRepository.Login, identity, 0);
            var apiClient = user.ApiClients.First(f => f.ApiClientId == apiClientId).ApiClient;

            var token = await Execute(_authenticationBusiness.GenerateUserToken,
                new AuthUser
                {
                    Id = user.Id,
                    ApplicationId = identity.ApiClientId,
                    ClientId = apiClient.ClientId,
                    ProfileId = user.ProfileId
                },
                identity.ApplicationToken);
            user.Token = token;

            return user;
        }

        public async Task<User> LoginByApi(Identity identity)
        {
            identity.Password = identity.Password.ToHash(HashType);

            var apiClient = await _apiClientRepository.GetByUser(identity.ApiCode, identity.Email);

            var user = await Execute(_userRepository.Login, identity, apiClient.ClientId);

            var token = await Execute(_authenticationBusiness.GenerateUserToken,
                new AuthUser
                {
                    Id = user.Id,
                    ApplicationId = apiClient.Id, //identity.ApiClientId,
                    ClientId = apiClient
                        .ClientId //user.ApiClients.First(f => f.ApiClientId == user.ApiClients.First().ApiClientId).ApiClient.ClientId
                },
                identity.ApplicationToken);
            user.Token = token;

            return user;
        }

        public async Task<User> LoginSocial(IdentitySocial identity, ApiClient apiClient)
        {
            var userSocial = new User
            {
                UserName = identity.UserName,
                Email = identity.Email,
                PhoneNumber = identity.PhoneNumber,
                Name = identity.Name,
                HasUserName = identity.HasUserName,
                ProfileId = identity.ProfileId,
                IsEmailValidated = true,
                IsPhoneNumberValidated = true,
                Active = true,
                Gender = identity.Gender,
                BirthDate = identity.BirthDate,
                Password = identity.Password.ToHash(HashType),
                Provider = identity.Provider.ProviderAuth,
                ProviderUserId = identity.Provider.ProviderUserId
            };

            await ValidateProvider(userSocial, identity.Provider);

            var apiClientId = userSocial.ApiClientId = identity.ApiClientId;

            var user = await Execute(_userRepository.Login, identity);

            if (user != null)
                userSocial.Password = user.Password;

            user ??= await SaveSocial(userSocial, apiClientId);

            var token = await Execute(_authenticationBusiness.GenerateUserToken,
                new AuthUser
                {
                    Id = user.Id, ApplicationId = identity.ApiClientId,
                    ClientId = user.ApiClients.First(f => f.ApiClientId == apiClientId).ApiClient.ClientId
                },
                identity.ApplicationToken);
            user.Token = token;

            return user;
        }

        public AuthUser Authenticate(Token token)
        {
            return _authenticationBusiness.Authenticate(token.AccessToken, token.ApplicationToken);
        }

        public async Task<bool> Logout(Token token)
        {
            return await Execute(_authenticationBusiness.Logout, token);
        }

        public async Task<bool> UpdatePassword(UpdatePassword updatePassword)
        {
            updatePassword.NewPassword = updatePassword.NewPassword.ToHash(HashType);
            updatePassword.OldPassword = updatePassword.OldPassword.ToHash(HashType);

            return await Execute(_userRepository.UpdatePassword, updatePassword);
        }

        public async Task<bool> RecoverPassword(Identity identity, ApiClient apiClient)
        {
            if (apiClient != null)
                identity.ApiClientId = apiClient.Id;
            else
                apiClient = await _apiClientRepository.GetByUser(identity.ApiCode, identity.Email);

            var password = UserHelper.GenerateNewPassword(8);

            var user = await Execute(_userRepository.SetNewPassword, identity, password.ToHash(HashType),
                apiClient.ClientId);
            apiClient ??= user.ApiClients.First().ApiClient;


            var mailMessage = new MailMessage
            {
                To = { new MailAddress(user.Email) },
                From = new MailAddress(Email.NoReplyEmailAddress),
                Sender = new MailAddress(Email.NoReplyEmailAddress),
                Subject = $"{apiClient.Sender} - Nova senha",
                Body =
                    $"<p>Sua nova senha para acessar o sistema {apiClient.Sender} é: {password}</p>",
                IsBodyHtml = true
            };

            //Não trocar para o método assíncrono, pois ele não tem um awaiter e não termina antes que o
            //escopo da requisição termine, ou seja, não envia o e-mail
            _mailService.Send(mailMessage);

            return true;
        }

        public async Task<Token> RefreshToken(Token token)
        {
            var result = await Execute(_authenticationBusiness.RefreshToken, token.RefreshToken,
                token.ApplicationToken);

            return new Token
            {
                AccessToken = result
            };
        }

        public async Task<PaginatedList<User>> ListByApplication(int apiClientId, Pagination pagination)
        {
            return await Execute(_userRepository.ListByApplication, new IdentifiableInt(apiClientId), pagination);
        }

        public async Task<PaginatedList<User>> ListByApplicationProfile(IFilterPagination request)
        {
            return await Execute(_userRepository.ListByApplicationProfile, request);
        }

        public async Task<PaginatedList<User>> ListById(int[] ids, Pagination pagination)
        {
            return await Execute(_userRepository.ListById, ids, pagination);
        }


        public async Task<PaginatedList<Address>> ListAddresses(int userId)
        {
            var pagination = new Pagination
            {
                Page = 1,
                Order = ESortType.ASCENDING,
                PageSize = 10,
                SortField = ""
            };

            return await Execute(_userRepository.ListAddresses, new IdentifiableInt(userId), pagination);
        }

        public async Task<User> Save(User user, string apiCode, int clientId, ApiClient apiClientContext)
        {
            if (apiCode != "PDE") //Não valida se for cadastro de entregador
                if (await ExistsInAnotherPortal(user.Email))
                    throw new BusinessException((int)EError.USER_ALREADY_EXISTS_IN_PORTAL,
                        Errors.UserAlreadyExistsInPortal);

            var apiClientId = await _apiClientRepository.GetApiClientId(apiCode, clientId);
            return await Save(user, apiClientId, apiClientContext);
        }

        public async Task<User> Save(User user, int apiClientId, ApiClient apiClientContext)
        {
            user.Password = user.Password?.ToHash(HashType);
            var token = TokenHelper.GenerateBupToken();
            var tokenAccessApproval = TokenHelper.GenerateBupToken();
            user.EmailValidationToken = token.ToHash(HashType);
            user.AccessValidationToken = tokenAccessApproval.ToHash(HashType);
            var code = UserHelper.GenerateSmsValidationCode().ToString();
            user.SmsValidationCode = code.ToHash(HashType);
            user.ApiClientId = apiClientId;

            if (!user.HasUserName)
                user.UserName = UserHelper.GenerateNewUserName();

            var (userExistsInAnotherApplication, userResult, apiClient) =
                await ExecuteValidate(_userRepository.Save, user);

            if (apiClientContext == null)
                apiClientContext = apiClient;

            //Envia os códigos/links de confirmação de dados
            if (userResult.EmailChanging || apiClientContext.NeedsExternalApproval)
                await ResendEmailValidationLink(userResult.Email, apiClientContext, false, token, tokenAccessApproval,
                    user,
                    userResult.EmailChanging, !userResult.IsExternalApproved);
            //Envia código de validação do telefone
            if (!string.IsNullOrWhiteSpace(user.PhoneNumber) && userResult.PhoneChanging)
                await ResendTelephoneValidationCode(userResult.Id, userResult.PhoneNumber, apiClientId, apiClient,
                    false, code);

            if (userExistsInAnotherApplication && apiClientContext.NeedsExternalApproval)
                throw new BusinessException(
                    (int)EError.USER_EXISTS_UPDATED_WITH_NEW_APPLICATION_BUT_NEEDS_APPROVAL,
                    Errors.UserAlreadyExistsAndWasAssociatedWithNewApplicationButNeedsApproval);

            if (userExistsInAnotherApplication)
                throw new BusinessException(
                    (int)EError.USER_EXISTS_UPDATED_WITH_NEW_APPLICATION,
                    Errors.UserAlreadyExistsAndWasAssociatedWithNewApplication);

            return userResult;
        }

        public async Task<bool> AdminActiveUserUpdate(UserAction user)
        {
            return await Execute(_userRepository.AdminActiveUserUpdate, user);
        }

        public async Task<bool> AdminUserApproval(UserAction user)
        {
            return await Execute(_userRepository.AdminUserApproval, user);
        }

        public async Task<User> SaveSocial(User user, int apiClientId)
        {
            if (!user.HasUserName)
                user.UserName = UserHelper.GenerateNewUserName();
            user.ApiClientId = apiClientId;

            var (_, userResult, _) =
                await ExecuteValidate(_userRepository.Save, user);

            return userResult;
        }

        public async Task<bool> Delete(int id, int apiClientId)
        {
            return await Execute(_userRepository.Delete, new IdentifiableInt(id), apiClientId);
        }

        public async Task<bool> Delete(int id, string apiCode, int clientId)
        {
            var apiClientId = await _apiClientRepository.GetApiClientId(apiCode, clientId);
            return await Delete(id, apiClientId);
        }

        public async Task<bool> Delete(string email, string clientCode, List<string> apiCodes)
        {
            var client = await _clientRepository.GetByCode(clientCode);

            foreach (var x in apiCodes)
            {
                var apiClientId = await _apiClientRepository.GetApiClientId(x, client.Id);
                await Execute(_userRepository.DeleteByEmail, email, apiClientId);
            }

            return true;
        }

        public async Task<bool> ValidateEmail(string token)
        {
            await Execute(_userRepository.ValidateEmail, token.ToHash(HashType));

            return true;
            //throw new BusinessException((int) EError.EMAIL_NOT_VALIDATED, Errors.EmailNotValidated);
        }

        public async Task<bool> ValidateExternalAccess(string token)
        {
            var userApiClient = await _userRepository.ValidateExternalAccess(token.ToHash(HashType));

            if (userApiClient == null)
                throw new BusinessException((int)EError.ACCESS_NOT_APPROVED, Errors.AccessNotApproved);

            SendEmailExternalValidationToUser(userApiClient);

            return true;
        }

        public async Task<bool> ValidateTelephone(string phoneNumber, string code)
        {
            if (await Execute(_userRepository.ValidateTelephone, phoneNumber, code.ToHash(HashType)))
                return true;

            throw new BusinessException((int)EError.PHONE_NOT_VALIDATED, Errors.PhoneNotValidated);
        }

        public async Task ResendEmailValidationLink(string email, ApiClient apiClient,
            bool resending = true, string token = null, string accessToken = null, User user = null,
            bool isEmailChanging = true, bool resendExternal = false)
        {
            if (resending)
            {
                token = TokenHelper.GenerateBupToken();
                accessToken = TokenHelper.GenerateBupToken();

                if (apiClient.NeedsExternalApproval)
                    (_, resendExternal) =
                        await _userRepository.SetNewExternalAccessValidationToken(email, apiClient.Id,
                            accessToken.ToHash(HashType));

                user = await Execute(_userRepository.SetNewValidationEmailToken, email, token.ToHash(HashType));
            }

            if (apiClient.NeedsExternalApproval && resendExternal)
                ResendEmailExternalValidationLink(user, apiClient, accessToken);

            if (isEmailChanging || resending)
            {
                var messageBody =
                    $"<p>O link abaixo confirma o seu cadastro no {apiClient.Sender}.</p><br><br><p>Para confirmar <a href='{string.Format(_emailValidationPath, HttpUtility.UrlEncode(token))}'>clique aqui</a></p>";

                var subject = $"{apiClient.Sender} - Confirmação de e-mail";

                var mailMessage = new MailMessage
                {
                    To = { email },
                    From = new MailAddress(Email.NoReplyEmailAddress),
                    Sender = new MailAddress(Email.NoReplyEmailAddress),
                    Subject = subject,
                    IsBodyHtml = true,
                    Body = messageBody
                };

                //Não trocar para o método assíncrono, pois ele não tem um awaiter e não termina antes que o
                //escopo da requisição termine, ou seja, não envia o e-mail
                _mailService.Send(mailMessage);
            }
        }

        public async Task<bool> ResendTelephoneValidationCode(int id, string phoneNumber, int apiClientId,
            ApiClient apiClient,
            bool resending = true,
            string code = null)
        {
            if (resending)
            {
                code = UserHelper.GenerateSmsValidationCode().ToString();

                if (apiClientId == 0)
                    apiClientId = apiClient.Id;

                (_, apiClient) = await _userRepository.SetNewValidationTelephoneCode(apiClientId, id, phoneNumber,
                    code.ToHash(HashType));
            }

            var smsMessage = new SmsMessage
            {
                Sender = apiClient.Sender,
                Message = $"{code} Este é o seu código de validação.",
                PhoneNumber = phoneNumber
            };

            await _smsService.SendAsync(smsMessage);

            return true;
        }

        public async Task<bool> ExistsInAnotherPortal(string email)
        {
            return await _userRepository.ExistsInAnotherPortal(email);
        }

        private async Task<bool> ValidateProvider(User user, Provider provider)
        {
            var isValid = false;

            switch (provider.ProviderAuth)
            {
                case EAuthProvider.FACEBOOK:
                    isValid = await ValidateProviderTokenFacebook(provider);
                    break;
                case EAuthProvider.GOOGLE:
                    isValid = await ValidateProviderTokenGoogle(provider);
                    break;
                case EAuthProvider.APPLE:
                    isValid = await ValidateProviderTokenApple(provider);
                    break;
                case EAuthProvider.BUP:
                case EAuthProvider.TWITTER:
                case EAuthProvider.MICROSOFT:
                default:
                    return false;
            }

            if (isValid)
            {
//                if(string.IsNullOrEmpty(user.PhoneNumber))
//                    throw new BusinessException(
//                        (int) EError.SOCIAL_ACCOUNT_WITHOUT_PHONE, Errors.SocialAccountWithoutPhone);

                if (string.IsNullOrEmpty(user.Email))
                    throw new BusinessException(
                        (int)EError.SOCIAL_ACCOUNT_WITHOUT_EMAIL, Errors.SocialAccountWithoutEmail);
            }
            else
            {
                throw new BusinessException(
                    (int)EError.SOCIAL_UNAUTHORIZED, Errors.InvalidAccessTokenProvider);
            }

            return true;
        }

        private async Task<bool> ValidateProviderTokenFacebook(Provider provider)
        {
            var clientId = "my-facebook-client-id";
            var clientSecret = "my-facebook-client-secret";

            var urlTokenApp = "https://graph.facebook.com/oauth/access_token?client_id=" + clientId +
                              "&client_secret=" + clientSecret + "&grant_type=client_credentials";
            var response = await _httpClient.GetAsync(urlTokenApp);
            if (!response.IsSuccessStatusCode)
                return false;

            //Console.WriteLine($"Status Code do Response : {(int)response.StatusCode} {response.ReasonPhrase}");
            var responseBodyAsText = await response.Content.ReadAsStringAsync();
            var appToken = JsonConvert.DeserializeObject<AccessTokenFacebook>(responseBodyAsText).access_token;

            var urlDebugToken = "https://graph.facebook.com/debug_token?input_token=" +
                                provider.ProviderToken + "&access_token=" + appToken;
            response = await _httpClient.GetAsync(urlDebugToken);
            if (!response.IsSuccessStatusCode)
                return false;

            responseBodyAsText = await response.Content.ReadAsStringAsync();
            var debugObject = JsonConvert.DeserializeObject<DebugTokenFacebook>(responseBodyAsText);

            if (debugObject.data.user_id != provider.ProviderUserId)
                throw new BusinessException(
                    (int)EError.USER_SOCIAL_INVALID, Errors.InvalidUserProvider);

            return debugObject.data.is_valid;
        }

        private async Task<bool> ValidateProviderTokenGoogle(Provider provider)
        {
            //var urlTokenInfo = "https://www.googleapis.com/oauth2/v3/tokeninfo?access_token=" + provider.ProviderToken;
            var urlTokenInfo = "https://oauth2.googleapis.com/tokeninfo?id_token=" + provider.ProviderToken;
            var response = await _httpClient.GetAsync(urlTokenInfo);
            if (!response.IsSuccessStatusCode)
                return false;

            var responseBodyAsText = await response.Content.ReadAsStringAsync();
            var debugObject = JsonConvert.DeserializeObject<TokenInfoGoogle>(responseBodyAsText);

            if (debugObject.sub != provider.ProviderUserId)
                throw new BusinessException(
                    (int)EError.USER_SOCIAL_INVALID, Errors.InvalidUserProvider);

            return debugObject.email_verified.ToLower() == "true";
        }

        private async Task<bool> ValidateProviderTokenApple(Provider provider)
        {
            var clientId = "my-apple-client-id";

            var clientSecret = provider.ProviderClientSecret;
            var urlTokenInfo = "https://appleid.apple.com/auth/token";

            var dict = new Dictionary<string, string>();
            dict.Add("client_id", clientId);
            dict.Add("client_secret", clientSecret);
            dict.Add("code", provider.ProviderToken);
            dict.Add("grant_type", "authorization_code");

            var req = new HttpRequestMessage(HttpMethod.Post, urlTokenInfo)
                { Content = new FormUrlEncodedContent(dict) };
            var response = await _httpClient.SendAsync(req);
            if (!response.IsSuccessStatusCode)
                return false;

            var responseBodyAsText = await response.Content.ReadAsStringAsync();
            var debugObject = JsonConvert.DeserializeObject<TokenInfoApple>(responseBodyAsText);

            return true;
        }

        private void ResendEmailExternalValidationLink(User user, ApiClient apiClient, string token = null)
        {
            var gender = user.Gender == EGender.MALE ? "Masculino" : "Feminino";
            var birthDate = user.BirthDate.HasValue ? user.BirthDate.Value.ToString("dd/MM/yyyy") : "Não informado";

            var messageBody =
                $"<p>Confirma o cadastro do usuário abaixo no {apiClient.Sender}?</p>" +
                "<br><br>" +
                $"<p>Usuário: {user.Name}</p>" +
                $"<p>Email: {user.Email}</p>" +
                $"<p>Cpf: {user.Document}</p>" +
                $"<p>Data de nascimento: {birthDate}</p>" +
                $"<p>Sexo: {gender}</p>" +
                "<br><br>" +
                $"<p>Para confirmar <a href='{string.Format(_emailExternalValidationPath, HttpUtility.UrlEncode(token))}'>clique aqui</a></p>";

            var subject = $"{apiClient.Sender} - Aprovação de entregador";

            var mailMessage = new MailMessage
            {
                To = { apiClient.ExternalApproversEmail },
                From = new MailAddress(Email.NoReplyEmailAddress),
                Sender = new MailAddress(Email.NoReplyEmailAddress),
                Subject = subject,
                IsBodyHtml = true,
                Body = messageBody
            };

            _mailService.Send(mailMessage);
        }

        private void SendEmailExternalValidationToUser(UserApiClient userApiClient)
        {
            var messageBody =
                $"<p>Olá {userApiClient.User.Name},</p>" +
                "<br><br>" +
                "<p>Seu cadastro para ser um de nossos entregadores foi aprovado!</p>" +
                "<br><br>" +
                "<p>Basta acessar o aplicativo e começar a usar!</p>";

            var subject = $"{userApiClient.ApiClient.Sender} - Aprovação de cadastro";

            var mailMessage = new MailMessage
            {
                To = { userApiClient.User.Email },
                From = new MailAddress(Email.NoReplyEmailAddress),
                Sender = new MailAddress(Email.NoReplyEmailAddress),
                Subject = subject,
                IsBodyHtml = true,
                Body = messageBody
            };

            _mailService.Send(mailMessage);
        }
    }
}