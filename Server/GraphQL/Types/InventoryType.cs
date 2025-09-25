using Server.Data.Models;

namespace Server.GraphQL.Types
{
	public class InventoryType : ObjectType<ProductInventory>
	{
		protected override void Configure(IObjectTypeDescriptor<ProductInventory> p)
		{
			//ignore parent
			p.Field(x => x.Shelf).Ignore();
		}
	}
}
