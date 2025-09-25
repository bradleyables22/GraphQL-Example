using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Server.Data.Models
{
	public class Product
	{
		[Key]
		public string Id { get; set; } = Guid.CreateVersion7().ToString();
		[Required]
		[MinLength(5, ErrorMessage = "Product name must be longer than 5 characters")]
		[MaxLength(255, ErrorMessage = "Product name must be no longer than 255 characters")]
		public string Name { get; set; } = string.Empty;
		public DateTimeOffset DB_CreatedAt { get; set; } = DateTimeOffset.UtcNow;
		public DateTimeOffset DB_LastUpdateAt { get; set; } = DateTimeOffset.UtcNow;

		[ForeignKey("Category")]
		public string? CategoryId { get; set; }

		public Category? Category { get; set; }
		public ICollection<ProductInventory>? Inventory { get; set; }

	}
}
