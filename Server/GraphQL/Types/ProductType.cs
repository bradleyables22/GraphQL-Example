using HotChocolate.Types;
using Server.Data.Models;

namespace Server.GraphQL.Types
{
	public class ProductType : ObjectType<Product>
	{
		protected override void Configure(IObjectTypeDescriptor<Product> p)
		{
			//ignore parents
			p.Field(x => x.Category).Ignore();
			p.Field(x => x.Shelf).Ignore();
		}
	}
}
