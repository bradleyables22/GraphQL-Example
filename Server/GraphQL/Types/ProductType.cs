using HotChocolate.Types;
using Server.Data.Models;
using Server.GraphQL.DataLoaders;

namespace Server.GraphQL.Types
{
	public class ProductType : ObjectType<Product>
	{
		protected override void Configure(IObjectTypeDescriptor<Product> p)
		{
			//ignore parents
			p.Field(x => x.Category).Ignore();
			p.Field(x => x.Inventory) // use preloaded data
			 .Resolve(async (ctx, ct) =>
			 {
				 var shelf = ctx.Parent<Shelf>();
				 var loader = ctx.DataLoader<InventoryByProductIdLoader>();
				 return await loader.LoadAsync(shelf.Id, ct);
			 })
			 .UseFiltering()
			 .UseSorting();
		}
	}
}
