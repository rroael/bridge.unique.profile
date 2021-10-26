using System;
using Bridge.Commons.Notification.Aws.Settings;
using Bridge.Commons.Notification.Aws.Sms;
using Bridge.Commons.Notification.Mail;
using Bridge.Commons.Notification.Mail.Contracts;
using Bridge.Commons.Notification.Mail.Settings;
using Bridge.Commons.Notification.Sms.Contracts;
using Bridge.Commons.Redis.Context;
using Bridge.Commons.Redis.Contracts;
using Bridge.Commons.Redis.DataStructures;
using Bridge.Commons.System.AspNet.Settings;
using Bridge.Commons.System.Contracts.DependencyInjection;
using Bridge.Commons.System.Contracts.Settings;
using Bridge.Commons.System.Wrappers;
using Bridge.Commons.Validation.Contracts;
using Bridge.Unique.Profile.Domain.Business;
using Bridge.Unique.Profile.Domain.Business.Contracts;
using Bridge.Unique.Profile.Domain.Contexts.Contracts;
using Bridge.Unique.Profile.Domain.Models;
using Bridge.Unique.Profile.Domain.Repositories.Contracts;
using Bridge.Unique.Profile.Domain.Validators;
using Bridge.Unique.Profile.Postgres.Context;
using Bridge.Unique.Profile.Postgres.Repositories;
using Bridge.Unique.Profile.Redis.Repositories;
using Bridge.Unique.Profile.System.Settings;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using SimpleInjector;
using SimpleInjector.Integration.AspNetCore.Mvc;
using SimpleInjector.Lifestyles;
using ViaCep;

namespace Bridge.Unique.Profile.IOC
{
    public class Container : IContainer
    {
        private static SimpleInjector.Container _container;
        private static bool _verified;

        #region INIT

        public static void Init()
        {
            _container = new SimpleInjector.Container();
            _container.Options.DefaultScopedLifestyle = new AsyncScopedLifestyle();
            _container.Options.DefaultLifestyle = new AsyncScopedLifestyle();

            _container.Register<IContainer, Container>(Lifestyle.Singleton);
            _container.Register<ISettings, DotNetSettings>(Lifestyle.Singleton);
        }

        public static void InitRegisters(ILoggerFactory loggerFactory)
        {
            RegisterValidator();
            RegisterData(loggerFactory);
            RegisterBusiness();
            RegisterRepository();
            RegisterConfiguration();
            RegisterService();
        }

        public static void InitServices(IServiceCollection services)
        {
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddSingleton<IControllerActivator>(new SimpleInjectorControllerActivator(_container));
            services.AddSingleton<IViewComponentActivator>(new SimpleInjectorViewComponentActivator(_container));

            services.AddSimpleInjector(_container,
                options => options
                    .AddAspNetCore()
                    .AddControllerActivation()
                    .AddViewComponentActivation()
            );

            //services.EnableSimpleInjectorCrossWiring(_container);
            services.UseSimpleInjectorAspNetRequestScoping(_container);

            services.AddHttpClient<IViaCepClient, ViaCepClient>(client =>
            {
                client.BaseAddress = new Uri("https://viacep.com.br/");
            });
        }

        public static void InitApplication()
        {
            //_container.AutoCrossWireAspNetComponents(app);

            if (_verified) return;

            _container.Verify();
            _verified = true;
        }

        public static void Verify()
        {
            if (_verified) return;

            _container.Verify();
            _verified = true;
        }

        #endregion

        #region DEPENDENCIES

        private static void RegisterBusiness()
        {
            _container.Register<IAddressBusiness, AddressBusiness>(Lifestyle.Scoped);
            _container.Register<IAuthenticationBusiness, AuthenticationBusiness>(Lifestyle.Scoped);
            _container.Register<IApiBusiness, ApiBusiness>(Lifestyle.Scoped);
            _container.Register<IClientBusiness, ClientBusiness>(Lifestyle.Scoped);
            _container.Register<IContactBusiness, ContactBusiness>(Lifestyle.Scoped);
            _container.Register<IUserBusiness, UserBusiness>(Lifestyle.Scoped);
            _container.Register<IClientUserBusiness, ClientUserBusiness>(Lifestyle.Scoped);
        }

