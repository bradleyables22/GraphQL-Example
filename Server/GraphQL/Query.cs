using HotChocolate;
using Server.Data;
using Server.Data.Models;

namespace Server.GraphQL
{
	public class Query
	{
		public IQueryable<Store> GetStores([Service] EfContext _context) => _context.Stores;
		public IQueryable<Store> GetStores([Service] EfContext _context) => _context.Stores;
	}
}
