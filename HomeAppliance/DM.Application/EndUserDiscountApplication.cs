using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using _0_Framework;
using _0_Framework.Application;
using DM.Application.Contracts;
using DM.Domian;

namespace DM.Application
{
    public class EndUserDiscountApplication : IEndUserDiscountApplication
    {
        private readonly IEndUserDiscountRepository _endUserDiscountRepository;

        public EndUserDiscountApplication(IEndUserDiscountRepository endUserDiscountRepository)
        {
            _endUserDiscountRepository = endUserDiscountRepository;
        }

        public List<EndUserViewModel> Search(EndUserSearchModel command)
        {
            return _endUserDiscountRepository.Search(command);
        }

        public OperationResult Create(DefineEndUserDiscount command)
        {
            var result = new OperationResult();
            if (_endUserDiscountRepository.Exist(x => x.ProductId == command.ProductId && x.DiscountRate == command.DiscountRate))
                return result.Failed(ApplicationMessage.RecordExists);

            var discount = new EndUserDiscount(command.ProductId, command.DiscountRate, command.StartTime,
                command.EndTime, command.Occasion);

            _endUserDiscountRepository.Create(discount);
            _endUserDiscountRepository.SaveChanges();
            return result.Succeeded();
        }

        public OperationResult Edit(EditEndUserDiscount command)
        {
            var result = new OperationResult();
            var discount = _endUserDiscountRepository.Get(command.Id);
            if (discount == null)
                return result.Failed(ApplicationMessage.RecordNotFound);

            if (_endUserDiscountRepository.Exist(x => x.Id != command.Id && x.DiscountRate == command.DiscountRate
             && x.ProductId == command.ProductId))
                return result.Failed(ApplicationMessage.RecordExists);

            discount.Edit(command.ProductId, command.StartTime, command.EndTime
                , command.Occasion, command.DiscountRate);
            _endUserDiscountRepository.SaveChanges();
            return result.Succeeded();
        }

        public EditEndUserDiscount GetDetails(int Id)
        {
            return _endUserDiscountRepository.GetDetails(Id);
        }
    }
}
