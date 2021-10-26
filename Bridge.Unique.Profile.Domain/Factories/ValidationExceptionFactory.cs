using System.Collections.Generic;
using System.Linq;
using Bridge.Commons.System.Exceptions;
using Bridge.Commons.System.Models.Validations;
using FluentValidation.Results;

namespace Bridge.Unique.Profile.Domain.Factories
{
    public class ValidationExceptionFactory
    {
        public static TException Create<TException>(IEnumerable<ValidationResult> results)
            where TException : BaseException
        {
            IList<ErrorInstance> errors = new List<ErrorInstance>();

            errors = results.Aggregate(errors, (current, result) => current.Concat(result.Errors.Select(x =>
                {
                    var error = x.CustomState == null
                        ? new ErrorInstance(-1, x.ErrorMessage)
                        : x.CustomState as ErrorInstance;

                    if (error != null) error.Field = x.PropertyName;

                    return error;
                }))
                .ToList());

            return (TException)new BaseException(errors);
        }
    }
}