using System;
using Bridge.Commons.Redis.Context;
using Bridge.Commons.Redis.Contracts;
using Bridge.Commons.Redis.DataStructures;
using Bridge.Commons.System.AspNet.Settings;
using Bridge.Unique.Profile.API.Filters;
using Bridge.Unique.Profile.Domain.Business;
using Bridge.Unique.Profile.Domain.Business.Contracts;
using Bridge.Unique.Profile.Domain.Contexts.Contracts;
using Bridge.Unique.Profile.Domain.Repositories.Contracts;
using Bridge.Unique.Profile.Postgres.Context;
using Bridge.Unique.Profile.Postgres.Repositories;
using Bridge.Unique.Profile.Redis.Repositories;
using Bridge.Unique.Profile.System.Settings;
using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Bridge.Unique.Profile.API.Extensions
{
    /// <summary>
    ///     Extensão de autenticação
    /// </summary>
    public static class AuthenticationExtension
    {
        /// <summary>
        ///     Adicionar autenticação do Bup
        /// </summary>
        /// <param name="services">Conteiner services</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static AuthenticationBuilder AddBupAuthentication(this IServiceCollection services)
        {
            if (services == null)
                throw new ArgumentNullException(nameof(services));
            var dotNetSettings = new DotNetSettings();
            var appSettings = dotNetSettings.GetAppSettings<AppSettings>();
            services.AddSingleton(appSettings);
            services.AddSingleton<IBupReadContext>(new BupReadContext(
                new DbContextOptionsBuilder<BupReadContext>()
                    .UseNpgsql(appSettings.ConnectionStrings.BUPReadContext).Options,
                new LoggerFactory()
            ));
            services.AddSingleton<IBupWriteContext>(new BupWriteContext(
                new DbContextOptionsBuilder<BupWriteContext>()
                    .UseNpgsql(appSettings.ConnectionStrings.BUPWriteContext).Options,
                new LoggerFactory()
            ));
            services.AddSingleton<IAuthenticationBusiness, AuthenticationBusiness>();
            services.AddSingleton<IApiClientRepository, ApiClientRepository>();
            services.AddSingleton<IAuthenticationRepository, AuthenticationRepository>();
            services.AddSingleton<IRedisContext>(new RedisContext(appSettings.Redis.GetServer()));
            services.AddSingleton<IRedisSingle, RedisSingle>();
            services.AddSingleton<AdminAuthorizeFilter>();
            services.AddSingleton<UserAuthorizeFilter>();
            return new AuthenticationBuilder(services);
        }
    }
}