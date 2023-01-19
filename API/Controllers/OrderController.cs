using Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Business.Repository.IRepository;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
public class OrderController : ControllerBase
{
    private readonly IOrderRepository _orderRepository;

    public OrderController(IOrderRepository orderRepository)
    {
        _orderRepository = orderRepository;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        return Ok(await _orderRepository.GetAll());
    }

    [HttpGet("{orderHeaderId:int}")]
    public async Task<IActionResult> GetById(int? orderHeaderId)
    {
        if (!orderHeaderId.HasValue || orderHeaderId.Value == 0)
        {
            return BadRequest(new ErrorModelDTO()
            {
                ErrorMessage = "Invalid ID",
                StatusCode = StatusCodes.Status400BadRequest
            });
        }

        var orderHeader = await _orderRepository.Get(orderHeaderId.Value);

        if (orderHeader == null)
        {
            return NotFound(new ErrorModelDTO()
            {
                ErrorMessage = "Invalid ID",
                StatusCode = StatusCodes.Status404NotFound
            });
        }

        return Ok(orderHeader);
    }

    [HttpPost]
    [ActionName("Create")]
    public async Task<IActionResult> Create([FromBody] StripePaymentDTO paymentDTO)
    {
        var result = await _orderRepository.Create(paymentDTO.Order);
        return Ok(result);
    }

    [HttpPost]
    [ActionName("paymentsuccessful")]
    public async Task<IActionResult> PaymentSuccessful([FromBody] OrderHeaderDTO orderHeaderDTO)
    {
        var result = await _orderRepository.MarkPaymentSuccessful(orderHeaderDTO.Id, Guid.NewGuid().ToString());
        if (result == null)
        {
            return BadRequest(new ErrorModelDTO() { ErrorMessage = "Can not mark payment as successful" });
        }

        return Ok(result);
    }
}
