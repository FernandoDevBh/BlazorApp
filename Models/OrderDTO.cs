namespace Models;

public class OrderDTO
{
    public OrderDTO()
    {
        OrderHeader = new();
        OrderDetails = new();
    }

    public OrderHeaderDTO OrderHeader { get; set; }
    public List<OrderDetailDTO> OrderDetails { get; set; }
}
