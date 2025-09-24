using HotChocolate.Types;
using Server.Data.Models;
using Server.GraphQL.DataLoaders;

namespace Server.GraphQL.Types
{
	public class BayType : ObjectType<Bay>
	{
		protected override void Configure(IObjectTypeDescriptor<Bay> b)
		{
			b.Field(x => x.Aisle).Ignore(); //ignore parent
			b.Field(x => x.Shelves) // use preloaded data
			 .Resolve(async (ctx, ct) =>
			 {
				 var bay= ctx.Parent<Bay>();
				 var loader = ctx.DataLoader<ShelvesByBayIdLoader>();
				 return await loader.LoadAsync(bay.Id, ct);
			 })
			 .UseFiltering()
			 .UseSorting();
		}
	}
}
