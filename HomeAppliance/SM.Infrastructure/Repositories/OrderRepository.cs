using _0_Framework.Infrastructure;
using SM.Domain.OrderAgg;

namespace SM.Infrastructure.Repositories
{
    public class OrderRepository : RepositoryBase<long, Order>, IOrderRepository
    {
        private readonly SMContext _smContext;
        public OrderRepository(SMContext smContext) : base(smContext)
        {
            _smContext = smContext;
        }
    }
}