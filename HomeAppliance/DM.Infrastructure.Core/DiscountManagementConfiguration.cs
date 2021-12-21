using DM.Application;
using DM.Application.Contracts.EndUser;
using DM.Application.Contracts.Colleague;
using DM.Domian;
using DM.Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using DM.Domian.EndUserAgg;
using DM.Domian.Colleague;

namespace DM.Infrastructure.Core
{
    public class DiscountManagementConfiguration
    {
        public static void Config(IServiceCollection services, string connectionString)
        {
            services.AddTransient<IEndUserDiscountRepository, EndUserDiscountRepository>();
            services.AddTransient<IEndUserDiscountApplication, EndUserDiscountApplication>();

            services.AddTransient<IColleagueDiscountRepository, ColleagueDiscountRepository>();
            services.AddTransient<IColleagueDiscountApplication, ColleagueDiscountApplication>();

            services.AddDbContext<DMContext>(x => x.UseSqlServer(connectionString));
        }
    }
}
