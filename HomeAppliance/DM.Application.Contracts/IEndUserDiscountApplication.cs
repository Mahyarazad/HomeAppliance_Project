using System.Collections.Generic;
using _0_Framework.Application;

namespace DM.Application.Contracts
{
    public interface IEndUserDiscountApplication
    {
        List<EndUserViewModel> Search(EndUserSearchModel command);
        OperationResult Create(DefineEndUserDiscount command);
        OperationResult Edit(EditEndUserDiscount command);
        EditEndUserDiscount GetDetails(int Id);
    }
}