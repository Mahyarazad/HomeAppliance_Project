using _0_Framework.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Query.Contracts;
using Query.Contracts.Product;
using Query.Contracts.ProductCategory;
using Query.Query;
using ShopManagement.Domain;
using SM.Application;
using SM.Application.Contracts;
using SM.Application.Contracts.Comment;
using SM.Application.Contracts.Order;
using SM.Application.Contracts.Product;
using SM.Application.Contracts.ProductPicture;
using SM.Application.Contracts.Slider;
using SM.Domain.CommentAgg;
using SM.Domain.OrderAgg;
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

            services.AddTransient<ICommentRepository, CommentRepository>();
            services.AddTransient<ICommentApplication, CommentApplication>();

            services.AddTransient<IOrderRepository, OrderRepository>();
            services.AddTransient<IOrderApplication, OrderApplication>();

            services.AddTransient<ISliderQuery, SliderQuery>();
            services.AddTransient<IProductCategoryQuery, ProductCategoryQuery>();
            services.AddTransient<IProductQuery, ProductQuery>();

            services.AddTransient<IPermissionExposer, ShopPermissionExposer>();


            services.AddDbContext<SMContext>(x => x.UseSqlServer(connectionString));
        }
    }
}
