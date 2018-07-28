using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using Microsoft.Practices.Unity;
using Repository.EF6;
using Repository.Pattern.DataContext;
using Repository.Pattern.Repositories;
using Repository.Pattern.UnitOfWork;
using System;
using System.Web;
using System.Web.Http;
using Himalayan_Angular.App_Start;
using Himalayan_Angular.Business.Services;
using Himalayan_Angular.Data;
using Unity.WebApi;

namespace Himalayan_Angular.App_Start
{
    public static class UnityConfig
    {
        private static Lazy<IUnityContainer> container = new Lazy<IUnityContainer>(() =>
        {
            var container = new UnityContainer();
            RegisterTypes(container);
            return container;
        }
        );

        public static IUnityContainer GetConfiguredContainer()
        {
            return container.Value;
        }

        public static void RegisterTypes(IUnityContainer container)
        {
            container.RegisterType<Himalayan_AngularEntities>();

            //container.RegisterType<ApplicationSignInManager>();
            //container.RegisterType<ApplicationUserManager>();
            //container.RegisterType<IOwinContext>(new InjectionFactory(c => HttpContext.Current.GetOwinContext()));
            //container.RegisterType<IAuthenticationManager>(new InjectionFactory(c => HttpContext.Current.GetOwinContext().Authentication));
            //container.RegisterType<IUserStore<Users, int>, Data.Stores.UserStore<Users>>(
            //    new InjectionConstructor(typeof(Himalayan_AngularEntities)));
            container.RegisterType<IDataContextAsync, Himalayan_AngularEntities>(new PerRequestLifetimeManager()).
              RegisterType<IUnitOfWorkAsync, UnitOfWork>(new PerRequestLifetimeManager()).

              RegisterType<IRepositoryAsync<Brand>, Repository<Brand>>().


              RegisterType<IBrandService, BrandService>();


        }
    }
}