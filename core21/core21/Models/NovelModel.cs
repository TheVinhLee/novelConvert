using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace core21.Models
{
    [Table("Novels")]
    public class NovelModel
    {
        [Display(Name = "Novel Id")]
        [Key]
        public string Id { get; set;  }

        [StringLength(100)]
        [Required(ErrorMessage = "This field is required")]
        public string Name { get; set; }

        [StringLength(255)]
        [Required(ErrorMessage = "This field is required")]
        public string Link { get; set; }

        public string NovelLink { get; set; }

        [StringLength(100)]
        [Required(ErrorMessage = "This field is required")]
        public string Author { get; set; }
        
        public int Chap_number { get; set; }
        public int Rating { get; set; }
        public int Viewer { get; set; }
        public int Voting { get; set; }
        public int Recommandation { get; set; }
        public string Image_link { get; set; }

        public string Image_Link_Get { get; set; }

        public string Owner { get; set; }

        public DateTime upload_date { get; set; }
    }
}