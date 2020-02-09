using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models
{
    public class Trips: BaseEntity
    {
        public Trips() : base() { }

        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime StartDate { get; private set; }
        [DataType(DataType.DateTime)]
        public DateTime EndDate { get; set; }

    }
}
