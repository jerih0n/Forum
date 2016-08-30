using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ForumSystem.Models
{
    public class Comment
    {
        public Comment()
        {
            this.Date = DateTime.Now;
        }
        [Key]
        public int CommnetId { get; set; }

        [Required]
        [StringLength(2000)]
        [Display(Name ="Comment")]
        public string CommentText { get; set;}

        [Required]
        [Display(Name ="Commented on")]
        public DateTime Date { get; set; }

        public int Rating { get; set;}
        public Question Qustion { get; set; }
        public int QuestionId { get; set; }
        public ApplicationUser Author { get; set; }

    }
}