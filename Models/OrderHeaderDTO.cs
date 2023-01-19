using System.ComponentModel.DataAnnotations;

namespace Models;

public class OrderHeaderDTO
{
    public int Id { get; set; }

    [Required]
    public string UserId { get; set; } = string.Empty;

    // add navigation property : #TODO

    [Required]
    [Display(Name = "Order Total")]
    public double OrderTotal { get; set; }

    [Required]
    public DateTime OrderDate { get; set; }

    [Required]
    [Display(Name = "Shipping Date")]
    public DateTime ShippingDate { get; set; }

    [Required]
    public string Status { get; set; } = string.Empty;

    // stripe payment
    public string SessionId { get; set; } = string.Empty;
    public string PaymentIntentId { get; set; } = string.Empty;

    [Required]
    [Display(Name = "Name")]
    public string Name { get; set; } = string.Empty;

    [Required]
    [Display(Name = "Phone Number")]
    public string PhoneNumber { get; set; } = string.Empty;
    [Required]
    [Display(Name = "Street Address")]
    public string StreetAddress { get; set; } = string.Empty;
    [Required]
    public string State { get; set; } = string.Empty;
    [Required]
    public string City { get; set; } = string.Empty;

    [Required]
    [Display(Name = "Postal Code")]
    public string PostalCode { get; set; } = string.Empty;

    [Required]
    [Display(Name ="E-mail")]
    public string Email { get; set; } = string.Empty;

    public string Tracking { get; set; } = string.Empty;

    public string Carrier { get; set; } = string.Empty;

}
