using System.Collections.Generic;
using _0_Framework;
using DM.Application.Contracts.Colleague;

namespace DM.Domian.Colleague
{
    public interface IColleagueDiscountRepository : IRepository<int, ColleagueDiscount>
    {
        EditColleagueDiscount GetDetails(int Id);
        List<ColleagueViewModel> Search(ColleagueSearchModel search);
    }
}