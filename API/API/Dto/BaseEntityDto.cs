using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Dto
{
    public abstract class BaseEntityDto
    {
        protected BaseEntityDto()
        {
            CreationDate = DateTime.Now;
            ModificationDate = DateTime.Now;
            Actived = true;
        }
        public DateTime CreationDate { get; private set; }
        public DateTime? ModificationDate { get; set; }
        public bool Actived { get; set; }
    }
}
