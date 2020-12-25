using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OSsite.Models
{
    [MetadataType(typeof(UserMetadata))]
    public partial class User
    {
    }
    public class UserMetadata
    {
        [Required]
        public string Name { get; set; }
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Not a valid phone number")]
        public int PhoneNumber { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        [Compare(nameof(Password), ErrorMessage = "password mismatch")]
        public string ConfirmPassword { get; set; }
        public string Adress { get; set; }

    }
    public class SIGNIN
    {
        [Required]
        public string Name { get; set; }
        public int ID { get; set; }
        [Required]
        public string Password { get; set; }
        public int rate { get; set; }
        public string feedbackMessage { get; set; }
        [Required]
        public string Email { get; set; }
        public Nullable<decimal> phoneNumber { get; set; }
        public bool founded { get; set; }

    }
    public static class userloggedin
    {
        public static int ID;
        public static string Name;
        public static string Password;
        public static string Email;
        public static Nullable<decimal> phoneNumber;
    }
}