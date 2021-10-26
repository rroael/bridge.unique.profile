using Bridge.Commons.System.Contracts.Mappers;
using Bridge.Unique.Profile.Communication.Models.Out;
using Bridge.Unique.Profile.Domain.Models;

namespace Bridge.Unique.Profile.API.Models.Results
{
    /// <summary>
    ///     Resultado de usuário
    /// </summary>
    public class UserResult : UserOut, IFromObjectMapper<User, UserResult>
    {
        /// <summary>
        ///     Construtor
        /// </summary>
        public UserResult()
        {
        }

        /// <summary>
        ///     Construtor
        /// </summary>
        /// <param name="model">Modelo</param>
        public UserResult(User model)
        {
            MapFrom(model);
        }

        /// <summary>
        ///     Mapeia modelo para resultado
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public UserResult MapFrom(User input)
        {
            Id = input.Id;
            UserName = input.UserName;
            Name = input.Name;
            Document = input.Document;
            ImageUrl = input.ImageUrl;
            Email = input.Email;
            PhoneNumber = input.PhoneNumber;
            BirthDate = input.BirthDate;
            Gender = (int)input.Gender;
            CreateDate = input.CreateDate;
            ProfileId = input.ProfileId;
            Token = new TokenResult(input.Token);
            Active = input.Active;
            TermsAcceptanceDate = input.TermsAcceptanceDate;
            IsExternalApproved = input.IsExternalApproved;
            IsPhoneNumberValidated = input.IsPhoneNumberValidated;
            IsEmailValidated = input.IsEmailValidated;

            if (input.ApiClients != null)
                foreach (var ac in input.ApiClients)
                    ApiClients.Add(new UserApiClientResult(ac));

            if (input.Addresses == null) return this;

            foreach (var uad in input.Addresses)
                Addresses.Add(new AddressResult(uad.Address));

            return this;
        }
    }
}