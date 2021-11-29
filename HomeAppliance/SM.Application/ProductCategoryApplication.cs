using System;
using System.Collections.Generic;
using System.Linq;
using _0_Framework.Application;
using ShopManagement.Domain;
using SM.Application.Contracts;

namespace SM.Application
{
    public class ProductCategoryApplication : IProductCategoryApplication
    {
        private readonly IProductCategoryRepository _productCategoryRepository;
        private readonly IFileUploader _fileUploader;
        public ProductCategoryApplication(IProductCategoryRepository productCategoryRepository,
            IFileUploader fileUploader)
        {
            _productCategoryRepository = productCategoryRepository;
            _fileUploader = fileUploader;
        }

        public OperationResult Create(CreateProductCategory command)
        {
            var operation = new OperationResult();
            if (_productCategoryRepository.Exist(x => x.Name == command.Name))
                return operation.Failed("This record already exists in database");
            var slug = Slugify.GenerateSlug(command.Slug);
            var fileName = _fileUploader.Uploader(command.Picture, $"{command.Name}", command.Name);
            var productCategory = new ProductCategory(command.Name, command.Description,
                fileName, command.PictureAlt, command.PictureTitle, command.Keyword,
                command.MetaDescription, slug);
            _productCategoryRepository.Create(productCategory);
            _productCategoryRepository.SaveChanges();
            return operation.Succeeded();

        }

        public OperationResult Edit(EditProductCategory command)
        {
            var operation = new OperationResult();
            var productCategory = _productCategoryRepository.Get(command.Id);
            if (productCategory == null)
                return operation.Failed("Sorry, no such record exists in database, please try again.");
            if (_productCategoryRepository.Exist(x => x.Name == command.Name && x.Id != command.Id))
                return operation.Failed("This record already exists in database");

            var slug = Slugify.GenerateSlug(command.Slug);
            var fileName = _fileUploader.Uploader(command.Picture, $"{command.Name}", command.Name);
            productCategory.Edit(command.Name, command.Description,
                fileName, command.PictureAlt, command.PictureTitle, command.Keyword,
                command.MetaDescription, command.Slug);

            _productCategoryRepository.SaveChanges();
            return operation.Succeeded();
        }

        public OperationResult Deactivate(int Id)
        {
            var result = new OperationResult();
            var productCategory = _productCategoryRepository.Get(Id);
            if (productCategory == null)
                return result.Failed("Sorry, no such record exists in database, please try again.");
            _productCategoryRepository.Deactive(Id);
            return result.Succeeded();
        }

        public OperationResult Reactivate(int Id)
        {
            var result = new OperationResult();
            var productCategory = _productCategoryRepository.Get(Id);
            if (productCategory == null)
                return result.Failed("Sorry, no such record exists in database, please try again.");
            _productCategoryRepository.Reactive(Id);
            return result.Succeeded();
        }

        public List<ProductCategoryViewModel> Search(ProductCategorySearchModel searchModel)
        {
            return _productCategoryRepository.Search(searchModel);
        }

        public List<ProductCategoryViewModel> GetList()
        {
            return _productCategoryRepository.GetCustomList();
        }

        public EditProductCategory GetDetails(int Id)
        {
            return _productCategoryRepository.GetDetail(Id);
        }
    }
}
