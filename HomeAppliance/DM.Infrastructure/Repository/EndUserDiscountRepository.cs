using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using _0_Framework.Infrastructure;
using DM.Application.Contracts;
using DM.Domian;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using SM.Infrastructure;

namespace DM.Infrastructure.Repository
{
    public class EndUserDiscountRepository : RepositoryBase<int, EndUserDiscount>, IEndUserDiscountRepository
    {
        private readonly DMContext _dmContext;
        private readonly SMContext _smContext;

        public EndUserDiscountRepository(DMContext dmContext, SMContext smContext) : base(dmContext)
        {
            _dmContext = dmContext;
            _smContext = smContext;

        }


        public EditEndUserDiscount GetDetails(int Id)
        {
            return _dmContext.EndUserDiscounts.Select(x => new EditEndUserDiscount
            {
                Id = x.Id,
                ProductId = x.ProductId,
                EndTime = x.EndTime,
                Occasion = x.Occasion,
                StartTime = x.StartTime,
                DiscountRate = x.DiscountRate,
                StartTimeString = x.StartTime.ToString("MM-dd-yyyy"),
                EndTimeString = x.EndTime.ToString("MM-dd-yyyy")

            }).FirstOrDefault(x => x.Id == Id);

        }

        public List<EndUserViewModel> Search(EndUserSearchModel search)
        {
            var products = _smContext.Products.Select(x => new { x.Name, x.Id }).ToList();
            var query = _dmContext.EndUserDiscounts.Select(x => new EndUserViewModel
            {
                Id = x.Id,
                ProductId = x.ProductId,
                EndTime = x.EndTime,
                Occasion = x.Occasion,
                StartTime = x.StartTime,
                DiscountRate = x.DiscountRate,
                CreationTime = x.CreationTime.ToString(),

            });



            if (search.ProductId > 0)
                query = query.Where(x => x.ProductId == search.ProductId);

            if (!string.IsNullOrWhiteSpace(search.StartTime) &&
                !string.IsNullOrWhiteSpace(search.EndTime))
            {
                var start = DateTime.Parse(search.StartTime);
                var end = DateTime.Parse(search.EndTime);
                if (start < end)
                {
                    query = query.Where(x => x.EndTime <= end);
                    query = query.Where(x => x.StartTime >= start);
                }
            }
            var DmList = query.ToList();

            DmList
                .ForEach(item => item.ProductName = products
                   .FirstOrDefault(x => x.Id == item.ProductId)?.Name);
            DmList.ForEach(item =>
            {
                item.EndTimeString = item.EndTime.ToString("MM-dd-yyyy");
                item.StartTimeString = item.StartTime.ToString("MM-dd-yyyy");
            });

            return DmList.OrderByDescending(x => x.Id).ToList();
        }
    }
}