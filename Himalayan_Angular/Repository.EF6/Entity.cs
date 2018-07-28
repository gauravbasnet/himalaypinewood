using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repository.Pattern.Infrastructure;
using System.ComponentModel.DataAnnotations.Schema;

namespace Repository.EF6
{
    public abstract class Entity: IObjectState
    {
        [NotMapped]
        public ObjectState ObjectState { get; set; }
    }

    
}
