namespace Fop.Order
{
    public interface IOrderRequest
    {
        string OrderBy { get; }

        OrderDirection Direction { get; }
    }
}
