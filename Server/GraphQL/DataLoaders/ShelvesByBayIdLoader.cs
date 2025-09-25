using GreenDonut;
using Microsoft.EntityFrameworkCore;
using Server.Data;
using Server.Data.Models;

namespace Server.GraphQL.DataLoaders
{
	public class ShelvesByBayIdLoader: GroupedDataLoader<string, Shelf>
	{
		private readonly IDbContextFactory<EfContext> _factory;
		public ShelvesByBayIdLoader(IBatchScheduler batchScheduler, DataLoaderOptions options, IDbContextFactory<EfContext> factory) : base(batchScheduler, options)
		{
			_factory = factory;
		}

		protected override async Task<ILookup<string, Shelf>> LoadGroupedBatchAsync(IReadOnlyList<string> keys, CancellationToken cancellationToken)
		{
			using var _db = await _factory.CreateDbContextAsync(cancellationToken);

			var shelves = await _db.Shelves.AsNoTracking()
				.ToListAsync(cancellationToken);

			return shelves.ToLookup(x => x.BayId!);
		}
	}
}
