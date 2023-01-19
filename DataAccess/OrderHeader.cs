using System.ComponentModel.DataAnnotations;
namespace DataAccess;

public class OrderHeader
{
    public int Id { get; set; }

    [Required]
    public string UserId { get; set; } = string.Empty;    

    [Required]
    public double OrderTotal { get; set; }

    [Required]
    public DateTime OrderDate { get; set; }

    [Required]
    public DateTime ShippingDate { get; set; }

    [Required]
    public string Status { get; set; } = string.Empty;

    // stripe payment
    public string SessionId { get; set; } = string.Empty;
    public string PaymentIntentId { get; set; } = string.Empty;

    [Required]
    public string Name { get; set; } = string.Empty;

    [Required]
    public string PhoneNumber { get; set; } = string.Empty;

    [Required]
    public string StreetAddress { get; set; } = string.Empty;

    [Required]
    public string State { get; set; } = string.Empty;

    [Required]
    public string City { get; set; } = string.Empty;

    [Required]
    public string PostalCode { get; set; } = string.Empty;

    [Required]
    public string Email { get; set; } = string.Empty;

    public string Tracking { get; set; } = string.Empty;

    public string Carrier { get; set; } = string.Empty;
}