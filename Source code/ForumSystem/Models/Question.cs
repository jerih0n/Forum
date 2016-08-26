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
        [Display(Name ="Title")]
        public string QuestionTitle { get; set;}
        [Required]
        [StringLength(1000)]
        [Display(Name ="Question")]
        public string QuestionBody { get; set; }
        public DateTime Date { get; set; }
        [Display(Name ="Asked By")]
        public ApplicationUser Author { get; set;}

    }
}