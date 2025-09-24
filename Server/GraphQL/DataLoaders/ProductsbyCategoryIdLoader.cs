using GreenDonut;
using Microsoft.EntityFrameworkCore;
using Server.Data;
using Server.Data.Models;

namespace Server.GraphQL.DataLoaders
{
	public class ProductsbyCategoryIdLoader : GroupedDataLoader<string, Data.Models.Product>
	{
		private readonly IDbContextFactory<EfContext> _factory;
		public ProductsbyCategoryIdLoader(IBatchScheduler batchScheduler, DataLoaderOptions options, IDbContextFactory<EfContext> factory) : base(batchScheduler, options)
		{
			_factory = factory;
		}

		protected override async Task<ILookup<string, Product>> LoadGroupedBatchAsync(IReadOnlyList<string> keys, CancellationToken cancellationToken)
		{
			using var _db = await _factory.CreateDbContextAsync(cancellationToken);

			var bays = await _db.Products.AsNoTracking()
				.ToListAsync(cancellationToken);

			return bays.ToLookup(x => x.CategoryId!);
		}
	}
}
