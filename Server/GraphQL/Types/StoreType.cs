using HotChocolate.Types;
using Server.Data.Models;
using Server.GraphQL.DataLoaders;

namespace Server.GraphQL.Types
{
	public class StoreType:ObjectType<Store>
	{
		protected override void Configure(IObjectTypeDescriptor<Store> s)
		{
			s.Field(x => x.Aisles)//ignore parent
			 .Resolve(async (ctx, ct) =>// use preloaded data
			 {
				 var store = ctx.Parent<Store>();
				 var loader = ctx.DataLoader<AislesByStoreIdLoader>();
				 return await loader.LoadAsync(store.Id, ct);
			 })
			 .UseFiltering()
			 .UseSorting();
		}
	}
}
