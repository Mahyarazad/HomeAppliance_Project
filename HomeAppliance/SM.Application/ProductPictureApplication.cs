using System.Collections.Generic;
using System.Linq;
using _0_Framework;
using _0_Framework.Application;
using SM.Application.Contracts.ProductPicture;
using SM.Domain.ProductPictureAgg;

namespace SM.Application
{
    public class ProductPictureApplication : IProductPictureApplication
    {
        private readonly IProductPictureRepository _productPictureRepository;
        private readonly IFileUploader _fileUploader;
        public ProductPictureApplication(IProductPictureRepository productPictureRepository,
            IFileUploader fileUploader)
        {
            _productPictureRepository = productPictureRepository;
            _fileUploader = fileUploader;
        }

        public OperationResult Create(CreateProductPicture command)
        {
            var result = new OperationResult();
            // if (_productPictureRepository.Exist(x => x.ProductId == command.ProductId))
            //     return result.Failed(ApplicationMessage.RecordExists);
            var productName = command.Products.FirstOrDefault(x => x.Id == command.ProductId).Name;
            var fileName = _fileUploader.Uploader(command.Picture, $"Site-Picture\\{productName}", productName);
            var newRecord = new ProductPicture(command.ProductId, fileName, command.PictureAlt,
                command.PictureTitle);
            _productPictureRepository.Create(newRecord);
            _productPictureRepository.SaveChanges();
            return result.Succeeded();
        }

        public OperationResult Edit(EditProductPicture command)
        {
            var result = new OperationResult();
            var target = _productPictureRepository.Get(command.Id);
            if (target == null)
                return result.Failed(ApplicationMessage.RecordNotFound);
            // if (_productPictureRepository.Exist(x =>
            //                                           x.ProductId == command.ProductId
            //                                          && x.Id == command.Id))
            var productName = command.Products.FirstOrDefault(x => x.Id == command.ProductId).Name;
            var fileName = _fileUploader.Uploader(command.Picture, $"Site-Picture\\{productName}", productName);
            target.Edit(command.ProductId, fileName, command.PictureAlt,
                command.PictureTitle);
            _productPictureRepository.SaveChanges();
            return result.Succeeded();

        }

        public OperationResult Delete(int id)
        {
            var result = new OperationResult();
            var target = _productPictureRepository.Get(id);
            if (target == null)
                return result.Failed(ApplicationMessage.RecordNotFound);
            target.Delete();
            _productPictureRepository.SaveChanges();
            return result.Succeeded();
        }

        public OperationResult Reactivate(int id)
        {
            var result = new OperationResult();
            var target = _productPictureRepository.Get(id);
            if (target == null)
                return result.Failed(ApplicationMessage.RecordNotFound);
            target.Reactivate();
            _productPictureRepository.SaveChanges();
            return result.Succeeded();
        }

        public List<ProductPictureViewModel> Search(ProductPictureSearchModel searchModel)
        {
            return _productPictureRepository.Search(searchModel);
        }
        public EditProductPicture GetDetails(int Id)
        {
            return _productPictureRepository.GetDetail(Id);
        }
    }
}