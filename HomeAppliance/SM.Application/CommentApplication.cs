using System.Collections.Generic;
using _0_Framework;
using _0_Framework.Application;
using SM.Application.Contracts.Comment;
using SM.Domain.CommentAgg;

namespace SM.Application
{
    public class CommentApplication : ICommentApplication
    {
        private readonly ICommentRepository _commentRepository;

        public CommentApplication(ICommentRepository commentRepository)
        {
            _commentRepository = commentRepository;
        }

        public OperationResult Create(CreateComment command)
        {
            var result = new OperationResult();
            var comment = new Comment(command.Email, command.PersonName, command.Body, command.ProductId, command.rating);
            _commentRepository.Create(comment);
            _commentRepository.SaveChanges();
            return result.Succeeded();

        }

        public OperationResult Accept(int Id)
        {
            var result = new OperationResult();
            var comment = _commentRepository.Get(Id);
            comment.Confirm();
            _commentRepository.SaveChanges();
            return result.Succeeded();
        }

        public OperationResult Deny(int Id)
        {
            var result = new OperationResult();
            var comment = _commentRepository.Get(Id);
            comment.Deny();
            _commentRepository.SaveChanges();
            return result.Succeeded();
        }

        public List<CommentViewModel> Search(CommentSearchModel search)
        {
            return _commentRepository.Search(search);
        }

        public List<CommentViewModel> GetAll(int Id)
        {
            return _commentRepository.GetAll(Id);
        }

    }
}