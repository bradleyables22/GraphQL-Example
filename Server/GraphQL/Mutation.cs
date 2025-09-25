using Microsoft.EntityFrameworkCore;
using Server.Data;
using Server.Data.Models;

namespace Server.GraphQL
{
	public class Mutation
	{
		public async Task<Store> AddStore([Service] IDbContextFactory<EfContext> _factory,string name) 
		{
			var _db = await _factory.CreateDbContextAsync();

			var store = new Store
			{
				Name = name,
			};
			_db.Add(store);
			await _db.SaveChangesAsync();
			return store;
		}
		public async Task<Aisle> AddAisle([Service] IDbContextFactory<EfContext> _factory,string storeId, string alias)
		{
			var _db = await _factory.CreateDbContextAsync();

			var aisle = new Aisle
			{
				StoreId = storeId,
				Alias = alias
			};
			_db.Add(aisle);
			await _db.SaveChangesAsync();
			return aisle;
		}
		public async Task<Bay> AddBay([Service] IDbContextFactory<EfContext> _factory, string aisleId, string alias)
		{
			var _db = await _factory.CreateDbContextAsync();

			var bay = new Bay
			{
				AisleId = aisleId,
				Alias = alias
			};
			_db.Add(bay);
			await _db.SaveChangesAsync();
			return bay;
		}
		public async Task<Shelf> AddShelf([Service] IDbContextFactory<EfContext> _factory, string bayId, string alias)
		{
			var _db = await _factory.CreateDbContextAsync();

			var shelf = new Shelf
			{
				BayId = bayId,
				Alias = alias
			};
			_db.Add(shelf);
			await _db.SaveChangesAsync();
			return shelf;
		}
		public async Task<Product> AddProduct([Service] IDbContextFactory<EfContext> _factory, string name)
		{
			var _db = await _factory.CreateDbContextAsync();

			var product = new Product
			{
				Name= name
			};
			_db.Add(product);
			await _db.SaveChangesAsync();
			return product;
		}
		public async Task<ProductInventory> Stock([Service] IDbContextFactory<EfContext> _factory, string productId, string shelfId, int newInventory)
		{
			var _db = await _factory.CreateDbContextAsync();

			var inventory = await _db.Inventory.Where(x => x.ShelfId == shelfId && x.ProductId == productId).FirstOrDefaultAsync();
			if (inventory is not null)
			{
				inventory.OnHand = +newInventory;
				_db.Update(inventory);
			}
			else 
			{
				inventory = new ProductInventory
				{
					ShelfId = shelfId,
					ProductId = productId,
					OnHand = newInventory
				};
				_db.Add(inventory);	
			}
			await _db.SaveChangesAsync();
			return inventory;
		}
	}
}
