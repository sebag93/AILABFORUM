using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AILABFORUM.Models
{
    [MetadataType(typeof(UserMetadata))]
    public partial class User
    {
        public string powtorzhaslo { get; set; }
        public string powtorznowehaslo { get; set; }
        public string nowehaslo { get; set; }
    }

    public class UserMetadata
    {
        [Display(Name = "Nazwa użytkownika")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Wprowadź nazwę użytkownika")]
        public string login { get; set; }

        [Display(Name="Imię")]
        [Required(AllowEmptyStrings =false, ErrorMessage ="Wprowadź imię")]
        public string imie { get; set; }

        [Display(Name ="Data urodzenia")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode =true, DataFormatString ="{0:MM/dd/yyyy}")]
        public DateTime data_urodzenia { get; set; }

        [Display(Name ="Miejscowość")]
        public string miejscowosc { get; set; }

        [Display(Name ="Hasło")]
        [Required(AllowEmptyStrings =false, ErrorMessage ="Wprowadź hasło")]
        [DataType(DataType.Password)]
        [MinLength(6,ErrorMessage = "Hasło musi zawierać co najmniej 6 znaków")]
        public string haslo { get; set; }

        [Display(Name = "Powtórz hasło")]
        [DataType(DataType.Password)]
        [Compare("haslo",ErrorMessage ="Wprowadź poprawne hasło")]
        public string powtorzhaslo { get; set; }

        [Display(Name ="Adres email")]
        [Required(AllowEmptyStrings =false,ErrorMessage ="Wprowadź adres email")]
        [RegularExpression(@"^([\w-\.]+)@((\[[0-9]{1,3]\.)|(([\w-]+\.)+))([a-zA-Z{2,4}|[0-9]{1,3})(\]?)$", ErrorMessage = "Wprowadź prawidłowy adres email")]
        public string email { get; set; }

        [Display(Name = "Nowe hasło")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Wprowadź nowe hasło")]
        [DataType(DataType.Password)]
        [MinLength(6, ErrorMessage = "Hasło musi zawierać co najmniej 6 znaków")]
        public string nowehaslo { get; set; }

        [Display(Name = "Powtórz nowe hasło")]
        [DataType(DataType.Password)]
        [Compare("nowehaslo", ErrorMessage = "Wprowadź poprawne hasło")]
        public string powtorznowehaslo { get; set; }
    }
}