using CSharpFunctionalExtensions;
using MenuService.Abstactions;
using MenuService.Data.Entities;

namespace MenuService.Core
{
    public class DishService : IDishService
    {
        private readonly IDishRepository _dishRepository;

        public DishService(IDishRepository dishRepository)
        {
            _dishRepository = dishRepository;
        }

        public async Task<Result> Add(AddDishDTO dish)
        {
            Console.WriteLine($"name = {dish.Name}  price = {dish.Price}");

            var dishEntity = new DishEntity()
            {
                Id = Guid.NewGuid(),
                Name = dish.Name,
                Price = dish.Price,
            };
            Console.WriteLine($"name = {dishEntity.Name}  price = {dishEntity.Price} id = {dishEntity.Id}");

            var result = await _dishRepository.Add(dishEntity);
            return result;
        }

        public async Task<Result<List<DishEntity>>> GetAll()
        {
            var result = await _dishRepository.GetAll();
            return result;

        }
    }
}
