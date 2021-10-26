using System;
using Bridge.Unique.Profile.Wrapper.Configurations;
using Bridge.Unique.Profile.Wrapper.Contracts;
using Bridge.Unique.Profile.Wrapper.Filters;
using Bridge.Unique.Profile.Wrapper.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.DependencyInjection;

namespace Bridge.Unique.Profile.Wrapper.Extensions
{
    /// <summary>
    ///     Extensão de autenticação
    /// </summary>
    public static class AuthenticationExtension
    {
        /// <summary>
        ///     Adiciona autenticação
        /// </summary>
        /// <param name="services"></param>
        /// <param name="bupConfiguration"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static AuthenticationBuilder AddBupAuthentication(this IServiceCollection services,
            BupConfiguration bupConfiguration)
        {
            if (services == null)
                throw new ArgumentNullException(nameof(services));
            services.AddSingleton<IBupAuthenticationService>(
                new BupAuthenticationService(bupConfiguration));
            services.AddSingleton<AdminAuthorizeFilter>();
            services.AddSingleton<UserAuthorizeFilter>();
            return new AuthenticationBuilder(services);
        }

        /// <summary>
        ///     Adicionar autenticação
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configExpression"></param>
        /// <returns></returns>
        public static AuthenticationBuilder AddBupAuthentication(this IServiceCollection services,
            Action<BupConfiguration> configExpression)
        {
            var bupConfiguration = new BupConfiguration();
            configExpression.Invoke(bupConfiguration);
            return services.AddBupAuthentication(bupConfiguration);
        }
    }
}