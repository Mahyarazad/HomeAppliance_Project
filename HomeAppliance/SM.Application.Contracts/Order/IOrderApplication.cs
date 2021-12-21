namespace SM.Application.Contracts.Order
{
    public interface IOrderApplication
    {
        long PlaceOrder(Cart Cart);
    }
}