using Microsoft.EntityFrameworkCore;
using Server.Data.Models;
using System;

namespace Server.Data
{
	public class EfContext : DbContext
	{
		public EfContext(DbContextOptions<EfContext> options) : base(options) { }
		
		public virtual DbSet<Store> Stores { get; set; }
		public virtual DbSet<Aisle> Aisles { get; set; }
		public virtual DbSet<Bay> Bays { get; set; }
		public virtual DbSet<Shelf> Shelves { get; set; }
		public virtual DbSet<Product> Products { get; set; }
		public virtual DbSet<Category> Categories { get; set; }

	}
}
