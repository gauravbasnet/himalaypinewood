using Himalayan_Angular.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Http.OData.Builder;
using System.Web.Http.OData.Extensions;

namespace Himalayan_Angular.App_Start
{
    public class ODataConfig
    {
        public static void Register(HttpConfiguration config)
        {
            ODataModelBuilder builder = new ODataConventionModelBuilder();
            builder.EntitySet<Brand>("Brands");
            builder.EntitySet<Enquiry>("Enquirys");
            builder.EntitySet<User>("Users");
            builder.EntitySet<ProductMain>("ProductMains");
            builder.EntitySet<ProductType>("ProductTypes");
            builder.EntitySet<Clients>("Clients");
            builder.EntitySet<Claim>("Claims");
            builder.EntitySet<Login>("Logins");
            builder.EntitySet<Role>("Roles");
            builder.EntitySet<RoleEntity>("RoleEntitys");
            

            var model = builder.GetEdmModel();
            config.Routes.MapODataServiceRoute("odata", "odata", model);
            config.AddODataQueryFilter();


        }
    }
}