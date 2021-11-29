using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using _0_Framework;
using _0_Framework.Application;
using Microsoft.AspNetCore.Http;

namespace SM.Application.Contracts.Product
{
    public class CreateProduct
    {
        public List<ProductCategoryViewModel> Categories { get; set; }
        [Required(ErrorMessage = ValidationMessages.IsRequired)]
        public string Name { get; set; }
        [Required(ErrorMessage = ValidationMessages.IsRequired)]
        public string Code { get; set; }
        [Range(1, 1000000000, ErrorMessage = ValidationMessages.IsRequired)]
        public double UnitPrice { get; set; }
        public string ShortDescription { get; set; }
        public string Description { get; set; }
        [FileExtensionLimit(new string[] { ".jpeg", ".jpg", ".png" }, ErrorMessage = ValidationMessages.InvalidFileFormat)]
        [Required(ErrorMessage = ValidationMessages.IsRequired)]
        [MaxFileSize(3 * 1024 * 1024, ErrorMessage = ValidationMessages.SizeError2M)]
        public IFormFile Picture { get; set; }
        public string PictureString { get; set; }
        public string PictureAlt { get; set; }
        public string PictureTitle { get; set; }
        public int CategoryId { get; set; }
        [Required(ErrorMessage = ValidationMessages.IsRequired)]
        public string Slug { get; set; }
        [Required(ErrorMessage = ValidationMessages.IsRequired)]
        public string MetaDescription { get; set; }
        [Required(ErrorMessage = ValidationMessages.IsRequired)]
        public string Keyword { get; set; }

        public string CategoryName { get; set; }
    }
}