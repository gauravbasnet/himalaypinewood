using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.ModelBinding;
using System.Web.Http.OData;
using System.Web.Http.OData.Routing;
using Himalayan_Angular.Business.BusinessLogics;
using Himalayan_Angular.Business.Services;
using Himalayan_Angular.Data;
using Repository.Pattern.UnitOfWork;

namespace Himalayan_Angular.Controllers
{
    /*
    The WebApiConfig class may require additional changes to add a route for this controller. Merge these statements into the Register method of the WebApiConfig class as applicable. Note that OData URLs are case sensitive.

    using System.Web.Http.OData.Builder;
    using System.Web.Http.OData.Extensions;
    using Himalayan_Angular.Data;
    ODataConventionModelBuilder builder = new ODataConventionModelBuilder();
    builder.EntitySet<Brand>("Brands");
    builder.EntitySet<ProductMain>("ProductMains"); 
    config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
    */
    public class BrandsController : ODataController
    {
        private readonly IUnitOfWorkAsync unitOfWorkAsync;
        private readonly IBrandService brandService;
        private readonly BrandLogic brandLogic;

        public BrandsController() { }

        public BrandsController(IUnitOfWorkAsync _unitOfWorkAsync, IBrandService _brandService)
        {
            unitOfWorkAsync = _unitOfWorkAsync;
            brandService = _brandService;
            brandLogic = new BrandLogic(_brandService);
        }

        [HttpGet]
        [EnableQuery(MaxExpansionDepth = 4)]
        public IQueryable<Brand> Get()
        {
            var brand = brandService.Queryable();
            return brand;
        }

        [EnableQuery(MaxExpansionDepth = 4)]
        public SingleResult<Brand> Get([FromODataUri] int key)
        {
            return SingleResult.Create(brandService.Queryable().Where(x => x.BrandId == key));
        }

        public async Task<IHttpActionResult> Post(Brand brand)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                brandLogic.RegisterBrand(brand);
            }
            catch(Exception ex)
            {
                throw ex;
            }
            try
            {
                await unitOfWorkAsync.SaveChangesAsync();
            }catch(Exception ex)
            {
                throw ex;
            }
            return Created(brand);
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                unitOfWorkAsync.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
