using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
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
			await using var db = await _factory.CreateDbContextAsync(stoppingToken);

			var cat1 = new Category { Name = "Category 1" };
			var cat2 = new Category { Name = "Category 2" };

			var products = new List<Product>(capacity: 100);
			foreach (var productNum in Enumerable.Range(0, 100))
			{
				var p = new Product
				{
					Name = $"Product {productNum}"
				};
				products.Add(p);

				if (productNum % 2 == 0)
				{
					(cat1.Products ??= new List<Product>()).Add(p);
				}
				else
				{
					(cat2.Products ??= new List<Product>()).Add(p);
				}
			}

			var stores = new List<Store>(capacity: 10);
			foreach (var storeNum in Enumerable.Range(0, 10))
			{
				var store = new Store
				{
					Name = $"Store {storeNum}",
					Aisles = new List<Aisle>(capacity: 10)
				};

				foreach (var aisleNum in Enumerable.Range(0, 10))
				{
					var aisleLetter = aisleNum % 2 == 0 ? "A" : "B";
					var aisle = new Aisle
					{
						Alias = $"Aisle {aisleNum}{aisleLetter}",
						Bays = new List<Bay>(capacity: 10)
					};

					foreach (var bayNum in Enumerable.Range(0, 10))
					{
						var bayLetter = bayNum % 2 == 0 ? "A" : "B";
						var bay = new Bay
						{
							Alias = $"Bay {bayNum}{bayLetter}",
							Shelves = new List<Shelf>(capacity: 5)
						};

						foreach (var shelfNum in Enumerable.Range(0, 5))
						{
							var shelf = new Shelf
							{
								Alias = $"Shelf {shelfNum}",
								Inventory = new List<ProductInventory>() 
							};

							bay.Shelves.Add(shelf);
						}

						aisle.Bays.Add(bay);
					}

					store.Aisles.Add(aisle);
				}

				stores.Add(store);
			}

			db.Categories.AddRange(cat1, cat2);
			db.Products.AddRange(products);
			db.Stores.AddRange(stores);

			await db.SaveChangesAsync(stoppingToken);

			var rng = new Random(12345); 
			var allShelves = await db.Shelves.AsNoTracking().ToListAsync(stoppingToken);

			var inventories = new List<ProductInventory>(capacity: allShelves.Count * 6);
			foreach (var shelf in allShelves)
			{
				var takeCount = rng.Next(3, 11); 
												 
				foreach (var product in products.OrderBy(_ => rng.Next()).Take(takeCount))
				{
					inventories.Add(new ProductInventory
					{
						ShelfId = shelf.Id,
						ProductId = product.Id,
						OnHand = rng.Next(1, 51), 
						DB_CreatedAt = DateTimeOffset.UtcNow,
						DB_LastUpdateAt = DateTimeOffset.UtcNow
					});
				}
			}

			db.Inventory.AddRange(inventories);
			await db.SaveChangesAsync(stoppingToken);
		}
	}
}
