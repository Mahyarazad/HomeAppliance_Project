using System.ComponentModel.DataAnnotations;
using _0_Framework;
using _0_Framework.Application;
using Microsoft.AspNetCore.Http;

namespace SM.Application.Contracts.Slider
{
    public class CreateSlider
    {
        [Required(ErrorMessage = ValidationMessages.IsRequired)]
        [FileExtensionLimit(new string[] { ".jpeg", ".jpg", ".png" }, ErrorMessage = ValidationMessages.InvalidFileFormat)]
        [MaxFileSize(2 * 1024 * 1024, ErrorMessage = ValidationMessages.SizeError2M)]
        public IFormFile Picture { get; set; }
        public string PictureAlt { get; set; }
        public string PictureTitle { get; set; }
        public string Heading { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        public string BtnText { get; set; }
        public string Link { get; set; }
    }
}