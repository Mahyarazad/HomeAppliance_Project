using System.Collections.Generic;
using _0_Framework.Application;
using _0_Framework;
using SM.Application.Contracts.Product;
using SM.Domain.ProductAgg;

namespace SM.Application
{
    public class ProductApplication : IProductApplication
    {
        private readonly IProductRepository _productRepository;

        public ProductApplication(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public OperationResult Create(CreateProduct command)
        {
            var operation = new OperationResult();
            if (_productRepository.Exist(x => x.Name == command.Name))
                operation.Failed(message: "This record already exists.");
            var Slug = Slugify.GenerateSlug(command.Slug);
            var product = new Product(command.Name, command.Code, command.UnitPrice, command.ShortDescription,
                command.ShortDescription, command.Picture, command.PictureAlt, command.PictureTitle,
                command.CategoryId, Slug, command.MetaDescription, command.Keyword);
            _productRepository.Create(product);
            _productRepository.SaveChanges();
            return operation.Succeeded();
        }

        public OperationResult Edit(EditProduct command)
        {
            var operation = new OperationResult();
            var product = _productRepository.Get(command.Id);
            if (product == null)
                operation.Failed(ApplicationMessage.RecordNotFound);
            if (_productRepository.Exist(x => x.Name == command.Name && x.Id != command.Id))
                operation.Failed(ApplicationMessage.RecordExists);
            var Slug = Slugify.GenerateSlug(command.Slug);
            product.Edit(command.Name, command.Code, command.UnitPrice, command.ShortDescription,
                command.ShortDescription, command.Picture, command.PictureAlt, command.PictureTitle,
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

        public OperationResult ReplenishStock(int Id)
        {
            var operation = new OperationResult();
            var product = _productRepository.Get(Id);
            if (product == null)
                return operation.Failed(ApplicationMessage.RecordNotFound);
            product.ReplenishStock();
            _productRepository.SaveChanges();
            return operation.Succeeded();
        }

        public OperationResult EmptyStock(int Id)
        {
            var operation = new OperationResult();
            var product = _productRepository.Get(Id);
            if (product == null)
                return operation.Failed(ApplicationMessage.RecordNotFound);
            product.EmptyStock();
            _productRepository.SaveChanges();
            return operation.Succeeded();
        }
    }
}