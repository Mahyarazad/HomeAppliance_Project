using System.Collections.Generic;
using _0_Framework;
using _0_Framework.Application;
using SM.Application.Contracts.ProductPicture;
using SM.Domain.ProductPictureAgg;

namespace SM.Application
{
    public class ProductPictureApplication : IProductPictureApplication
    {
        private readonly IProductPictureRepository _productPictureRepository;

        public ProductPictureApplication(IProductPictureRepository productPictureRepository)
        {
            _productPictureRepository = productPictureRepository;
        }

        public OperationResult Create(CreateProductPicture command)
        {
            var result = new OperationResult();
            if (_productPictureRepository.Exist(x => x.Picture == command.Picture && x.ProductId == command.ProductId))
                return result.Failed(ApplicationMessage.RecordExists);
            var newRecord = new ProductPicture(command.ProductId, command.Picture, command.PictureAlt,
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
            if (_productPictureRepository.Exist(x => x.Picture == command.Picture
                                                     && x.ProductId == command.ProductId
                                                     && x.Id != command.Id))
                return result.Failed(ApplicationMessage.RecordExists);
            target.Edit(command.ProductId, command.Picture, command.PictureAlt,
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