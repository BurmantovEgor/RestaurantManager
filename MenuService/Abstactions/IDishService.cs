using CSharpFunctionalExtensions;
using MenuService.Core;
using MenuService.Data.Entities;

namespace MenuService.Abstactions
{
    public interface IDishService
    {
        Task<Result<List<DishEntity>>> GetAll();
        Task<Result> Add(AddDishDTO dish);

    }
}
