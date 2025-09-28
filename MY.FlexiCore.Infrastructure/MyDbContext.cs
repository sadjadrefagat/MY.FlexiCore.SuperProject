using Microsoft.EntityFrameworkCore;
using MY.FlexiCore.Core.Entities;
using MY.FlexiCore.Core.Enums;

namespace MY.FlexiCore.Infrastructure
{
	public class MyDbContext : DbContext
	{
		public MyDbContext(DbContextOptions<MyDbContext> options) : base(options) { }

		public DbSet<DynamicMasterEntity> DynamicMasterEntity { get; set; }
		public DbSet<ExecutionLog> ExecutionLogs { get; set; }


		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			var baseType = typeof(BaseDataType);

			var dataTypes = baseType.Assembly.GetTypes()
				.Where(t => baseType.IsAssignableFrom(t) && !t.IsAbstract)
				.ToList();

			// ایجاد Discriminator روی BaseDataType
			var entity = modelBuilder.Entity<BaseDataType>();
			var discriminator = entity.HasDiscriminator<string>("Discriminator");

			foreach (var type in dataTypes)

				discriminator.HasValue(type, type.Name);
		}
	}
}
