using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OSsite.Models
{
    [MetadataType(typeof(OrderMetadata))]
    public partial class Order
    {
    }
    public class OrderMetadata
    {
        
        public int OrderID { get; set; }
        [Required]
        public int ProductID { get; set; }
        [Required]
        public int UserID { get; set; }
        public string ProductType { get; set; }
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Not a valid phone number")]
        public Nullable<int> Phone { get; set; }
        public bool readed { get; set; }
        public Nullable<bool> IsValid { get; set; }
        [Required]
        public string Adress { get; set; }
    }
}