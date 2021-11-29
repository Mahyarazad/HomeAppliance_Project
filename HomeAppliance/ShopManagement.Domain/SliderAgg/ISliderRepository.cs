using System.Collections.Generic;
using _0_Framework;
using SM.Application.Contracts.Slider;

namespace SM.Domain.SliderAgg
{
    public interface ISliderRepository : IRepositoty<int, Slider>
    {
        EditSlider GetDetails(int Id);
        List<SliderViewModel> GetViewList();
    }
}