namespace DataAccess.ViewModel;

public class Order
{
    public Order()
    {
        OrderHeader = new OrderHeader();
        OrderDetails = new List<OrderDetail>(0);
    }
    public OrderHeader OrderHeader { get; set; }
    public IEnumerable<OrderDetail> OrderDetails { get; set; }
}
