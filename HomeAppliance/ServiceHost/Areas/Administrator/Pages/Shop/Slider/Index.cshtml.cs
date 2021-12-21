using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using SM.Application.Contracts.Product;
using SM.Application.Contracts.ProductPicture;
using SM.Application.Contracts.Slider;

namespace ServiceHost.Areas.Administrator.Pages.Shop.Slider
{
    public class IndexModel : PageModel
    {
        public IndexModel(ISliderApplication sliderApplication)
        {
            _sliderApplication = sliderApplication;
        }

        [TempData]
        public string Message { get; set; }
        public List<SliderViewModel> Slider { get; set; }
        public SelectList ProductList { get; set; }
        private readonly ISliderApplication _sliderApplication;


        public void OnGet()
        {
            Slider = _sliderApplication.GetList();
            @ViewData["title"] = "Manage Slider";
        }

        public IActionResult OnGetCreate()
        {
            var command = new CreateSlider();
            @ViewData["title"] = "Add a new slider";
            return Partial("./Create", command);
        }

        public JsonResult OnPostCreate(CreateSlider command)
        {
            var result = _sliderApplication.Create(command);
            return new JsonResult(result);

        }

        public IActionResult OnGetEdit(int id)
        {
            @ViewData["title"] = "Slider Management";
            var slider = _sliderApplication.GetDetails(id);
            return Partial("./Edit", slider);
        }

        public JsonResult OnPostEdit(EditSlider command)
        {
            var result = _sliderApplication.Edit(command);
            return new JsonResult(result);
        }
        public IActionResult OnGetDelete(int Id)
        {
            var result = _sliderApplication.Delete(Id);
            if (result.IsSucceeded)
            {
                Message = result.Message;
                return RedirectToPage("./Index");
            }
            else
            {
                Message = result.Message;
                return RedirectToPage("./Index");
            }
        }
        public IActionResult OnGetReactivate(int Id)
        {
            var result = _sliderApplication.ReActivate(Id);
            if (result.IsSucceeded)
            {
                Message = result.Message;
                return RedirectToPage("./Index");
            }
            else
            {
                Message = result.Message;
                return RedirectToPage("./Index");
            }
        }
    }
}
