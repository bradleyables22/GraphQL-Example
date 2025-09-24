using GreenDonut;
using Microsoft.EntityFrameworkCore;
using Server.Data;
using Server.Data.Models;

namespace Server.GraphQL.DataLoaders
{
	public class AislesByStoreIdLoader :GroupedDataLoader<string, Aisle>
	{
		private readonly IDbContextFactory<EfContext> _factory;

		public AislesByStoreIdLoader(IBatchScheduler batchScheduler, DataLoaderOptions options, IDbContextFactory<EfContext> factory) : base(batchScheduler, options)
		{
			_factory = factory;
		}

		protected override async Task<ILookup<string, Aisle>> LoadGroupedBatchAsync(IReadOnlyList<string> keys, CancellationToken cancellationToken)
		{
			using var _db = await _factory.CreateDbContextAsync(cancellationToken);

			var aisles = await _db.Aisles.AsNoTracking()
				.ToListAsync(cancellationToken);

			return aisles.ToLookup(x => x.StoreId!);

		}
	}
}
