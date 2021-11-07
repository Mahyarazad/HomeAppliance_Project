using System.Collections.Generic;
using _0_Framework;
using _0_Framework.Application;
using SM.Application.Contracts.Slider;
using SM.Domain.SliderAgg;

namespace SM.Application
{
    public class SliderApplication : ISliderApplication
    {
        private readonly ISliderRepository _sliderRepository;

        public SliderApplication(ISliderRepository sliderRepository)
        {
            _sliderRepository = sliderRepository;
        }

        public OperationResult Create(CreateSlider command)
        {
            var operation = new OperationResult();
            var slider = new Slider(command.Picture, command.PictureAlt, command.PictureTitle,
                command.Title, command.Heading, command.Text, command.BtnText);
            _sliderRepository.Create(slider);
            _sliderRepository.SaveChanges();
            return operation.Succeeded();
        }

        public OperationResult Edit(EditSlider command)
        {
            var operation = new OperationResult();
            var slider = _sliderRepository.Get(command.Id);
            if (_sliderRepository.Exist(x => x.Title == command.Title && x.Id != command.Id))
                return operation.Failed(ApplicationMessage.RecordExists);
            if (slider == null)
                return operation.Failed(ApplicationMessage.RecordNotFound);
            slider.Edit(command.Picture, command.PictureAlt, command.PictureTitle,
                command.Title, command.Heading, command.Text, command.BtnText);
            _sliderRepository.SaveChanges();
            return operation.Succeeded();
        }

        public OperationResult Delete(int Id)
        {
            var operation = new OperationResult();
            var slider = _sliderRepository.Get(Id);
            if (slider == null)
                return operation.Failed(ApplicationMessage.RecordNotFound);
            slider.Delete();
            _sliderRepository.SaveChanges();
            return operation.Succeeded();
        }

        public OperationResult ReActivate(int Id)
        {
            var operation = new OperationResult();
            var slider = _sliderRepository.Get(Id);
            if (slider == null)
                return operation.Failed(ApplicationMessage.RecordNotFound);
            slider.ReActivate();
            _sliderRepository.SaveChanges();
            return operation.Succeeded();
        }

        public EditSlider GetDetails(int Id)
        {
            return _sliderRepository.GetDetails(Id);
        }

        public List<SliderViewModel> CustomGetList()
        {
            return _sliderRepository.GetList();
        }

        public List<SliderViewModel> GetList()
        {
            return _sliderRepository.GetList();
        }
    }
}