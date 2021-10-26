using System;
using System.Net;
using System.Threading.Tasks;
using Bridge.Commons.System.Models.Validations;
using Bridge.Unique.Profile.API.Models.Requests;
using Bridge.Unique.Profile.API.Models.Results;
using Bridge.Unique.Profile.Domain.Business.Contracts;
using Bridge.Unique.Profile.System.Resources;
using Microsoft.AspNetCore.Mvc;

namespace Bridge.Unique.Profile.API.Controllers
{
    /// <summary>
    ///     Email - Validação
    /// </summary>
    [Route("[controller]")]
    [ProducesResponseType(typeof(ErrorResult), (int)HttpStatusCode.BadRequest)]
    [ProducesResponseType(typeof(ErrorResult), (int)HttpStatusCode.Unauthorized)]
    [ProducesResponseType(typeof(ErrorResult), (int)HttpStatusCode.NotFound)]
    [ProducesResponseType(typeof(ErrorResult), (int)HttpStatusCode.InternalServerError)]
    [ProducesResponseType(typeof(ErrorResult), (int)HttpStatusCode.Forbidden)]
    public class EmailController : Controller
    {
        private readonly IUserBusiness _userBusiness;

        /// <summary>
        ///     Construtor
        /// </summary>
        /// <param name="userBusiness"></param>
        public EmailController(IUserBusiness userBusiness)
        {
            _userBusiness = userBusiness;
        }

        /// <summary>
        ///     Validar e-mail
        /// </summary>
        /// <param name="request">Objeto EmailValidationRequest</param>
        /// <returns></returns>
        /// <remarks>
        ///     Valida o e-mail do usuário a partir das informações do objeto `EmailValidationRequest`.
        /// </remarks>
        [ProducesResponseType(typeof(IActionResult), (int)HttpStatusCode.OK)]
        [HttpGet("validation")]
        public async Task<IActionResult> Validation([FromQuery] EmailValidationRequest request)
        {
            var message = Email.EmailValidationError;
            var result = new ValidationResult();

            try
            {
                result.IsValidated = await _userBusiness.ValidateEmail(request.Token);
                if (result.IsValidated)
                    message = Email.EmailValidated;
            }
            catch (Exception ex)
            {
                message = ex.Message;
            }

            result.Message = message;

            return View(result);
        }
    }
}