namespace Models;

public class StripePaymentDTO
{
    public StripePaymentDTO()
    {
        SuccessUrl = "OrderConfirmation";
        CancelUrl = "Summary";
        Order = new();
    }

    public OrderDTO Order { get; set; }
    public string SuccessUrl { get; set; }
    public string CancelUrl { get; set; }
}
