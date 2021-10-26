using System;
using System.Linq;
using System.Threading.Tasks;
using Bridge.Commons.System.Contracts;
using Bridge.Commons.System.Contracts.Filters;
using Bridge.Commons.System.EntityFramework.Bases.Repositories;
using Bridge.Commons.System.EntityFramework.Extensions;
using Bridge.Commons.System.Enums;
using Bridge.Commons.System.Exceptions;
using Bridge.Commons.System.Models;
using Bridge.Commons.System.Models.Results;
using Bridge.Unique.Profile.Communication.Enums;
using Bridge.Unique.Profile.Communication.Models.In.Filters;
using Bridge.Unique.Profile.Communication.Resources;
using Bridge.Unique.Profile.Domain.Contexts.Contracts;
using Bridge.Unique.Profile.Domain.Models;
using Bridge.Unique.Profile.Domain.Repositories.Contracts;
using Bridge.Unique.Profile.Postgres.Context;
using Bridge.Unique.Profile.Postgres.Entities;
using Bridge.Unique.Profile.System.Enums;
using Microsoft.EntityFrameworkCore;

namespace Bridge.Unique.Profile.Postgres.Repositories
{
    public class UserRepository : BaseRepository<BupReadContext, BupWriteContext, UserEntity, IIdentifiable<int>, int>,
        IUserRepository
    {
        public UserRepository(IBupReadContext bupReadContext, IBupWriteContext bupWriteContext)
        {
            Init((BupReadContext)bupReadContext, (BupWriteContext)bupWriteContext);
        }

        public async Task<bool> AcceptTerms(IIdentifiable<int> identifiable, int apiClientId)
        {
            ValidateIdentifiable(identifiable);
            var id = identifiable.Id;

            var entity = await GetWritable<UserApiClientEntity>()
                .SingleOrDefaultAsync(x => x.UserId == id && x.ApiClientId == apiClientId);

            ValidateEntity(entity);

            entity.TermsAcceptanceDate = DateTime.UtcNow;

            return await SaveChangesAsync() > 0;
        }

        public async Task<bool> Delete(IIdentifiable<int> identifiable, int apiClientId)
        {
            ValidateIdentifiable(identifiable);
            var id = identifiable.Id;

            var entity = await (
                from u in GetQueryable()
                join ua in GetQueryable<UserApiClientEntity>() on u.Id equals ua.UserId
                where u.Id == id && ua.ApiClientId == apiClientId
                select ua
            ).Include(x => x.User).SingleOrDefaultAsync();

            GetWritable<UserApiClientEntity>().Remove(entity);

            return await SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteByEmail(string email, int apiClientId)
        {
            var entity = await (
                from u in GetQueryable()
                join ua in GetQueryable<UserApiClientEntity>() on u.Id equals ua.UserId
                where u.Email == email && ua.ApiClientId == apiClientId
                select ua
            ).Include(x => x.User).SingleOrDefaultAsync();

            if (entity == null)
                return true;

            GetWritable<UserApiClientEntity>().Remove(entity);

            return await SaveChangesAsync() > 0;
        }

        public async Task<User> Get(IIdentifiable<int> identifiable, int apiClientId)
        {
            ValidateIdentifiable(identifiable);
            var id = identifiable.Id;

            var entity = await (
                    from u in GetQueryable()
                    join ua in GetQueryable<UserApiClientEntity>() on u.Id equals ua.UserId
                    where u.Id == id && ua.ApiClientId == apiClientId
                    select u
                ).Include(i => i.ApiClients.Where(ww => ww.ApiClientId == apiClientId))
                .SingleOrDefaultAsync();

            return entity?.MapTo();
        }

        public async Task<User> GetAdmin(IIdentifiable<int> identifiable)
        {
            ValidateIdentifiable(identifiable);
            var id = identifiable.Id;

            var entity = await GetQueryable()
                .Where(w => w.Id == id)
                .Include(i => i.ApiClients)
                .FirstOrDefaultAsync();

            return entity?.MapTo();
        }

        public async Task<User> GetByApiClientAdmin(IIdentifiable<int> identifiable, string apiClientCode)
        {
            ValidateIdentifiable(identifiable);
            var id = identifiable.Id;

            var entity = await (
                from u in GetQueryable()
                join ua in GetQueryable<UserApiClientEntity>() on u.Id equals ua.UserId
                join ac in GetQueryable<ApiClientEntity>() on ua.ApiClientId equals ac.Id
                where ua.UserId == id && ac.Code == apiClientCode
                select new { u, ua }
            ).FirstOrDefaultAsync();

            var userResult = entity?.u.MapTo();

            return userResult;
        }

        public async Task<PaginatedList<User>> ListByApplication(IIdentifiable<int> identifiable, Pagination pagination)
        {
            ValidateIdentifiable(identifiable);

            var query =
                from u in GetQueryable()
                join ua in GetQueryable<UserApiClientEntity>() on u.Id equals ua.UserId
                where ua.ApiClientId == identifiable.Id
                select u;

            return await query.GetPaginatedListAsync<UserEntity, User>(pagination);
        }

        public async Task<PaginatedList<User>> ListByApplicationProfile(IFilterPagination request)
        {
            var pagination = (FilterPagination<UserFilterIn>)request;
            var filter = pagination.Filters;

            var query =
                from u in GetQueryable().Include(i => i.ApiClients)
                join ua in GetQueryable<UserApiClientEntity>() on u.Id equals ua.UserId
                join ap in GetQueryable<ApiClientEntity>() on ua.ApiClientId equals ap.Id
                join a in GetQueryable<ApiEntity>() on ap.ApiId equals a.Id
                where (a.Code == filter.ApiCode && ap.ClientId == filter.ClientId ||
                       ua.ApiClientId == filter.ApiClientId) &&
                      (ua.ProfileId == filter.ProfileId || filter.ProfileId == 0) &&
                      u.Id != filter.UserId
                select u;

            if (filter != null)
            {
                if (!string.IsNullOrWhiteSpace(filter.Name))
                    query = query.Where(x => x.Name.ToLower().StartsWith(filter.Name.ToLower()));

                if (!string.IsNullOrWhiteSpace(filter.Document))
                    query = query.Where(x => x.Document.StartsWith(filter.Document));

                if (!string.IsNullOrWhiteSpace(filter.Email))
                    query = query.Where(x => x.Email.ToLower().StartsWith(filter.Email.ToLower()));

                if (!string.IsNullOrWhiteSpace(filter.PhoneNumber))
                    query = query.Where(x => x.PhoneNumber == filter.PhoneNumber);
            }

            if (!string.IsNullOrWhiteSpace(pagination.SortField))
                query = pagination.Order == ESortType.ASCENDING
                    ? query.OrderBy(pagination.SortField)
                    : query.OrderByDescending(pagination.SortField);
            else
                query = query.OrderBy(o => o.Id);

            return await query.GetPaginatedListAsync<UserEntity, User>(pagination);
        }

        public async Task<PaginatedList<User>> ListById(int[] ids, Pagination pagination)
        {
            var query = GetQueryable().Where(u => ids.Contains(u.Id))
                .Include(u => u.Addresses.Where(ww => (ww.Address.AddressTypes & (int)EAddressType.DEFAULT) > 0))
                .ThenInclude(ua => ua.Address);

            return await query.GetPaginatedListAsync<UserEntity, User>(pagination);
        }

        public async Task<PaginatedList<Address>> ListAddresses(IIdentifiable<int> identifiable, Pagination pagination)
        {
            ValidateIdentifiable(identifiable);

            var query =
                from a in GetQueryable<AddressEntity>()
                join ua in GetQueryable<UserAddressEntity>() on a.Id equals ua.AddressId
                orderby a.Neighborhood, a.CreateDate descending
                where ua.UserId == identifiable.Id
                select a;

            return await query.GetPaginatedListAsync<AddressEntity, Address>(pagination);
        }

        public async Task<User> Login(Identity request, int clientId = 0)
        {
            var email = request.Email;
            var userName = request.UserName;
            var apiCode = request.ApiCode;
            var apiClientId = request.ApiClientId;
            var password = request.Password;

            var entity = await (
                    from u in GetWritable()
                    join ua in GetWritable<UserApiClientEntity>() on u.Id equals ua.UserId
                    join ac in GetWritable<ApiClientEntity>() on ua.ApiClientId equals ac.Id
                    join a in GetWritable<ApiEntity>() on ac.ApiId equals a.Id
                    where (
                              u.Email.Equals(email) && u.Email != null
                              || u.UserName.Equals(userName) && u.UserName != null
                          ) && u.Password.Equals(password) && u.Password != null && u.Active.Value &&
                          (ua.ApiClientId == apiClientId && apiClientId != 0 ||
                           a.Code == apiCode && apiCode != null && ac.ClientId == clientId && clientId != 0) &&
                          ua.Active.Value
                    select new { User = u, ua, ac }
                )
                .SingleOrDefaultAsync();

            if (entity == null)
                throw new RepositoryException((int)EError.USER_OR_PASSWORD_INCORRECT, Errors.UserOrPasswordIncorrect);
            if (!entity.User.IsEmailValidated)
                throw new RepositoryException((int)EError.EMAIL_NOT_VALIDATED, Errors.EmailNotValidated);
            if (entity.ua.IsExternalApproved != null && !entity.ua.IsExternalApproved.Value)
                throw new RepositoryException((int)EError.ACCESS_NOT_APPROVED, Errors.AccessNotApproved);

            var result = entity.User.MapTo();
            result.ApiClients.Add(entity.ua.MapTo());

            return result;
        }

        public async Task<User> Login(IdentitySocial request)
        {
            var email = request.Email;
            var userName = request.UserName;
            var apiClientId = request.ApiClientId;
            var providerId = (int)request.Provider.ProviderAuth;
            var providerUserId = request.Provider.ProviderUserId;

            var entity = await (from u in GetWritable()
                    join ua in GetWritable<UserApiClientEntity>().Include(i => i.ApiClient) on u.Id equals ua.UserId
                    join ul in GetWritable<UserLoginEntity>()
                        on u.Id equals ul.UserId
                    where (
                              u.Email.Equals(email) && u.Email != null
                              || u.UserName.Equals(userName) && u.UserName != null
                              || providerId == (int)EAuthProvider.APPLE
                          )
                          && u.Active.Value
                          && ua.ApiClientId == apiClientId
                          && ua.Active.Value
                          && ul.Provider == providerId
                          && ul.ProviderUserId == providerUserId
                    select new { u, ua }
                )
                .SingleOrDefaultAsync();

            var result = entity?.u?.MapTo();

            return result;
        }

        public async Task<(bool, User, ApiClient)> Save(User request)
        {
            var email = request.Email;
            var userName = request.UserName;
            var apiClientId = request.ApiClientId;
            var providerId = (byte)request.Provider;
            var id = request.Id;
            var emailChanging = true;
            var phoneChanging = true;
            var userExistsInAnotherApplication = false;

            var apiClientEntity = await
            (
                from ac in GetWritable<ApiClientEntity>().Include(i => i.Client)
                where ac.Id == apiClientId
                select ac
            ).SingleOrDefaultAsync();

            request.IsExternalApproved = !apiClientEntity.NeedsExternalApproval;
            if (request.IsExternalApproved)
                request.AccessValidationToken = null;

            var entity = await (from u in GetWritable()
                    where u.Email.Equals(email) && u.Email != null
                          || u.UserName.Equals(userName) && u.UserName != null
                          || u.Id == id
                    select u)
                .Include(i => i.ApiClients)
                .Include(i => i.UserLogins)
                .SingleOrDefaultAsync();

            if (entity != null)
            {
                var hasBupProvider =
                    entity.UserLogins.FirstOrDefault(f => f.Provider == (byte)EAuthProvider.BUP) != null;

                var inProvider =
                (
                    from ua in entity.UserLogins
                    where ua.Provider == providerId &&
                          ua.ProviderUserId == request.ProviderUserId
                    select ua
                ).FirstOrDefault();

                if (inProvider == null)
                    entity.UserLogins.Add(new UserLoginEntity
                    {
                        Provider = (byte)request.Provider,
                        UserId = entity.Id,
                        ProviderUserId = request.ProviderUserId
                    });

                var inApplication =
                (
                    from ua in entity.ApiClients
                    where ua.ApiClientId == apiClientId
                    select ua
                ).FirstOrDefault();

                if (request.Id == 0 && inApplication != null && inProvider != null)
                    throw new RepositoryException((int)EError.USER_ALREADY_EXISTS, Errors.UserAlreadyExists);

                if (inApplication == null)
                {
                    entity.ApiClients.Add(new UserApiClientEntity
                    {
                        ProfileId = request.ProfileId,
                        ApiClientId = apiClientId,
                        IsExternalApproved = request.IsExternalApproved,
                        AccessValidationToken = request.AccessValidationToken
                    });

                    userExistsInAnotherApplication = true;
                    emailChanging = false;
                    phoneChanging = false;
                }

                if (entity.Id == request.Id && request.Id > 0)
                {
                    //Troca de e-mail
                    emailChanging = !entity.Email.ToLower().Equals(request.Email.ToLower()) &&
                                    !string.IsNullOrWhiteSpace(request.Email);
                    //Troca de telephone
                    if (!string.IsNullOrWhiteSpace(request.PhoneNumber))
                        if (entity.PhoneNumber == null)
                            phoneChanging = true;
                        else
                            phoneChanging = !entity.PhoneNumber.ToLower().Equals(request.PhoneNumber.ToLower()) ||
                                            !entity.IsPhoneNumberValidated;

                    if (emailChanging)
                    {
                        entity.Email = request.Email.ToLower();
                        entity.EmailValidationToken = request.EmailValidationToken;
                        entity.IsEmailValidated = false;
                    }

                    if (phoneChanging)
                    {
                        entity.PhoneNumber = request.PhoneNumber?.ToLower();
                        entity.SmsValidationCode = request.SmsValidationCode;
                        entity.IsPhoneNumberValidated = false;
                    }

                    entity.Name = request.Name;
                    entity.Active = request.Active;
                    entity.Gender = (byte)request.Gender;
                    entity.Document = request.Document ?? entity.Document;
                    entity.ImageUrl = request.ImageUrl ?? entity.ImageUrl;
                    entity.BirthDate = request.BirthDate;
                    if (inApplication != null)
                        inApplication.ProfileId = request.ProfileId;
                }
                else
                {
                    // Atualiza senha quando cadastro novo do provider BUP
                    if (request.Provider == EAuthProvider.BUP)
                    {
                        // Não envia email ou sms se já tiver cadastro com outros providers
                        var listProviders =
                        (
                            from ua in entity.UserLogins
                            where ua.UserId == entity.Id
                            select ua
                        ).ToList();

                        if (listProviders.Count() > 1)
                        {
                            emailChanging = false;
                            phoneChanging = false;
                        }

                        //Se não tinha provider BUP, atualiza senha e pede validação de e-mail
                        //### CORREÇÃO DE BRECHA DE SEGURANÇA.
                        //Não deve permitir que usuário altere senha durante uma criação de conta e não revalide o e-mail
                        if (!hasBupProvider)
                        {
                            entity.Password = request.Password;

                            entity.Email = request.Email.ToLower();
                            entity.EmailValidationToken = request.EmailValidationToken;
                            entity.IsEmailValidated = false;
                            emailChanging = true;
                        }
                    }
                }
            }
            else
            {
                entity = new UserEntity(request);
            }

            GetWritable().CreateOrUpdate(entity);

            await SaveChangesAsync();

            var entityId = entity.Id;
            entity = await (
                    from u in GetWritable()
                    join uac in GetWritable<UserApiClientEntity>() on u.Id equals uac.UserId
                    where u.Id == entityId && uac.ApiClientId == request.ApiClientId
                    select u)
                .Include(i => i.ApiClients.Where(ww => ww.ApiClientId == apiClientId))
                .ThenInclude(i => i.ApiClient)
                .SingleOrDefaultAsync();

            ValidateEntity(entity);

            var result = entity.MapTo();
            result.EmailChanging = emailChanging;
            result.PhoneChanging = phoneChanging;

            return (userExistsInAnotherApplication, result, apiClientEntity.MapTo());
        }

        public async Task<bool> AdminActiveUserUpdate(UserAction update)
        {
            var id = update.UserId;
            var active = update.Action;
            var apiCode = update.ApiCode;
            var clientId = update.ClientId;
            var apiClientId = update.ApiClientId;

            var entity = await (
                from ua in GetWritable<UserApiClientEntity>()
                join ac in GetWritable<ApiClientEntity>() on ua.ApiClientId equals ac.Id
                join a in GetWritable<ApiEntity>() on ac.ApiId equals a.Id
                where ua.UserId == id &&
                      (a.Code == apiCode && ac.ClientId == clientId || ac.Id == apiClientId)
                select ua
            ).SingleOrDefaultAsync();

            if (entity == null)
                throw new NotFoundException((int)EError.USER_NOT_FOUND, Errors.UserNotFound);

            entity.Active = active;

            await SaveChangesAsync();

            return true;
        }

        public async Task<bool> AdminUserApproval(UserAction update)
        {
            var id = update.UserId;
            var approve = update.Action;
            var apiCode = update.ApiCode;
            var clientId = update.ClientId;
            var apiClientId = update.ApiClientId;

            var entity = await (
                from ua in GetWritable<UserApiClientEntity>()
                join ac in GetWritable<ApiClientEntity>() on ua.ApiClientId equals ac.Id
                join a in GetWritable<ApiEntity>() on ac.ApiId equals a.Id
                where ua.UserId == id &&
                      (a.Code == apiCode && ac.ClientId == clientId || ac.Id == apiClientId)
                select ua
            ).SingleOrDefaultAsync();

            if (entity == null)
                throw new NotFoundException((int)EError.USER_NOT_FOUND, Errors.UserNotFound);

            entity.IsExternalApproved = approve;

            await SaveChangesAsync();

            return true;
        }

        public async Task<bool> UpdatePassword(UpdatePassword updatePassword)
        {
            var id = updatePassword.Id;
            var oldPassword = updatePassword.OldPassword;
            var newPassword = updatePassword.NewPassword;
            var applicationId = updatePassword.ApplicationId;

            var entity = await (
                from u in GetWritable()
                join ua in GetWritable<UserApiClientEntity>() on u.Id equals ua.UserId
                where u.Id == id
                      && ua.ApiClientId == applicationId
                      && u.Password.Equals(oldPassword)
                select u
            ).SingleOrDefaultAsync();

            if (entity == null)
                throw new NotFoundException((int)EError.WRONG_PASSWORD, Errors.WrongPassword);

            entity.Password = newPassword;

            await SaveChangesAsync();

            return true;
        }

        public async Task<User> ValidateEmail(string auth)
        {
            var entity = await GetWritable().SingleOrDefaultAsync(x => x.EmailValidationToken.Equals(auth));

            if (entity == null)
                throw new RepositoryException((int)EError.WRONG_EMAIL_VALIDATION_TOKEN,
                    Errors.WrongEmailValidationToken);

            if (entity.IsEmailValidated)
                throw new RepositoryException((int)EError.EMAIL_ALREADY_VALIDATED, Errors.EmailAlreadyValidated);

            entity.IsEmailValidated = true;
            entity.EmailValidationToken = null;
            await SaveChangesAsync();

            return entity.MapTo();
        }

        public async Task<UserApiClient> ValidateExternalAccess(string auth)
        {
            var entity = await GetWritable<UserApiClientEntity>()
                .Include(i => i.User)
                .Include(i => i.ApiClient)
                .ThenInclude(ti => ti.Client)
                .SingleOrDefaultAsync(x => x.AccessValidationToken.Equals(auth));

            if (entity == null)
                throw new RepositoryException((int)EError.WRONG_EMAIL_VALIDATION_TOKEN,
                    Errors.WrongEmailValidationToken);

            if (entity.IsExternalApproved != null && entity.IsExternalApproved.Value)
                throw new RepositoryException((int)EError.ACCESS_ALREADY_APPROVED, Errors.AccessAlreadyApproved);

            entity.IsExternalApproved = true;
            entity.AccessValidationToken = null;
            await SaveChangesAsync();

            return entity.MapTo();
        }

        public async Task<bool> ValidateTelephone(string phoneNumber, string codeAuth)
        {
            var entity = await GetWritable().SingleOrDefaultAsync(
                x => x.PhoneNumber.Equals(phoneNumber)
                     && x.SmsValidationCode.Equals(codeAuth));

            if (entity == null)
                throw new RepositoryException((int)EError.WRONG_PHONE_VALIDATION_CODE,
                    Errors.WrongPhoneValidationCode);

            if (entity.IsPhoneNumberValidated)
                throw new RepositoryException((int)EError.PHONE_ALREADY_VALIDATED, Errors.PhoneAlreadyValidated);

            entity.IsPhoneNumberValidated = true;
            entity.SmsValidationCode = null;
            await SaveChangesAsync();

            return true;
        }

        public async Task<User> SetNewValidationEmailToken(string email, string auth)
        {
            var entity = await GetWritable().SingleOrDefaultAsync(x => x.Email.Equals(email));

            ValidateEntity(entity);

            if (entity.IsEmailValidated) return entity.MapTo();

            entity.EmailValidationToken = auth;
            await SaveChangesAsync();

            return entity.MapTo();
        }

        public async Task<(User, bool)> SetNewExternalAccessValidationToken(string email, int apiClientId, string auth)
        {
            var entity = await
                (
                    from u in GetWritable()
                    join uac in GetWritable<UserApiClientEntity>() on u.Id equals uac.UserId
                    where u.Email.Equals(email)
                          && uac.ApiClientId == apiClientId
                    select uac
                )
                .Include(i => i.User)
                .SingleOrDefaultAsync();

            ValidateEntity(entity);

            if (entity.IsExternalApproved == null || entity.IsExternalApproved.Value)
                return (entity.User.MapTo(), false);

            entity.AccessValidationToken = auth;
            await SaveChangesAsync();

            return (entity.User.MapTo(), true);
        }

        public async Task<(User, ApiClient)> SetNewValidationTelephoneCode(int apiClientId, int id, string phoneNumber,
            string codeAuth)
        {
            var entity = await (from u in GetWritable()
                join ua in GetWritable<UserApiClientEntity>() on u.Id equals ua.UserId
                join ac in GetWritable<ApiClientEntity>() on ua.ApiClientId equals ac.Id
                where u.Id == id &&
                      u.PhoneNumber.Equals(phoneNumber) &&
                      ua.ApiClientId == apiClientId
                select new { u, ac }).SingleOrDefaultAsync();

            ValidateEntity(entity.u);

            entity.u.SmsValidationCode = codeAuth;

            await SaveChangesAsync();

            return (entity.u.MapTo(), entity.ac.MapTo());
        }

        public async Task<User> SetNewPassword(Identity identity, string passwordAuth, int clientId = 0)
        {
            var email = identity.Email;
            var userName = identity.UserName;
            var apiClientId = identity.ApiClientId;
            var apiCode = identity.ApiCode;

            var entity = await (from u in GetWritable()
                    join ua in GetWritable<UserApiClientEntity>() on u.Id equals ua.UserId
                    join ac in GetWritable<ApiClientEntity>() on ua.ApiClientId equals ac.Id
                    join a in GetWritable<ApiEntity>() on ac.ApiId equals a.Id
                    where (
                              u.Email.Equals(email) && u.Email != null
                              || u.UserName.Equals(userName) && u.UserName != null
                          )
                          && u.Active.Value
                          && (ua.ApiClientId == apiClientId && apiClientId != 0 ||
                              a.Code == apiCode && apiCode != null && ac.ClientId == clientId && clientId != 0)
                          && ua.Active.Value
                    select u)
                .Include(x => x.ApiClients)
                .ThenInclude(ti => ti.ApiClient)
                .SingleOrDefaultAsync();

            ValidateEntity(entity);

            if (!entity.IsEmailValidated)
                throw new RepositoryException((int)EError.EMAIL_NOT_VALIDATED, Errors.EmailNotValidated);

            entity.Password = passwordAuth;

            await SaveChangesAsync();

            return entity.MapTo();
        }

        public async Task<bool> ExistsInAnotherPortal(string email)
        {
            var entity = await (from u in GetQueryable()
                    where u.Email.Equals(email) && u.Email != null
                    select u)
                .Include(i => i.ApiClients)
                .ThenInclude(t => t.ApiClient.Api)
                .SingleOrDefaultAsync();

            var has = entity?.ApiClients.Where(w => w.ApiClient.Api.Code == "PAP").FirstOrDefault();

            return has != null;
        }
    }
}