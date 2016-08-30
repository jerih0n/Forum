using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ForumSystem.Models
{
    public class Question
    {
        public Question()
        {
            this.Date = DateTime.Now;
            this.Comments = new List<Comment>();
            this.Tags = new List<Tag>();
            this.Ranking = 0;
        }
        [Key]
        public int QuestionId { get; set; }

        [Required]
        [StringLength(100)]
        [Display(Name = "Topic")]
        public string Title { get; set; }

        [Required]
        [StringLength(3000)]
        [Display(Name = "Your Question")]
        public string Body { get; set; }

        [Required]
        [Display(Name = "Asked on")]
        public DateTime Date { get; set; }

        [Display(Name = "Qustion Author")]
        public ApplicationUser Author { get; set; }
        
        public IList<Comment> Comments { get; set; }

        public IList<Tag> Tags { get; set; }

        public int Ranking { get; set; }
       
    }
}