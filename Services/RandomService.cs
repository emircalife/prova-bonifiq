using Microsoft.EntityFrameworkCore;
using ProvaPub.Models;
using ProvaPub.Repository;

namespace ProvaPub.Services
{
	public class RandomService
	{
        private static readonly Random _rng = new Random();
        private readonly TestDbContext _ctx;

        public RandomService()
        {
            var contextOptions = new DbContextOptionsBuilder<TestDbContext>()
                                   .UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=Teste;Trusted_Connection=True;")
                                   .Options;
            _ctx = new TestDbContext(contextOptions);
        }

        public async Task<int> GetRandom()
        {
            int number;
            do
            {
                number = _rng.Next(100);  // ou Random.Shared.Next(100)
            } while (_ctx.Numbers.Any(n => n.Number == number));

            _ctx.Numbers.Add(new RandomNumber { Number = number });
            await _ctx.SaveChangesAsync();
            return number;
        }
	}
}
