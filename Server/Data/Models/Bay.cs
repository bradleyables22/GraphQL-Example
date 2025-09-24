using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Server.Data.Models
{
	public class Bay
	{
		[Key]
		public string Id { get; set; } = Guid.CreateVersion7().ToString();
		[Required]
		[MinLength(5, ErrorMessage = "Bay alias must be longer than 5 characters")]
		[MaxLength(255, ErrorMessage = "Bay alias must be no longer than 255 characters")]
		public string Alias { get; set; } = string.Empty;
		public DateTimeOffset DB_CreatedAt { get; set; } = DateTimeOffset.UtcNow;
		public DateTimeOffset DB_LastUpdateAt { get; set; } = DateTimeOffset.UtcNow;

		[ForeignKey("Aisle")]
		public string? AisleId { get; set; }

		public Aisle? Aisle { get; set; }

		public ICollection<Shelf>? Shelves { get; set; }
	}
}
