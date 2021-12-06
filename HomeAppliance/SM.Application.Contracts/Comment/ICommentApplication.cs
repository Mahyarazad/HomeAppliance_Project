using System.Collections.Generic;
using _0_Framework.Application;
using Microsoft.EntityFrameworkCore;

namespace SM.Application.Contracts.Comment
{
    public interface ICommentApplication
    {
        OperationResult Create(CreateComment command);
        OperationResult Accept(int Id);
        OperationResult Deny(int Id);
        List<CommentViewModel> Search(CommentSearchModel search);
        List<CommentViewModel> GetAll(int Id);
    }
}