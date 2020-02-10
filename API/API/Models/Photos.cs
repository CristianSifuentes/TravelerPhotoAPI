using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models
{
    public class Photos : BaseEntity
    {

        public Photos() : base() { }

        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Name { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime TakenDate { get; private set; }

        public int TripsId { get; set; }
        public virtual Trips Trips { get; set; }

         public virtual ICollection<Comments> Comments { get; set; }
    }
}
