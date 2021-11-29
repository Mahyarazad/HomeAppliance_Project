using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using _0_Framework;
using _0_Framework.Application;
using Microsoft.AspNetCore.Http;
using SM.Application.Contracts.Product;

namespace SM.Application.Contracts.ProductPicture
{
    public class CreateProductPicture
    {
        [Range(1, 100000, ErrorMessage = ValidationMessages.IsRequired)]
        public int ProductId { get; set; }
        [Required(ErrorMessage = ValidationMessages.IsRequired)]
        [FileExtensionLimit(new string[] { ".jpeg", ".jpg", ".png" }, ErrorMessage = ValidationMessages.InvalidFileFormat)]
        [MaxFileSize(2 * 1024 * 1024, ErrorMessage = ValidationMessages.SizeError2M)]
        public IFormFile Picture { get; set; }
        public string PictureAlt { get; set; }
        public string PictureTitle { get; set; }
        public List<ProductViewModel> Products { get; set; }
    }
}