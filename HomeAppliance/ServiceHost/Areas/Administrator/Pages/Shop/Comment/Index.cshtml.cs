using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using SM.Application.Contracts.Comment;

namespace ServiceHost.Areas.Administrator.Pages.Shop.Comment
{
    public class IndexModel : PageModel
    {

        private readonly ICommentApplication _commentApplication;

        public IndexModel(ICommentApplication commentApplication)
        {
            _commentApplication = commentApplication;
        }

        [TempData]
        public string Message { get; set; }
        public List<CommentViewModel> Comments { get; set; }
        public SelectList ProductList { get; set; }


        public void OnGet(CommentSearchModel search)
        {
            @ViewData["title"] = "Comments";
            Comments = _commentApplication.Search(search);
        }

        public IActionResult OnPostConfirm(int Id)
        {
            var result = _commentApplication.Accept(Id);
            return new JsonResult(result);
        }
        public IActionResult OnPostDeny(int Id)
        {
            var result = _commentApplication.Deny(Id);
            return new JsonResult(result);
        }
    }
}
