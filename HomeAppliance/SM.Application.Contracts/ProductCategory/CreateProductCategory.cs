using System;
using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;
using _0_Framework;
using _0_Framework.Application;
using Microsoft.AspNetCore.Http;

namespace SM.Application.Contracts
{
    public class CreateProductCategory
    {
        [Required(ErrorMessage = ValidationMessages.IsRequired)]
        public string Name { get; set; }
        public string Description { get; set; }
        [FileExtensionLimit(new string[] { ".jpeg", ".jpg", ".png" }, ErrorMessage = ValidationMessages.InvalidFileFormat)]
        [Required(ErrorMessage = ValidationMessages.IsRequired)]
        [MaxFileSize(2 * 1024 * 1024, ErrorMessage = ValidationMessages.SizeError2M)]
        public IFormFile Picture { get; set; }
        public string PictureAlt { get; set; }
        public string PictureTitle { get; set; }

        [Required(ErrorMessage = ValidationMessages.IsRequired)]
        public string Keyword { get; set; }

        [Required(ErrorMessage = ValidationMessages.IsRequired)]
        public string MetaDescription { get; set; }

        [Required(ErrorMessage = ValidationMessages.IsRequired)]
        public string Slug { get; set; }
    }
}