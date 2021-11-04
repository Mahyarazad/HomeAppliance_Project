namespace SM.Application.Contracts.Product
{
    public class ProductViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public bool IsInStock { get; set; }
        public double UnitPrice { get; set; }
        public string ShortDescription { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public int CategoryId { get; set; }
        public string Picture { get; set; }
        public string CreationTime { get; set; }
        public string Slug { get; set; }
    }
}