using System.Collections.Generic;
using System.Linq;
using _0_Framework.Application;
using _0_Framework;
using SM.Application.Contracts;
using SM.Application.Contracts.Product;
using SM.Domain.ProductAgg;

namespace SM.Application
{
    public class ProductApplication : IProductApplication
    {
        private readonly IProductRepository _productRepository;
        private readonly IFileUploader _fileUploader;
        public ProductApplication(IProductRepository productRepository, IFileUploader fileUploader)
        {
            _productRepository = productRepository;
            _fileUploader = fileUploader;
        }

        public OperationResult Create(CreateProduct command)
        {
            var operation = new OperationResult();
            if (_productRepository.Exist(x => x.Name == command.Name))
                operation.Failed(message: "This record already exists.");
            var Slug = Slugify.GenerateSlug(command.Slug);

            var fileName = _fileUploader.Uploader(command.Picture, $"{command.CategoryName}\\{command.Name}\\", command.Name);
            var product = new Product(command.Name, command.Code, command.ShortDescription,
                command.ShortDescription, fileName, command.PictureAlt, command.PictureTitle,
                command.CategoryId, Slug, command.MetaDescription, command.Keyword);
            _productRepository.Create(product);
            _productRepository.SaveChanges();
            return operation.Succeeded();
        }

        public OperationResult Edit(EditProduct command)
        {
            var operation = new OperationResult();
            var product = _productRepository.Get(command.Id);
            var categoryName = _productRepository.GetDetail(command.Id);
            if (product == null)
                operation.Failed(ApplicationMessage.RecordNotFound);
            if (_productRepository.Exist(x => x.Name == command.Name && x.Id != command.Id))
                operation.Failed(ApplicationMessage.RecordExists);
            var Slug = Slugify.GenerateSlug(command.Slug);
            var fileName = _fileUploader.Uploader(command.Picture, $"{categoryName.CategoryName}\\{command.Name}", command.Name);

            product.Edit(command.Name, command.Code, command.ShortDescription,
                command.ShortDescription, fileName, command.PictureAlt, command.PictureTitle,
                command.CategoryId, Slug, command.MetaDescription, command.Keyword);
            _productRepository.SaveChanges();
            return operation.Succeeded();
        }

        public List<ProductViewModel> Search(ProductSearchModel searchModel)
        {
            return _productRepository.Search(searchModel);
        }

        public EditProduct GetDetails(int Id)
        {
            return _productRepository.GetDetail(Id);
        }

        //public OperationResult ReplenishStock(int Id)
        //{
        //    var operation = new OperationResult();
        //    var product = _productRepository.Get(Id);
        //    if (product == null)
        //        return operation.Failed(ApplicationMessage.RecordNotFound);
        //    product.ReplenishStock();
        //    _productRepository.SaveChanges();
        //    return operation.Succeeded();
        //}

        //public OperationResult EmptyStock(int Id)
        //{
        //    var operation = new OperationResult();
        //    var product = _productRepository.Get(Id);
        //    if (product == null)
        //        return operation.Failed(ApplicationMessage.RecordNotFound);
        //    product.EmptyStock();
        //    _productRepository.SaveChanges();
        //    return operation.Succeeded();
        //}

        public List<ProductViewModel> GetList()
        {
            var query = _productRepository.GetList().Select(x => new ProductViewModel
            {
                Name = x.Name,
                Id = x.Id,
            });
            return query.ToList();
        }
    }
}