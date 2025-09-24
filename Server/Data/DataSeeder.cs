
using Microsoft.EntityFrameworkCore;
using Server.Data.Models;

namespace Server.Data
{
	public class DataSeeder : BackgroundService
	{
		private readonly IDbContextFactory<EfContext> _factory;
		public DataSeeder(IDbContextFactory<EfContext> factory)
		{
			_factory = factory;
		}
		protected override async Task ExecuteAsync(CancellationToken stoppingToken)
		{
			using var db = await _factory.CreateDbContextAsync(stoppingToken);


			var catAccessories = new Category { Name = "Accessories" };
			var catShoes = new Category { Name = "Shoes" };

			var store = new Store { Name = "Main Street 001" };

			var aisle1 = new Aisle { Alias = "Aisle 01", Store = store };
			var aisle2 = new Aisle { Alias = "Aisle 02", Store = store };

			var bay1A = new Bay { Alias = "Bay A", Aisle = aisle1 };
			var bay1B = new Bay { Alias = "Bay B", Aisle = aisle1 };
			var bay2A = new Bay { Alias = "Bay A", Aisle = aisle2 };

			var shelf1 = new Shelf { Alias = "Shelf 01", Bay = bay1A };
			var shelf2 = new Shelf { Alias = "Shelf 02", Bay = bay1A };
			var shelf3 = new Shelf { Alias = "Shelf 03", Bay = bay1B };
			var shelf4 = new Shelf { Alias = "Shelf 04", Bay = bay2A };

			var p1 = new Product { Name = "Classic Belt", Category = catAccessories, Shelf = shelf1, Quantity = 25 };
			var p2 = new Product { Name = "Runner 2000 Shoe", Category = catShoes, Shelf = shelf2, Quantity = 12 };
			var p3 = new Product { Name = "Wool Beanie Hat", Category = catAccessories, Shelf = shelf3, Quantity = 18 };

			db.AddRange(
				store, aisle1, aisle2,
				bay1A, bay1B, bay2A,
				shelf1, shelf2, shelf3, shelf4,
				catAccessories, catShoes,
				p1, p2, p3
			);

			await db.SaveChangesAsync(stoppingToken);
		}
	}
}
