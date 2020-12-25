using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OSsite.Models
{
    public class Laptop
    {
        public int ID { get; set; }
        [Required]
        public string model { get; set; }
        [Required]
        public string Details { get; set; }
        [Required]
        public Nullable<int> Price { get; set; }
        [Required]
        public Nullable<int> sale { get; set; }
        [Required]
        public HttpPostedFileBase file { get; set; }
        public byte[] img { get; set; }
        [Required]
        public string Brand { get; set; }
        public Nullable<int> Rate { get; set; }
        [Required]
        public Nullable<int> PicesNO { get; set; }
    }

    public class AddPhone
    {
        public int ID { get; set; }
        [Required]
        public string model { get; set; }
        [Required]
        public string Details { get; set; }
        [Required]
        public Nullable<int> Price { get; set; }
        [Required]
        public Nullable<int> sale { get; set; }
        [Required]
        public HttpPostedFileBase file { get; set; }
        public byte[] img { get; set; }
        [Required]
        public string Brand { get; set; }
        public Nullable<int> Rate { get; set; }
        [Required]
        public Nullable<int> PicesNO { get; set; }
    }

    public class AddAccessory
    {
        public int ID { get; set; }
        [Required]
        public string model { get; set; }
        [Required]
        public string Details { get; set; }
        [Required]
        public Nullable<int> Price { get; set; }
        [Required]
        public Nullable<int> sale { get; set; }
        [Required]
        public HttpPostedFileBase file { get; set; }
        public byte[] img { get; set; }
        [Required]
        public string Brand { get; set; }
        public Nullable<int> Rate { get; set; }
        [Required]
        public Nullable<int> PicesNO { get; set; }
    }

    public class AddClothes
    {
        public int productID { get; set; }
        [Required]
        public Nullable<int> Size { get; set; }
        [Required]
        public string color { get; set; }
        [Required]
        public string name { get; set; }
        [Required]
        public Nullable<int> Price { get; set; }
        [Required]
        public HttpPostedFileBase file { get; set; }
        public byte[] img { get; set; }
        [Required]
        public string Brand { get; set; }
        [Required]
        public Nullable<int> sale { get; set; }
        public Nullable<int> Rate { get; set; }
    }
}    