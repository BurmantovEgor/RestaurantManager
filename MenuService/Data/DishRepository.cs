using CSharpFunctionalExtensions;
using MenuService.Abstactions;
using MenuService.Data.Configurations;
using MenuService.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace MenuService.Data
{
    public class DishRepository : IDishRepository
    {
        private readonly MenuServiceDbContext _context;

        public DishRepository(MenuServiceDbContext dishRepository)
        {
            _context = dishRepository;
        }

        public async Task<Result> Add(DishEntity dish)
        {
            if(dish == null)
            {
                return Result.Success();
            }
            await _context.Dish.AddAsync(dish);
            await _context.SaveChangesAsync();
            return Result.Success();

        }

        public async Task<Result<List<DishEntity>>> GetAll()
        {
            var result = await _context.Dish.AsNoTracking().ToListAsync();
            return Result.Success(result);
        }
    }
}
