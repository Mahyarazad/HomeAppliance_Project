using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Linq;
using Query.Contracts.Product;
using SM.Application.Contracts.Comment;

namespace ServiceHost.Pages
{
    public class SingleProduct : PageModel
    {
        private readonly IProductQuery _productQuery;
        private readonly ICommentApplication _commentApplication;
        public ProductQueryModel Product { get; set; }
        public List<CommentViewModel> Comments { get; set; }
        public int CommentCount { get; set; }
        public double AverageScore { get; set; }
        public Dictionary<string, int> ScoreList { get; set; }
        public SingleProduct(IProductQuery productQuery, ICommentApplication commentApplication)
        {
            _productQuery = productQuery;
            _commentApplication = commentApplication;
        }

        public void OnGet(string id)
        {
            Product = _productQuery.GetSingleProduct(id);
            Comments = _commentApplication.GetAll(Product.Id);
            CommentCount = Comments.Where(x => x.Accepted).Count();
            if (CommentCount != 0)
                AverageScore = Comments.Sum(x => x.Rating) / CommentCount;
            ScoreList = new Dictionary<string, int>();
            for (int i = 1; i < 6; i++)
            {
                var value = Comments.Where(x => x.Rating == i).ToList().Count();
                if (value != null)
                    ScoreList.Add($"{i}-star", value);
            }
        }

        public IActionResult OnPost(CreateComment command, string productSlug)
        {
            var result = _commentApplication.Create(command);
            return RedirectToPage("./SingleProduct", new { Id = productSlug });
        }
    }
}
