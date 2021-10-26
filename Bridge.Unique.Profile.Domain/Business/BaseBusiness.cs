using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Bridge.Commons.Extension.Enums;
using Bridge.Commons.System.Contracts;
using Bridge.Commons.System.Exceptions;
using Bridge.Commons.Validation.Contracts;
using Bridge.Unique.Profile.Domain.Factories;
using FluentValidation;
using FluentValidation.Internal;
using FluentValidation.Results;
using Newtonsoft.Json;

namespace Bridge.Unique.Profile.Domain.Business
{
    public class BaseBusiness
    {
        protected const EHashType HashType = EHashType.SHA512;
        public readonly IDictionary<Type, IValidator> _validator;

        /// <summary>
        ///     Construtor da Business com o array de validadores
        /// </summary>
        /// <param name="validators"></param>
        protected BaseBusiness(params IBaseValidator[] validators)
        {
            _validator = new Dictionary<Type, IValidator>();

            foreach (var validator in validators) _validator.Add(validator.GetEntityType(), validator);
        }

        /// <summary>
        ///     Construtor da Business com o array de tipos de validadores e o array de validadores
        /// </summary>
        /// <param name="types"></param>
        /// <param name="validators"></param>
        protected BaseBusiness(Type[] types, params IBaseValidator[] validators)
        {
            _validator = new Dictionary<Type, IValidator>();

            for (var i = 0; i < types.Length; i++)
                _validator.Add(types[i], validators[i]);
        }


        /// <summary>
        ///     Valida o objeto do domínio da Business, a instancia do validador precisa ser injetada no construtor da classe
        /// </summary>
        /// <param name="model"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public async Task<bool> Validate(IBaseModel model, Type index = null)
        {
            if (index == null)
                index = model.GetType();

            if (_validator.ContainsKey(index))
            {
                var validationResult = await _validator[index].ValidateAsync(new ValidationContext<object>(model));
                IList<ValidationResult> results = new List<ValidationResult>();

                if (!validationResult.IsValid)
                    results.Add(validationResult);

                if (results.Count > 0)
                    throw ValidationExceptionFactory.Create<BusinessException>(results);

                return true;
            }

            throw new BusinessException(-1, "Validator não encontrado.");
        }

        /// <summary>
        ///     Retorna o validador através do tipo do modelo de domínio
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        private IValidator<T> GetTypeValidator<T>() where T : IBaseModel
        {
            var type = typeof(T);

            try
            {
                return (IValidator<T>)_validator[type];
            }
            catch (Exception)
            {
                throw new BusinessException(-1, "Validator não encontrado.");
            }
        }

        /// <summary>
        ///     Executa uma regra específica do validador do modelo do domínio
        /// </summary>
        /// <typeparam name="T">Tipo do modelo do domínio</typeparam>
        /// <param name="ruleSet">Nome da regra</param>
        /// <param name="instance">Instância do objeto de domínio</param>
        /// <returns></returns>
        public async Task ValidateRuleSet<T>(string ruleSet, T instance) where T : IBaseModel
        {
            var validator = GetTypeValidator<T>();

            var validationResult =
                await validator.ValidateAsync(instance, options => options.IncludeRuleSets(ruleSet));

            IList<ValidationResult> results = new List<ValidationResult>();

            if (!validationResult.IsValid)
                results.Add(validationResult);

            if (results.Count > 0)
                throw ValidationExceptionFactory.Create<BusinessException>(results);
        }

        /// <summary>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="ruleSet"></param>
        /// <param name="instance"></param>
        /// <seealso
        ///     cref="https://github.com/JeremySkinner/FluentValidation/blob/master/src/FluentValidation/DefaultValidatorExtensions.cs" />
        /// <returns></returns>
        public ValidationContext<T> CreateValidationContextForRuleSet<T>(string ruleSet, T instance)
        {
            IValidatorSelector selector;

            var ruleSetNames = ruleSet.Split(',', ';');
            selector = ValidatorOptions.Global.ValidatorSelectors.RulesetValidatorSelectorFactory(ruleSetNames);

            var context = new ValidationContext<T>(instance, new PropertyChain(), selector);
            return context;
        }

        /// <summary>
        ///     Realiza uma validação customizada e passa informações necessárias para validação via ValidationContext.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="ruleSet"></param>
        /// <param name="context"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task ValidateRuleSet<T>(string ruleSet, ValidationContext<T> context,
            CancellationToken cancellationToken = default)
            where T : IBaseModel
        {
            var validator = GetTypeValidator<T>();

            var validationResult = await validator.ValidateAsync(context, cancellationToken);

            IList<ValidationResult> results = new List<ValidationResult>();

            if (!validationResult.IsValid)
                results.Add(validationResult);

            if (results.Count > 0)
                throw ValidationExceptionFactory.Create<BusinessException>(results);
        }

