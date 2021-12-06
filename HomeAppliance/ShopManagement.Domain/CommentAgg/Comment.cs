using _0_Framework;
using SM.Domain.ProductAgg;

namespace SM.Domain.CommentAgg
{
    public class Comment : BaseEntity<int>
    {
        public Comment(string email, string personName, string body, int productId, string rating)
        {
            Email = email;
            PersonName = personName;
            Body = body;
            ProductId = productId;
            Accepted = false;
            Denied = true;
            Rating = int.Parse(rating);
        }

        public void Confirm()
        {
            Accepted = true;
            Denied = false;
        }

        public void Deny()
        {
            Accepted = false;
            Denied = true;

        }
        protected Comment()
        {

        }
        public string Email { get; private set; }
        public string PersonName { get; private set; }
        public string Body { get; private set; }
        public int ProductId { get; private set; }
        public bool Accepted { get; private set; }
        public bool Denied { get; private set; }
        public Product Product { get; private set; }
        public int Rating { get; private set; }

    }
}