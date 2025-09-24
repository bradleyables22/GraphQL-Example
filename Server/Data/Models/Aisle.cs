using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Server.Data.Models
{
	public class Aisle
	{
		[Key]
		public string Id { get; set; } = Guid.CreateVersion7().ToString();
		[Required]
		[MinLength(5, ErrorMessage = "Aisle alias must be longer than 5 characters")]
		[MaxLength(255, ErrorMessage = "Aisle alias must be no longer than 255 characters")]
		public string Alias { get; set; } = string.Empty;
		public DateTimeOffset DB_CreatedAt { get; set; } = DateTimeOffset.UtcNow;
		public DateTimeOffset DB_LastUpdateAt { get; set; } = DateTimeOffset.UtcNow;

		[ForeignKey("Store")]
		public string? StoreId { get; set; }

		public Store? Store { get; set; }
		public ICollection<Bay>? Bays { get; set; }
	}
}
