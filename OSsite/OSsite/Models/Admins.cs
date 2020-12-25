using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OSsite.Models
{
    [MetadataType(typeof(AdminMetadata))]
    public partial class Admin
    {
    }
    public class AdminMetadata
    {
        [Required]
        public string name { get; set; }
        [Required]
        public string password { get; set; }
        public string Type { get; set; }
    }
    public class LoginAdmin
    {
        [Required]
        public string name { get; set; }
        [Required]
        public string password { get; set; }
        public string Type { get; set; }
    }
    public static class Adminloggedin
    {
        public static string name { get; set; }
        public static string password { get; set; }
        public static string type { get; set; }
    }
}