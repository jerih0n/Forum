using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ForumSystem.Models
{
    public class Tag
    {
        public Tag()
        {
            this.Questions = new List<Question>();
        }
        [Key]
        public int TagId { get; set; }

        [StringLength(50)]
        [Display(Name = "Tag")]
        public string TagText { get; set; }
        
        public IList<Question> Questions { get; set; }
        public int QuestionId { get; set; }
    }
}