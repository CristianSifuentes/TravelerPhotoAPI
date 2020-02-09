using System;
using System.ComponentModel.DataAnnotations;

namespace API.Models
{
    public abstract class BaseEntity
    {
        protected BaseEntity()
        {
            CreationDate = DateTime.Now;
            ModificationDate = DateTime.Now;
            Actived = true;
        }
        [DataType(DataType.DateTime)]
        public DateTime CreationDate { get; private set; }
        [DataType(DataType.DateTime)]
        public DateTime? ModificationDate { get; set; }
        public bool Actived { get; set; }
    }
}

