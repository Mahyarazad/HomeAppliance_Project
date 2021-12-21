namespace Query.Contracts.Inventory
{
    public interface IInventoryQuery
    {
        StockStatus CheckStock(CheckStock command);
    }
}