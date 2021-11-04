namespace SM.Application.Contracts.Product
{
    public class ProductSearchModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public int CategoryId { get; set; }
    }
}