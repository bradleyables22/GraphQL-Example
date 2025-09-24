using HotChocolate.Types;
using Server.Data.Models;
using Server.GraphQL.DataLoaders;

namespace Server.GraphQL.Types
{
	public class CategoryType : ObjectType<Category>
	{
		protected override void Configure(IObjectTypeDescriptor<Category> c)
		{
			c.Field(x => x.Products) // use preloaded data
			 .Resolve(async (ctx, ct) =>
			 {
				 var cat = ctx.Parent<Category>();
				 var loader = ctx.DataLoader<ProductsbyCategoryIdLoader>();
				 return await loader.LoadAsync(cat.Id, ct);
			 })
			 .UsePaging()
			 .UseFiltering()
			 .UseSorting();
		}
	}
}
