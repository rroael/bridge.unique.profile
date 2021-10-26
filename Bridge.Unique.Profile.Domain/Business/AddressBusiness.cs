using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Bridge.Commons.Location.Models;
using Bridge.Commons.System.Exceptions;
using Bridge.Commons.System.Models;
using Bridge.Commons.System.Models.Results;
using Bridge.Commons.Validation.Contracts;
using Bridge.Unique.Profile.Communication.Enums;
using Bridge.Unique.Profile.Communication.Resources;
using Bridge.Unique.Profile.Domain.Business.Contracts;
using Bridge.Unique.Profile.Domain.Models;
using Bridge.Unique.Profile.Domain.Models.Providers.Google.Maps;
using Bridge.Unique.Profile.Domain.Repositories.Contracts;
using Bridge.Unique.Profile.System.Settings;
using ViaCep;

namespace Bridge.Unique.Profile.Domain.Business
{
    public class AddressBusiness : BaseBusiness, IAddressBusiness
    {
        private readonly IAddressRepository _addressRepository;
        private readonly AppSettings _appSettings;
        private readonly HttpClient _httpClient;
        private readonly IViaCepClient _viaCepClient;

        public AddressBusiness(IAddressRepository addressRepository, IViaCepClient viaCepClient,
            IHttpClientFactory httpClientFactory, AppSettings appSettings,
            IBaseValidator<Address> addressValidator) : base(addressValidator)
        {
            _addressRepository = addressRepository;
            _viaCepClient = viaCepClient;
            _httpClient = httpClientFactory.CreateClient();
            _appSettings = appSettings;
        }

        public async Task<Address> Get(long id)
        {
            return await Execute(_addressRepository.Get, new IdentifiableLong(id));
        }

        public async Task<Address> Get(long id, int userId)
        {
            return await Execute(_addressRepository.Get, new IdentifiableLong(id), userId);
        }

        public async Task<PaginatedList<Address>> List(IEnumerable<long> ids, Pagination pagination)
        {
            return await _addressRepository.List(ids, pagination);
        }

        public async Task<PaginatedList<Address>> ListByUser(int userId, Pagination pagination)
        {
            return await _addressRepository.ListByUser(new IdentifiableLong(userId), pagination);
        }

        public async Task<Address> Save(Address address)
        {
            return await ExecuteValidate(_addressRepository.Save, address);
        }

        public async Task<bool> Delete(long id, int userId)
        {
            return await Execute(_addressRepository.Delete, new IdentifiableLong(id), userId);
        }

        public async Task<Address> GetByZipCode(string zipCode, ApiClient apiClient)
        {
            try
            {
                var result = _viaCepClient.Search(zipCode);
                if (result?.Street == null)
                    throw new BusinessException((int)EError.ADDRESS_ZIPCODE_NOT_FOUND, Errors.AddressZipCodeNotFound);

                var resultLocation =
                    await GetLocationByAddress(result.Street, result.City, result.StateInitials);

                return new Address
                {
                    ZipCode = result.ZipCode,
                    City = result.City,
                    Street = result.Street,
                    Neighborhood = result.Neighborhood,
                    Complement = result.Complement,
                    State = result.StateInitials,
                    Location = resultLocation.Location
                };
            }
            catch (Exception)
            {
                throw new BusinessException((int)EError.ADDRESS_ZIPCODE_NOT_FOUND, Errors.AddressZipCodeNotFound);
            }
        }

        private async Task<Address> GetLocationByAddress(string street, string city, string state)
        {
            var key = "my-google-maps-key";

            var addressFormatted = street + "," + city + "," + state;
            var urlTokenInfo = "https://maps.googleapis.com/maps/api/geocode/json?address=" + addressFormatted +
                               "&key=" + key;
            var response = await _httpClient.GetAsync(urlTokenInfo);
            if (!response.IsSuccessStatusCode)
                return new Address();

            var responseObj = await response.Content.ReadAsAsync<MapsInfoGoogle>();

            var addressMap = responseObj.results?.FirstOrDefault();
            if (addressMap != null)
                return new Address
                {
                    Location = new LocationPoint
                    {
                        Latitude = addressMap.geometry.location.lat,
                        Longitude = addressMap.geometry.location.lng
                    }
                };

            return new Address();
        }
    }
}