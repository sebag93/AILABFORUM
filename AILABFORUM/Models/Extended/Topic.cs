using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AILABFORUM.Models
{
    [MetadataType(typeof(TopicMetadata))]
    public partial class Topic
    {

    }

    public class TopicMetadata
    {
        [Display(Name = "Nazwa tematu")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Nazwa tematu nie może być pusta.")]
        public string tytul { get; set; }

        [Display(Name = "Treść wpisu")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Treść nie może być pusta.")]
        public string tresc { get; set; }

        public string autor { get; set; }
        public DateTime data_dodania { get; set; }
    }
}