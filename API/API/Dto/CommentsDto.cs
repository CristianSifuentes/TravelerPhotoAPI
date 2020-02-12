using API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Dto
{
    public class CommentsDto : BaseEntityDto
    { 

        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }

        public DateTime CommentDate { get; set; }

        public int PhotosId { get; set; }
        public virtual PhotosDto Photos { get; set; }
    }
}
