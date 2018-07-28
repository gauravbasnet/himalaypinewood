//using Himalayan_Angular.Data;
//using Himalayan_Angular.Data.Stores;
//using Microsoft.AspNet.Identity;
//using Microsoft.AspNet.Identity.Owin;
//using Microsoft.Owin;
//using Microsoft.Owin.Security;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
//using System.Web;

//namespace Himalayan_Angular.App_Start
//{
//    public class ApplicationUserStore : UserStore<User>
//    {
//        public ApplicationUserStore()
//           : this(new Himalayan_AngularEntities())
//        {
//            DisposeContext = true;
//        }
//        public ApplicationUserStore(Himalayan_AngularEntities context)
//            : base(context)
//        {

//        }
//    }

//    public class ApplicationUserManager : UserManager<User, string>
//    {
//        public ApplicationUserManager(UserStore<User> store) : base(store)
//        {
//        }
//        public async Task<IdentityResult> CustomChangePassword(User user, string newPassword)
//        {
//            var result = new IdentityResult();
//            using (Himalayan_AngularEntities context = Himalayan_AngularEntities.NewDbContext())
//            using (ApplicationUserManager userManager = new ApplicationUserManager(new ApplicationUserStore(context)))
//            {
//                result = await base.ChangePasswordAsync(user.Id, user.PasswordHash, newPassword);
//                context.SaveChanges();
//            }
//            return result;
//        }
//        public static ApplicationUserManager Create(IdentityFactoryOptions<ApplicationUserManager> options, IOwinContext context)
//        {
//            var dbcontext = context.Get<Himalayan_AngularEntities>();
//            var userstore = new UserStore<User>(dbcontext);
//            var manager = new ApplicationUserManager(userstore);

//            manager.UserValidator = new UserValidator<User>(manager)
//            {
//                AllowOnlyAlphanumericUserNames = false,
//                RequireUniqueEmail = true
//            };
//            manager.PasswordValidator = new PasswordValidator
//            {
//                RequiredLength = 6,
//                RequireNonLetterOrDigit = true,
//                RequireDigit = true,
//                RequireLowercase = true,
//                RequireUppercase = true,
//            };
//            manager.UserLockoutEnabledByDefault = true;
//            manager.DefaultAccountLockoutTimeSpan = TimeSpan.FromMinutes(30);
//            //TimeSpan.FromMinutes(5);
//            manager.MaxFailedAccessAttemptsBeforeLockout = 5;
//            var dataProtectionProvider = options.DataProtectionProvider;
//            if (dataProtectionProvider != null)
//            {
//                manager.UserTokenProvider = new DataProtectorTokenProvider<User>(dataProtectionProvider.Create("ASP.NET Identity"));
//            }
//            return manager;
//        }
//    }

//    public class ApplicationSignInManager : SignInManager<User, string>
//    {
//        public ApplicationSignInManager(ApplicationUserManager userManager, IAuthenticationManager authenticationManager) : base(userManager, authenticationManager)
//        {
//        }

//        public override Task<System.Security.Claims.ClaimsIdentity> CreateUserIdentityAsync(User user)
//        {
//            var ident = user.GenerateUserIdentityAsync((ApplicationUserManager)UserManager, DefaultAuthenticationTypes.ApplicationCookie);
//            return ident;
//        }

//        public static ApplicationSignInManager Create(IdentityFactoryOptions<ApplicationSignInManager> options, IOwinContext context)
//        {
//            return new ApplicationSignInManager(context.GetUserManager<ApplicationUserManager>(), context.Authentication);
//        }

//        // ReSharper disable once RedundantOverridenMember
//        public override Task<SignInStatus> PasswordSignInAsync(string userName, string password, bool isPersistent, bool shouldLockout)
//        {
//            return base.PasswordSignInAsync(userName, password, isPersistent, shouldLockout);
//        }
//    }

//}