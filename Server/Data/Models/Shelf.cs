using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Server.Data.Models
{
	public class Shelf
	{
		[Key]
		public string Id { get; set; } = Guid.CreateVersion7().ToString();
		[Required]
		[MinLength(5, ErrorMessage = "Shelf alias must be longer than 5 characters")]
		[MaxLength(255, ErrorMessage = "Shelf alias must be no longer than 255 characters")]
		public string Alias { get; set; } = string.Empty;
		public DateTimeOffset DB_CreatedAt { get; set; } = DateTimeOffset.UtcNow;
		public DateTimeOffset DB_LastUpdateAt { get; set; } = DateTimeOffset.UtcNow;

		[ForeignKey("Bay")]
		public string? BayId { get; set; }

		public Bay? Bay { get; set; }

		public ICollection<Product>? Products { get; set; }
	}
}
