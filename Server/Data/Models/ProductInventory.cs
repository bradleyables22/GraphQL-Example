using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Server.Data.Models
{
	public class ProductInventory
	{
		[Key]
		public string Id { get; set; } = Guid.CreateVersion7().ToString();
		public DateTimeOffset DB_CreatedAt { get; set; } = DateTimeOffset.UtcNow;
		public DateTimeOffset DB_LastUpdateAt { get; set; } = DateTimeOffset.UtcNow;

		[ForeignKey("Shelf")]
		public string? ShelfId { get; set; }

		public Shelf? Shelf { get; set; }
		[ForeignKey("Product")]
		public string? ProductId { get; set; }

		public Product? Product { get; set; }

		[Range(0,100)]
		public int OnHand { get; set; }
	}
}
