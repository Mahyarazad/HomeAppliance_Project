using System.Collections.Generic;
using System.Linq;
using _0_Framework.Infrastructure;
using Microsoft.EntityFrameworkCore;
using SM.Application.Contracts.Comment;
using SM.Domain.CommentAgg;

namespace SM.Infrastructure.Repositories
{
    public class CommentRepository : RepositoryBase<int, Comment>, ICommentRepository
    {
        private readonly SMContext _smContext;

        public CommentRepository(SMContext smContext) : base(smContext)
        {
            _smContext = smContext;
        }


        public List<CommentViewModel> Search(CommentSearchModel search)
        {
            var query = _smContext.Comment
                .Include(x => x.Product)
                .Select(x => new CommentViewModel
                {
                    ProductId = x.Product.Id,
                    ProductName = x.Product.Name,
                    Accepted = x.Accepted,
                    Denied = x.Denied,
                    Body = x.Body,
                    Email = x.Email,
                    Id = x.Id,
                    PersonName = x.PersonName
                });
            if (!string.IsNullOrWhiteSpace(search.Product))
            {
                query = query.Where(x => x.ProductName.Contains(search.Product));
            }
            if (!string.IsNullOrWhiteSpace(search.Name))
            {
                query = query.Where(x => x.PersonName.Contains(search.Name));
            }
            if (!string.IsNullOrWhiteSpace(search.Email))
            {
                query = query.Where(x => x.Email.Contains(search.Email));
            }

            return query.OrderByDescending(x => x.Id).ToList();
        }

        public List<CommentViewModel> GetAll(int Id)
        {
            var query = _smContext.Comment
                .Include(x => x.Product)
                .Select(x => new CommentViewModel
                {
                    ProductId = x.Product.Id,
                    ProductName = x.Product.Name,
                    Accepted = x.Accepted,
                    Denied = x.Denied,
                    Body = x.Body,
                    Email = x.Email,
                    Id = x.Id,
                    PersonName = x.PersonName,
                    CreationDate = x.CreationTime,
                    Rating = x.Rating
                }).Where(x => x.ProductId == Id);

            return query.OrderByDescending(x => x.Id).ToList();
        }

        public CommentViewModel GetDetail(int id)
        {
            return _smContext.Comment
                .Include(x => x.Product)
                .Select(x => new CommentViewModel
                {
                    ProductId = x.Product.Id,
                    ProductName = x.Product.Name,
                    Accepted = x.Accepted,
                    Denied = x.Denied,
                    Body = x.Body,
                    Email = x.Email,
                    Id = x.Id,
                    PersonName = x.PersonName,
                    Rating = x.Rating
                }).FirstOrDefault(x => x.Id == id);
        }
    }
}