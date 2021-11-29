namespace SM.Application.Contracts
{
    public class ProductCategoryViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Picture { get; set; }
        public string CreationDate { get; set; }
        public long ProductCount { get; set; }
        public bool IsActive { get; set; }
    }
}