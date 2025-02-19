using KitchenService.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace KitchenService.Controllers
{
    [ApiController]
    [Route("kitchen")]

    public class KitchenController: ControllerBase
    {
        private readonly IKitchenService _kitchenService;

        public KitchenController(IKitchenService kitchenService)
        {
            _kitchenService = kitchenService;
        }

        [HttpGet]
        public async Task<IActionResult> AllOrders()
        {
            Console.WriteLine("test123");
            var result = await _kitchenService.GetAll(); 
            return Ok(result.Value);
        }
    }
}
