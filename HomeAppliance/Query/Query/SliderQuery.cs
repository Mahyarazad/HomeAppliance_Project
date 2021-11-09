using System.Collections.Generic;
using System.Linq;
using Query.Contracts;
using SM.Infrastructure;

namespace Query.Query
{
    public class SliderQuery : ISliderQuery
    {
        private readonly SMContext _smContext;

        public SliderQuery(SMContext smContext)
        {
            _smContext = smContext;
        }


        public List<SliderQueryModel> GetList()
        {
            return _smContext.Slider.Where(x => x.IsDeleted == false)
                .Select(x => new SliderQueryModel
                {
                    BtnText = x.BtnText,
                    Heading = x.Heading,
                    Link = x.Link,
                    Picture = x.Picture,
                    PictureAlt = x.PictureAlt,
                    Title = x.Title,
                    PictureTitle = x.PictureTitle,
                    Text = x.Text,
                })
                .ToList();
        }
    }
}