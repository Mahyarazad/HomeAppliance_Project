using System.Collections.Generic;
using _0_Framework;
using DM.Application.Contracts.EndUser;

namespace DM.Domian.EndUserAgg
{
    public interface IEndUserDiscountRepository : IRepository<int, EndUserDiscount>
    {
        EditEndUserDiscount GetDetails(int Id);
        List<EndUserViewModel> Search(EndUserSearchModel search);
    }
}