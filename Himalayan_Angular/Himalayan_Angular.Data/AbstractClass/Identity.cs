//using Microsoft.AspNet.Identity.EntityFramework;
//using Repository.Pattern.Infrastructure;
//using Himalayan_Angular.Data;
//using System;
//using System.Collections.Generic;
//using System.ComponentModel.DataAnnotations.Schema;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace Himalayan_Angular.Data.AbstractClasses
//{
//    public abstract class IdentityUserEntity : IdentityUser<string, Login, Role, Claim>, IObjectState
//    {
//        [NotMapped]
//        public ObjectState ObjectState { get; set; }
//    }

//    public abstract class IdentityUserLoginEntity : IdentityUserLogin<string>, IObjectState
//    {
//        [NotMapped]
//        public ObjectState ObjectState { get; set; }
//    }

//    public abstract class IdentityUserRoleEntity : IdentityUserRole, IObjectState
//    {
//        [NotMapped]
//        public ObjectState ObjectState { get; set; }
//    }
//    public abstract class IdentityUserClaimEntity : IdentityUserClaim<string>, IObjectState
//    {
//        [NotMapped]
//        public ObjectState ObjectState { get; set; }
//    }
//    public abstract class IdentityRoleEntity : IdentityRole<string, Role>, IObjectState
//    {
//        [NotMapped]
//        public ObjectState ObjectState { get; set; }
//    }
//}
