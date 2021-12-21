using System.Collections.Generic;
using _0_Framework.Application;

namespace DM.Application.Contracts.Colleague
{
    public interface IColleagueDiscountApplication
    {
        List<ColleagueViewModel> Search(ColleagueSearchModel command);
        OperationResult Create(DefineColleagueDiscount command);
        OperationResult Edit(EditColleagueDiscount command);
        EditColleagueDiscount GetDetails(int Id);
    }
}