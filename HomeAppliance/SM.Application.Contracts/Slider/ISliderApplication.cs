using System.Collections.Generic;
using _0_Framework.Application;

namespace SM.Application.Contracts.Slider
{
    public interface ISliderApplication
    {
        OperationResult Create(CreateSlider command);
        OperationResult Edit(EditSlider command);
        OperationResult Delete(int Id);
        OperationResult ReActivate(int Id);
        EditSlider GetDetails(int Id);
        List<SliderViewModel> GetList();

    }
}