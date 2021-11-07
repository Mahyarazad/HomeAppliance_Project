using System.Collections.Generic;
using System.Linq;
using _0_Framework.Infrastructure;
using Microsoft.EntityFrameworkCore;
using SM.Application.Contracts.Slider;
using SM.Domain.SliderAgg;

namespace SM.Infrastructure.Repositories
{
    public class SliderRepository : RepositoryBase<int, Slider>, ISliderRepository
    {
        private readonly SMContext _smContext;

        public SliderRepository(SMContext smContext) : base(smContext)
        {
            _smContext = smContext;
        }

        public EditSlider GetDetails(int id)
        {
            var query = _smContext.Slider.Select(x => new EditSlider
            {
                Title = x.Title,
                Heading = x.Heading,
                Picture = x.Picture,
                Id = x.Id,
                BtnText = x.BtnText,
                PictureAlt = x.PictureAlt,
                PictureTitle = x.PictureTitle,
                Text = x.Text
            }).FirstOrDefault(x => x.Id == id);
            return query;
        }

        public List<SliderViewModel> GetList()
        {
            return _smContext.Slider.Select(x => new SliderViewModel
            {
                Title = x.Title,
                Heading = x.Heading,
                Picture = x.Picture,
                Id = x.Id,
                IsDeleted = x.IsDeleted
            }).OrderByDescending(x => x.Id).ToList();
        }
    }
}