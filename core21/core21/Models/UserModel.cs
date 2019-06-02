using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace core21.Models
{
        public class UserModel
        {
            [Key]
            public string fID { get; set; }

            [Display(Name = "User Name")]
            [Required(ErrorMessage = "This field is required")]
            public string fUsername { get; set; }

            [Required(ErrorMessage = "This field is required")]
            [Display(Name = "User Password")]
            [DataType(DataType.Password)]
            public string fPassword { get; set; }

            [Display(Name = "User Birthday")]
            public string fDate_of_birth { get; set; }

            [Display(Name = "User Image")]
            public string fImage_profile { get; set; }

            [Display(Name = "Coin")]
            public int fCoin { get; set; }

            [Display(Name = "Level")]
            public int fLevel { get; set; }

            [Display(Name = "Experience")]
            public int fExperience { get; set; }

            [Display(Name = "User Type")]
            public string fType { get; set; }

            [Display(Name = "Power")]
            public int fPower { get; set; }

            [Display(Name = "User Nickname")]
            public string fNick_name { get; set; }
        }
}