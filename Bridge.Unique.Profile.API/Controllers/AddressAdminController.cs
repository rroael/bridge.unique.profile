using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Bridge.Commons.System.Models;
using Bridge.Commons.System.Models.Validations;
using Bridge.Unique.Profile.API.Attributes;
using Bridge.Unique.Profile.API.Models.Requests;
using Bridge.Unique.Profile.API.Models.Results;
using Bridge.Unique.Profile.Domain.Business.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace Bridge.Unique.Profile.API.Controllers
{
    /// <summary>
    ///     Administração de endereços
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    [Produces("application/json")]
    [AdminAuthorize]
    [ProducesResponseType(typeof(ErrorResult), (int)HttpStatusCode.BadRequest)]
    [ProducesResponseType(typeof(ErrorResult), (int)HttpStatusCode.Unauthorized)]
    [ProducesResponseType(typeof(ErrorResult), (int)HttpStatusCode.NotFound)]
    [ProducesResponseType(typeof(ErrorResult), (int)HttpStatusCode.InternalServerError)]
    [ProducesResponseType(typeof(ErrorResult), (int)HttpStatusCode.Forbidden)]
    public class AddressAdminController : BaseController
    {
        private readonly IAddressBusiness _addressBusiness;

        /// <summary>
        ///     Construtor
        /// </summary>
        /// <param name="addressBusiness">DI de endereço</param>
        /// <param name="authenticationBusiness">DI de autenticação</param>
        public AddressAdminController(IAddressBusiness addressBusiness,
            IAuthenticationBusiness authenticationBusiness) :
            base(authenticationBusiness)
        {
            _addressBusiness = addressBusiness;
        }

        /// <summary>
        ///     Buscar endereço por identificador
        /// </summary>
        /// <remarks>
        ///     Busca um endereço a partir de um identificador, retornando um objeto `AddressResult` com o endereço encontrado.
        ///     Tipos de endereço (É um somatório):
        ///     |  Valor  |   Nome  |  Descrição   |
        ///     | ------- | --------| ------------ |
        ///     | 1  |  BILLING | Endereço de cobrança |
        ///     | 2  |  COMMERCIAL | Endereço comercial |
        ///     | 4  |  RESIDENTIAL | Endereço residencial |
        ///     | 8  |  SHIPPING | Endereço de entrega |
        ///     | 16 |  DEFAULT | Endereço principal |
        /// </remarks>
        /// <param name="id">Identificador do endereço</param>
        /// <returns></returns>
        [ProducesResponseType(typeof(AddressResult), (int)HttpStatusCode.OK)]
        [HttpGet("{id}")]
        public async Task<AddressResult> Get(long id)
        {
            var result = await _addressBusiness.Get(id);
            return new AddressResult(result);
        }

        /// <summary>
        ///     Criação de endereço avulso, de forma administrativa e sem vínculos
        /// </summary>
        /// <param name="request">Objeto AddressRequest</param>
        /// <returns>Objeto AddressResult</returns>
        [ProducesResponseType(typeof(AddressResult), (int)HttpStatusCode.OK)]
        [HttpPost]
        public async Task<AddressResult> Create([FromBody] AddressRequest request)
        {
            var result = await _addressBusiness.Save(request.MapTo());
            return new AddressResult(result);
        }

        /// <summary>
        ///     Atualização de endereço avulso, de forma administrativa e sem vínculos
        /// </summary>
        /// <param name="request">Objeto AddressUpdateRequest</param>
        /// <returns>Objeto AddressResult</returns>
        [ProducesResponseType(typeof(AddressResult), (int)HttpStatusCode.OK)]
        [HttpPut]
        public async Task<AddressResult> Update([FromBody] AddressUpdateRequest request)
        {
            var result = await _addressBusiness.Save(request.MapTo());
            return new AddressResult(result);
        }

        /// <summary>
        ///     Lista de endereços por identificadores
        /// </summary>
        /// <param name="request">Lista de identificadores</param>
        /// <returns>Lista de endereços</returns>
        [ProducesResponseType(typeof(List<AddressResult>), (int)HttpStatusCode.OK)]
        [HttpGet("listbyid")]
        public async Task<List<AddressResult>> ListByIds([FromBody] AddressListByIdRequest request)
        {
            var results = await _addressBusiness.List(request.Ids, new Pagination
            {
                Page = 1,
                PageSize = request.Ids.Length
            });

            var res = results.ConvertTo<AddressResult>();
            return res.Data?.ToList();
        }

        /// <summary>
        ///     Buscar endereço por cep
        /// </summary>
        /// <remarks>
        ///     Busca um endereço a partir de um cep, retornando um objeto `AddressResult` com o endereço encontrado.
        ///     Tipos de endereço (É um somatório):
        ///     |  Valor  |   Nome  |  Descrição   |
        ///     | ------- | --------| ------------ |
        ///     | 1  |  BILLING | Endereço de cobrança |
        ///     | 2  |  COMMERCIAL | Endereço comercial |
        ///     | 4  |  RESIDENTIAL | Endereço residencial |
        ///     | 8  |  SHIPPING | Endereço de entrega |
        ///     | 16 |  DEFAULT | Endereço principal |
        /// </remarks>
        /// <param name="zipcode">Identificador do cep</param>
        /// <returns></returns>
        [ProducesResponseType(typeof(AddressResult), (int)HttpStatusCode.OK)]
        [HttpGet("zipcode/{zipcode}")]
        public async Task<AddressResult> GetByZipCode(string zipcode)
        {
            var result = await _addressBusiness.GetByZipCode(zipcode, GetApiClientFromContext);
            return new AddressResult(result);
        }
    }
}