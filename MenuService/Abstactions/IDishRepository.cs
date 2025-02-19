using CSharpFunctionalExtensions;
using MenuService.Data.Entities;

namespace MenuService.Abstactions
{
    public interface IDishRepository
    {
        Task<Result<List<DishEntity>>> GetAll();
        Task<Result> Add(DishEntity dish);
    }
}
