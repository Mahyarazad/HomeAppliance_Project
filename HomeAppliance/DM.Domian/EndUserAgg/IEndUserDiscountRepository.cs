using System.Collections.Generic;
using _0_Framework;
using DM.Application.Contracts;

namespace DM.Domian
{
    public interface IEndUserDiscountRepository : IRepositoty<int, EndUserDiscount>
    {
        EditEndUserDiscount GetDetails(int Id);
        List<EndUserViewModel> Search(EndUserSearchModel search);
    }
}