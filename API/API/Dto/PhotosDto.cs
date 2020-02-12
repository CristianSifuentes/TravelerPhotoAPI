using API.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace API.Dto
{
    public class PhotosDto : BaseEntityDto
    {


        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public DateTime TakenDate { get; set; }

        public int TripsId { get; set; }
        public virtual TripsDto Trips { get; set; }

        public virtual ICollection<CommentsDto> Comments { get; set; }
    }
}
