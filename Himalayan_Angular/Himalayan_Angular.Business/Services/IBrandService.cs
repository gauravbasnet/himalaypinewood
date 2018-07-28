using Himalayan_Angular.Data;
using Repository.Pattern.Repositories;
using Service.Pattern;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Himalayan_Angular.Business.Services
{
    public interface IBrandService:IService<Brand>
    {

    }

    public class BrandService: Service<Brand>, IBrandService
    {
        public BrandService(IRepositoryAsync<Brand> repository) : base(repository) { }
    }
}
