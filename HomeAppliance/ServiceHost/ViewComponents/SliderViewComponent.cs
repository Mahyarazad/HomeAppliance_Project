using Microsoft.AspNetCore.Mvc;
using Query.Contracts;

namespace ServiceHost.ViewComponents
{
    public class SliderViewComponent : ViewComponent
    {
        private readonly ISliderQuery _sliderQuery;

        public SliderViewComponent(ISliderQuery sliderQuery)
        {
            _sliderQuery = sliderQuery;
        }

        public IViewComponentResult Invoke()
        {
            var slides = _sliderQuery.GetList();
            return View(slides);
        }
    }
}