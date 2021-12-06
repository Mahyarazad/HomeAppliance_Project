using System.Collections.Generic;
using _0_Framework;
using SM.Application.Contracts.Comment;

namespace SM.Domain.CommentAgg
{
    public interface ICommentRepository : IRepository<int, Comment>
    {
        List<CommentViewModel> Search(CommentSearchModel search);
        List<CommentViewModel> GetAll(int Id);
        CommentViewModel GetDetail(int id);
    }
}