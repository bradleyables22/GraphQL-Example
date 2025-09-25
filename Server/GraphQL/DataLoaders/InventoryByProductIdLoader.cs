using Microsoft.EntityFrameworkCore;
using Server.Data;
using Server.Data.Models;

namespace Server.GraphQL.DataLoaders
{
	public class InventoryByProductIdLoader : GroupedDataLoader<string, ProductInventory>
	{
		private readonly IDbContextFactory<EfContext> _factory;
		public InventoryByProductIdLoader(IBatchScheduler batchScheduler, DataLoaderOptions options, IDbContextFactory<EfContext> factory) : base(batchScheduler, options)
		{
			_factory = factory;
		}

		protected override async Task<ILookup<string, ProductInventory>> LoadGroupedBatchAsync(IReadOnlyList<string> keys, CancellationToken cancellationToken)
		{
			using var _db = await _factory.CreateDbContextAsync(cancellationToken);

			var inventory = await _db.Inventory.AsNoTracking()
				.ToListAsync(cancellationToken);

			return inventory.ToLookup(x => x.ProductId!);
		}
	}
}
