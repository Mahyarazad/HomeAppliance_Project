using System.Collections.Generic;
using _0_Framework;
using _0_Framework.Application;
using DM.Application.Contracts.Colleague;
using DM.Domian;
using DM.Domian.Colleague;

namespace DM.Application
{
    public class ColleagueDiscountApplication : IColleagueDiscountApplication
    {
        private readonly IColleagueDiscountRepository _colleagueDiscountRepository;

        public ColleagueDiscountApplication(IColleagueDiscountRepository colleagueDiscountRepository)
        {
            _colleagueDiscountRepository = colleagueDiscountRepository;
        }

        public List<ColleagueViewModel> Search(ColleagueSearchModel command)
        {
            return _colleagueDiscountRepository.Search(command);
        }

        public OperationResult Create(DefineColleagueDiscount command)
        {
            var result = new OperationResult();
            if (_colleagueDiscountRepository.Exist(x => x.ProductId == command.ProductId && x.DiscountRate == command.DiscountRate))
                return result.Failed(ApplicationMessage.RecordExists);

            var discount = new ColleagueDiscount(command.ProductId, command.DiscountRate, command.StartTime,
                command.EndTime, command.Occasion);

            _colleagueDiscountRepository.Create(discount);
            _colleagueDiscountRepository.SaveChanges();
            return result.Succeeded();
        }

        public OperationResult Edit(EditColleagueDiscount command)
        {
            var result = new OperationResult();
            var discount = _colleagueDiscountRepository.Get(command.Id);
            if (discount == null)
                return result.Failed(ApplicationMessage.RecordNotFound);

            if (_colleagueDiscountRepository.Exist(x => x.Id != command.Id && x.DiscountRate == command.DiscountRate
                                                                           && x.ProductId == command.ProductId))
                return result.Failed(ApplicationMessage.RecordExists);

            discount.Edit(command.ProductId, command.StartTime, command.EndTime
                , command.Occasion, command.DiscountRate);
            _colleagueDiscountRepository.SaveChanges();
            return result.Succeeded();
        }

        public EditColleagueDiscount GetDetails(int Id)
        {
            return _colleagueDiscountRepository.GetDetails(Id);
        }
    }
}
