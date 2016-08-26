using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ForumSystem.Models
{
    public class Question
    {
        [Key]
        public int QuestionId { get; set; }

        [Required]
        [StringLength(100)]
        public string QuestionTitle { get; set;}
        [Required]
        [StringLength(1000)]
        public string QuestionBody { get; set; }

        public DateTime Date { get { return DateTime.Now.Date; } }
        
        public ApplicationUser Author { get; set;}

    }
}