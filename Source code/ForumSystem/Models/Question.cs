using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ForumSystem.Models
{
    public class Question
    {
        public Question ()
        {
            this.Date = DateTime.Now;
        }

        [Key]
        public int QuestionId { get; set; }

        [Required]
        [StringLength(100)]
        public string QuestionTitle { get; set;}
        [Required]
        [StringLength(1000)]
        public string QuestionBody { get; set; }

        public DateTime Date { get; set; }
        
        public ApplicationUser Author { get; set;}

    }
}