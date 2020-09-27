using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace SuperStore.Domain.Entities
{
    public class Product
    {
        [HiddenInput(DisplayValue = false)]
        public int ProductID { get; set; }

        [Required(ErrorMessage ="Please enter the product name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Please enter the product description")]
        public string Description { get; set; }

        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "Please enter a positive price ")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "Please enter the product category")]
        public string Category { get; set; }

        //Image.ImageData Property
        //Byte[] A byte array containing the image in binary format
        public byte[] ImageData { get; set; }

        //mime types: image/bmp, image/jpeg, image/x-png; image/png, or image/gif
        public string ImageMimeType { get; set; }
    }
}
