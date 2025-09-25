using Microsoft.EntityFrameworkCore;
using Server.Data;
using Server.Data.Models;

namespace Server.GraphQL
{
	public class Query
	{
		[UseFiltering, UseSorting]
		public async Task<IQueryable<Store>> Stores([Service] IDbContextFactory<EfContext> _factory) 
		{
			var _db = await _factory.CreateDbContextAsync();

			return _db.Stores.AsNoTracking();
		}
	}
}
