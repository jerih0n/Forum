using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ForumSystem.Models
{
    public class Tag
    {
        [Key]
        public int TagId { get; set; }

        [StringLength(50)]
        [Display(Name = "Tag")]
        public string TagText { get; set; }
        
        public Question Question { get; set; }
        public int QuestionId { get; set; }
    }
}