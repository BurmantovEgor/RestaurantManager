using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using PaymentService.Abstractions;

namespace PaymentService.Controllers
{
    [ApiController]
    [Route("payment")]

    public class PaymentController : ControllerBase
    {
        IPaymentOrderService _service;
        public PaymentController(IPaymentOrderService service)
        {
            _service = service;
        }

        [HttpPut("{orderId}")]
        public async Task<IActionResult> OrderPay(Guid orderId)
        {
           var result = await _service.UpdatePaymentStatus(orderId);  
            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> AllOrders()
        {
            var result = await _service.GetAll();
            return Ok(result.Value);
        }

    }
}
