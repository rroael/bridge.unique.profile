using Bridge.Commons.System.Contracts.Mappers;
using Bridge.Unique.Profile.Communication.Models.Out;
using Bridge.Unique.Profile.Domain.Models;

namespace Bridge.Unique.Profile.API.Models.Results
{
    /// <summary>
    ///     Resultado de endereço
    /// </summary>
    public class AddressResult : AddressOut, IFromObjectMapper<Address, AddressResult>
    {
        /// <summary>
        ///     Construtor
        /// </summary>
        public AddressResult()
        {
        }

        /// <summary>
        ///     Construtor
        /// </summary>
        /// <param name="model">Modelo</param>
        public AddressResult(Address model)
        {
            MapFrom(model);
        }

        /// <summary>
        ///     Mapeia modelo para resultado
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public AddressResult MapFrom(Address input)
        {
            Id = input.Id;
            Nickname = input.Nickname;
            Street = input.Street;
            StreetNumber = input.StreetNumber;
            Neighborhood = input.Neighborhood;
            City = input.City;
            State = input.State;
            Country = input.Country;
            Complement = input.Complement;
            AddressTypes = input.AddressTypes;
            ZipCode = input.ZipCode;
            Location = input.Location;
            Active = input.Active;
            Name = input.Name;
            Email = input.Email;
            PhoneNumber = input.PhoneNumber;

            return this;
        }
    }
}