namespace SM.Application.Contracts.ProductPicture
{
    public class ProductPictureViewModel
    {
        public int ProductId { get; set; }
        public int Id { get; set; }
        public string Picture { get; set; }
        public string Product { get; set; }
        public string CreationTime { get; set; }
        public bool IsRemoved { get; set; }

    }
}