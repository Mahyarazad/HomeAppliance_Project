using System;
using System.Collections.Generic;
using _0_Framework.Application;
using ShopManagement.Domain;
using SM.Application.Contracts;

namespace SM.Application
{
    public class ProductCategoryApplication : IProductCategoryApplication
    {
        private readonly IProductCategoryRepository _productCategoryRepository;

        public ProductCategoryApplication(IProductCategoryRepository productCategoryRepository)
        {
            _productCategoryRepository = productCategoryRepository;
        }

        public OperationResult Create(CreateProductCategory command)
        {
            var operation = new OperationResult();
            if (_productCategoryRepository.Exist(x => x.Name == command.Name))
                return operation.Failed("This record already exists in database");
            var slug = Slugify.GenerateSlug(command.Slug);

            var productCategory = new ProductCategory(command.Name, command.Description,
                command.Picture, command.PictureAlt, command.PictureTitle, command.Keyword,
                command.MetaDescription, slug);
            _productCategoryRepository.Create(productCategory);
            _productCategoryRepository.SaveChanges();
            return operation.Succeeded();

        }

        public OperationResult Edit(EditProductCategory command)
        {
            var operation = new OperationResult();
            var productCategory = _productCategoryRepository.Edit(command.Id);
            if (productCategory == null)
                return operation.Failed("Sorry, no such record exists in database, please try again.");
            if (_productCategoryRepository.Exist(x => x.Name == command.Name && x.Id != command.Id))
                return operation.Failed("This record already exists in database");

            var slug = Slugify.GenerateSlug(command.Slug);
            productCategory.Edit(command.Name, command.Description,
                command.Picture, command.PictureAlt, command.PictureTitle, command.Keyword,
                command.MetaDescription, command.Slug);

            _productCategoryRepository.SaveChanges();
            return operation.Succeeded();
        }

        public List<ProductCategoryViewModel> Search(ProductCategorySearchModel searchModel)
        {
            return _productCategoryRepository.Search(searchModel);
        }

        public EditProductCategory GetDetails(int Id)
        {
            return _productCategoryRepository.GetDetail(Id);
        }
    }
}
