using HotChocolate.Types;
using Server.Data.Models;
using Server.GraphQL.DataLoaders;

namespace Server.GraphQL.Types
{
	public class ShelfType : ObjectType<Shelf>
	{
		protected override void Configure(IObjectTypeDescriptor<Shelf> s)
		{
			s.Field(x => x.Bay).Ignore(); //ignore parent
			s.Field(x => x.Products) // use preloaded data
			 .Resolve(async (ctx, ct) =>
			 {
				 var shelf = ctx.Parent<Shelf>();
				 var loader = ctx.DataLoader<ProductsByShelfIdLoader>();
				 return await loader.LoadAsync(shelf.Id, ct);
			 })
			 .UsePaging()
			 .UseFiltering()
			 .UseSorting();
		}
	}
}
