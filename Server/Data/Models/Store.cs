using System.ComponentModel.DataAnnotations;

namespace Server.Data.Models
{
	public class Store
	{
		[Key]
		public string Id { get; set; } = Guid.CreateVersion7().ToString();
		[Required]
		[MinLength(5, ErrorMessage = "Store names must be longer than 5 characters")]
		[MaxLength(255, ErrorMessage = "Store names must be no longer than 255 characters")]
		public string Name { get; set; } = string.Empty;
		public DateTimeOffset DB_CreatedAt { get; set; } = DateTimeOffset.UtcNow;
		public DateTimeOffset DB_LastUpdateAt { get; set; } = DateTimeOffset.UtcNow;
		public ICollection<Aisle>? Aisles { get; set; }
	}
}
