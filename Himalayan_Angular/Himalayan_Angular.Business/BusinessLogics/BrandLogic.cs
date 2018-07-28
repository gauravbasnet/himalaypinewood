using Himalayan_Angular.Business.Services;
using Himalayan_Angular.Data;
using Repository.Pattern.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Himalayan_Angular.Business.BusinessLogics
{
    public class BrandLogic
    {
        public readonly IBrandService brandService;
        
        public BrandLogic(IBrandService _brandService)
        {
            brandService = _brandService;
        }

        public Brand RegisterBrand(Brand brand)
        {
            return brand;
        }
    }
}
