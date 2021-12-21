namespace SM.Application.Contracts.Order
{
    public class CartItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public double DiscountRate { get; set; }
        public string Picture { get; set; }
        public string CategoryPicture { get; set; }
        public long Count { get; set; }
        public double TotalCart { get; set; }
        public double TotalCartAfterDiscount { get; set; }
        public double TotalDiscount { get; set; }
        public bool IsInStock { get; set; }
    }
}