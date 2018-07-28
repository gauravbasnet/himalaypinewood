using Repository.EF6;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Himalayan_Angular.Data
{
    public partial class Himalayan_AngularEntities:DataContext
    {
        public static Himalayan_AngularEntities NewDbContext()
        {
            return new Himalayan_AngularEntities();
        }
    }
}
