using MenuService.Abstactions;
using MenuService.Core;
using MenuService.Data.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MenuService.Controllers
{
    [Route("menu")]
    [ApiController]
    public class MenuController:ControllerBase
    {
        IDishService _dishService;

        public MenuController(IDishService dishService)
        {
            _dishService = dishService;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AddDishDTO dish)
        {
           var result = await _dishService.Add(dish);
            return Ok(dish);
        }


        [HttpGet]
        public async Task<IActionResult> All()
        {
            var result = await _dishService.GetAll();
            return Ok(result.Value);
        }


    }
}
