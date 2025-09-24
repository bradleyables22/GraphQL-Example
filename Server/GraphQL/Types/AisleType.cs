using HotChocolate.Types;
using Server.Data.Models;
using Server.GraphQL.DataLoaders;

namespace Server.GraphQL.Types
{
	public class AisleType : ObjectType<Aisle>
	{
		protected override void Configure(IObjectTypeDescriptor<Aisle> a)
		{
			a.Field(x => x.Store).Ignore(); //ignore parent
			a.Field(x => x.Bays) // use preloaded data
			 .Resolve(async (ctx, ct) =>
			 {
				 var aisle = ctx.Parent<Aisle>();
				 var loader = ctx.DataLoader<BaysByAisleIdLoader>();
				 return await loader.LoadAsync(aisle.Id, ct);
			 })
			 .UsePaging()
			 .UseFiltering()
			 .UseSorting();
		}
	}
}
