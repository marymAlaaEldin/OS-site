using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OSsite.Models
{
    [MetadataType(typeof(ContactMetadata))]
    public partial class ContactU
    {
    }
    public class ContactMetadata
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required]
        public string Message { get; set; }
        public Nullable<bool> readed { get; set; }
    }

}