        /// <summary>
        ///     Realiza uma validação customizada para um objeto do domínio e adiciona ao contexto do validador um dicionário de
        ///     parâmetros
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="ruleSet"></param>
        /// <param name="request"></param>
        /// <param name="customParameters"></param>
        /// <returns></returns>
        public async Task ValidateRuleSet<T>(string ruleSet, T request, IDictionary<string, object> customParameters)
            where T : IBaseModel
        {
            var validationContext = CreateValidationContextForRuleSet(ruleSet, request);

            foreach (var parameter in customParameters)
                validationContext.RootContextData[parameter.Key] = parameter.Value;

            await ValidateRuleSet(ruleSet, validationContext);
        }

        /// <summary>
        ///     Executa uma validação de parâmetro e após aprovado executa o método
        /// </summary>
        /// <typeparam name="TResult">Tipo do objeto retornado para o método executado</typeparam>
        /// <typeparam name="TParam">Tipo do parâmetro do método validado e executado</typeparam>
        /// <param name="function">Método executado</param>
        /// <param name="parameter">Parâmetro validado e executado</param>
        /// <returns></returns>
        protected async Task<TResult> ExecuteValidate<TResult, TParam>(Func<TParam, Task<TResult>> function,
            TParam parameter)
            where TParam : IBaseModel
        {
            await Validate(parameter);
            return await Execute(function, parameter);
        }

        /// <summary>
        ///     Executa um método sem parâmetro e trata as exceções
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="task"></param>
        /// <returns></returns>
        protected async Task<TResult> Execute<TResult>(Func<Task<TResult>> task)
        {
            try
            {
                return await task();
            }
            catch (BaseException)
            {
                throw;
            }
            catch (Exception e)
            {
                throw new RepositoryException(e);
            }
        }

        /// <summary>
        ///     Executa um método com parâmetro e trata as exceções
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <typeparam name="TParam"></typeparam>
        /// <param name="function"></param>
        /// <param name="parameter"></param>
        /// <returns></returns>
        protected async Task<TResult> Execute<TResult, TParam>(Func<TParam, Task<TResult>> function, TParam parameter)
        {
            try
            {
                return await function(parameter);
            }
            catch (BaseException)
            {
                throw;
            }
            catch (Exception e)
            {
                throw new RepositoryException(e);
            }
        }

        /// <summary>
        ///     Executa um método com parâmetro e trata as exceções
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <typeparam name="TParam1"></typeparam>
        /// <typeparam name="TParam2"></typeparam>
        /// <param name="function"></param>
        /// <param name="parameter1"></param>
        /// <param name="parameter2"></param>
        /// <returns></returns>
        protected async Task<TResult> Execute<TResult, TParam1, TParam2>(Func<TParam1, TParam2, Task<TResult>> function,
            TParam1 parameter1, TParam2 parameter2)
        {
            try
            {
                return await function(parameter1, parameter2);
            }
            catch (BaseException)
            {
                throw;
            }
            catch (Exception e)
            {
                throw new RepositoryException(e);
            }
        }

        /// <summary>
        ///     Executa um método com parâmetro e trata as exceções
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <typeparam name="TParam1"></typeparam>
        /// <typeparam name="TParam2"></typeparam>
        /// <typeparam name="TParam3"></typeparam>
        /// <param name="function"></param>
        /// <param name="parameter1"></param>
        /// <param name="parameter2"></param>
        /// <param name="parameter3"></param>
        /// <returns></returns>
        protected async Task<TResult> Execute<TResult, TParam1, TParam2, TParam3>(
            Func<TParam1, TParam2, TParam3, Task<TResult>> function, TParam1 parameter1, TParam2 parameter2,
            TParam3 parameter3)
        {
            try
            {
                return await function(parameter1, parameter2, parameter3);
            }
            catch (BaseException)
            {
                throw;
            }
            catch (Exception e)
            {
                throw new RepositoryException(e);
            }
        }

        /// <summary>
        ///     Execução de métodos por transação, executa os métodos da lista de execução e caso ocorra uma exception executa os
        ///     métodos da lista de rollback utilizando a ordem de execução
        /// </summary>
        /// <param name="execution">Lista de métodos que devem ser executados</param>
        /// <param name="rollback">Lista de métodos que devem ser executados caso ocorra alguma exceção durante o processo</param>
        /// <returns></returns>
        protected async Task ExecuteInTransaction(List<Func<Task>> execution, List<Func<Task>> rollback)
        {
            int i, l;
            var operations = Enumerable.Repeat(false, execution.Count).ToArray();
            try
            {
                for (i = 0, l = execution.Count; i < l; i++)
                {
                    await execution[i]();
                    operations[i] = true;
                }
            }
            catch (Exception)
            {
                for (i = 0, l = rollback.Count; i < l; i++)
                    if (operations[i])
                        await rollback[i]();

                throw;
            }
        }

        protected BusinessException ThrowExceptionFromService(IEnumerable<ValidationResult> errors)
        {
            return ValidationExceptionFactory.Create<BusinessException>(errors);
        }

        protected BusinessException ThrowExceptionFromContent(string content)
        {
            var result = JsonConvert.DeserializeObject<ValidationResult>(content);
            var errors = new List<ValidationResult> { result };
            return ThrowExceptionFromService(errors);
        }
    }
}