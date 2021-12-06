using System;

namespace SM.Application.Contracts.Comment
{
    public class CommentViewModel
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string PersonName { get; set; }
        public string Body { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public bool Accepted { get; set; }
        public bool Denied { get; set; }
        public DateTime CreationDate { get; set; }
        public int Rating { get; set; }
    }
}