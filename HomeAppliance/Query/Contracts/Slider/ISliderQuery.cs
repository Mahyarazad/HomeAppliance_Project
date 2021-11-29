using System.Collections.Generic;

namespace Query.Contracts
{
    public interface ISliderQuery
    {
        List<SliderQueryModel> GetList();
    }
}