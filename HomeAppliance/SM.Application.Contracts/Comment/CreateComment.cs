namespace SM.Application.Contracts.Comment
{
    public class CreateComment
    {
        public string Email { get; set; }
        public string PersonName { get; set; }
        public string Body { get; set; }
        public int ProductId { get; set; }
        public string rating { get; set; }

    }
}