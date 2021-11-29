using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Query.Contracts;
using Query.Contracts.Product;
using Query.Contracts.ProductCategory;
using Query.Query;
using ShopManagement.Domain;
using SM.Application;
using SM.Application.Contracts;
using SM.Application.Contracts.Product;
using SM.Application.Contracts.ProductPicture;
using SM.Application.Contracts.Slider;
using SM.Domain.ProductAgg;
using SM.Domain.ProductPictureAgg;
using SM.Domain.SliderAgg;
using SM.Infrastructure.Repositories;

namespace SM.Infrastructure.Core
{
    public class ShopManagementConfiguration
    {
        public static void Config(IServiceCollection services, string connectionString)
        {
            services.AddTransient<IProductCategoryApplication, ProductCategoryApplication>();
            services.AddTransient<IProductCategoryRepository, ProductCategoryRepository>();

            services.AddTransient<IProductRepository, ProductRepository>();
            services.AddTransient<IProductApplication, ProductApplication>();

            services.AddTransient<IProductPictureRepository, ProductPictureRepository>();
            services.AddTransient<IProductPictureApplication, ProductPictureApplication>();

            services.AddTransient<ISliderRepository, SliderRepository>();
            services.AddTransient<ISliderApplication, SliderApplication>();

            services.AddTransient<ISliderQuery, SliderQuery>();
            services.AddTransient<IProductCategoryQuery, ProductCategoryQuery>();
            services.AddTransient<IProductQuery, ProductQuery>();

            services.AddDbContext<SMContext>(x => x.UseSqlServer(connectionString));
        }
    }
}
