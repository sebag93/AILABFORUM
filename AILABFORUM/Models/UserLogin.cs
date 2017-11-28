using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace AILABFORUM.Models
{
    public class UserLogin
    {
        [Display(Name = "Nazwa użytkownika")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Wprowadź nazwę użytkownika")]
        public string login { get; set; }

        [Display(Name ="Hasło")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Wprowadź hasło")]
        [DataType(DataType.Password)]
        public string haslo { get; set; }

        [Display(Name ="Zapamiętaj")]
        public bool zapamietaj { get; set; }
    }
}