        private static void RegisterConfiguration()
        {
            _container.Register(() =>
                    SettingsWrapper.GetSettings<AppSettings, AppSettings>(
                        x => x,
                        GetIContainer()),
                Lifestyle.Singleton);
        }

        private static void RegisterData(ILoggerFactory loggerFactory)
        {
            _container.Register<IBupReadContext>(() =>
                new BupReadContext(
                    new DbContextOptionsBuilder<BupReadContext>()
                        .UseNpgsql(SettingsWrapper.GetSettings<AppSettings, string>(
                            x => x.ConnectionStrings.BUPReadContext,
                            GetIContainer())).Options, loggerFactory), Lifestyle.Scoped);
            _container.Register<IBupWriteContext>(() =>
                new BupWriteContext(
                    new DbContextOptionsBuilder<BupWriteContext>()
                        .UseNpgsql(SettingsWrapper.GetSettings<AppSettings, string>(
                            x => x.ConnectionStrings.BUPWriteContext,
                            GetIContainer())).Options, loggerFactory), Lifestyle.Scoped);
            _container.Register<IRedisContext>(
                () => new RedisContext(
                    SettingsWrapper.GetSettings<AppSettings, string[]>(x => x.Redis.GetServer(), GetIContainer())),
                Lifestyle.Singleton);
        }

        private static void RegisterRepository()
        {
            _container.Register<IAddressRepository, AddressRepository>(Lifestyle.Scoped);
            _container.Register<IApiRepository, ApiRepository>(Lifestyle.Scoped);
            _container.Register<IAuthenticationRepository, AuthenticationRepository>(Lifestyle.Scoped);
            _container.Register<IClientRepository, ClientRepository>(Lifestyle.Scoped);
            _container.Register<IContactRepository, ContactRepository>(Lifestyle.Scoped);
            _container.Register<IUserRepository, UserRepository>(Lifestyle.Scoped);
            _container.Register<IApiClientRepository, ApiClientRepository>(Lifestyle.Scoped);
            _container.Register<IRedisSingle, RedisSingle>(Lifestyle.Singleton);
        }

        private static void RegisterService()
        {
            _container.Register<IMailService>(
                () => new SmtpService(
                    SettingsWrapper.GetSettings<AppSettings, SmtpSettings>(x => x.SmtpServer, GetIContainer())),
                Lifestyle.Scoped);
            _container.Register<ISmsService>(
                () =>
                    new AwsSmsService(
                        SettingsWrapper.GetSettings<AppSettings, AwsCredentials>(x =>
                                x.AmazonGlobal,
                            GetIContainer())),
                Lifestyle.Scoped
            );
        }

        private static void RegisterValidator()
        {
            _container.Register<IBaseValidator<Address>, AddressValidator>(Lifestyle.Scoped);
            _container.Register<IBaseValidator<Api>, ApiValidator>(Lifestyle.Scoped);
            _container.Register<IBaseValidator<ClientContact>, ClientContactValidator>(Lifestyle.Scoped);
            _container.Register<IBaseValidator<Client>, ClientValidator>(Lifestyle.Scoped);
            _container.Register<IBaseValidator<Contact>, ContactValidator>(Lifestyle.Scoped);
            _container.Register<IBaseValidator<Token>, TokenValidator>(Lifestyle.Scoped);
            _container.Register<IBaseValidator<UpdatePassword>, UpdatePasswordValidator>(Lifestyle.Scoped);
            _container.Register<IBaseValidator<UserAddress>, UserAddressValidator>(Lifestyle.Scoped);
            _container.Register<IBaseValidator<UserLogin>, UserLoginValidator>(Lifestyle.Scoped);
            _container.Register<IBaseValidator<User>, UserValidator>(Lifestyle.Scoped);
        }

        #endregion

        #region MEMBERS

        public static IContainer GetIContainer()
        {
            return _container.GetInstance<IContainer>();
        }

        public static SimpleInjector.Container GetContainer()
        {
            return _container;
        }

        public T GetInstance<T>() where T : class
        {
            if (_container == null)
                Init();

            if (_container == null)
                throw new NullReferenceException("Variable _container is null.");

            return _container.GetInstance<T>();
        }

        #endregion
    }
}