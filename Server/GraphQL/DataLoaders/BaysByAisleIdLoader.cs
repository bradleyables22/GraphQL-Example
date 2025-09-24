using GreenDonut;
using Microsoft.EntityFrameworkCore;
using Server.Data;
using Server.Data.Models;

namespace Server.GraphQL.DataLoaders
{
	public class BaysByAisleIdLoader : GroupedDataLoader<string, Bay>
	{
		private readonly IDbContextFactory<EfContext> _factory;
		public BaysByAisleIdLoader(IBatchScheduler batchScheduler, DataLoaderOptions options,, IDbContextFactory<EfContext> factory) : base(batchScheduler, options)
		{
			_factory = factory;
		}

		protected override async Task<ILookup<string, Bay>> LoadGroupedBatchAsync(IReadOnlyList<string> keys, CancellationToken cancellationToken)
		{
			using var _db = await _factory.CreateDbContextAsync(cancellationToken);

			var bays = await _db.Bays.AsNoTracking()
				.ToListAsync(cancellationToken);

			return bays.ToLookup(x => x.AisleId!);
		}
	}
}
