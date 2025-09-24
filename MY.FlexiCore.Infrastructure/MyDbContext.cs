using Microsoft.EntityFrameworkCore;
using MY.FlexiCore.Core.Entities;

namespace MY.FlexiCore.Infrastructure
{
	public class MyDbContext : DbContext
	{
		public MyDbContext(DbContextOptions<MyDbContext> options) : base(options) { }

		public DbSet<DynamicMasterEntity> DynamicMasterEntity { get; set; }
		public DbSet<ExecutionLog> ExecutionLogs { get; set; }
	}
}
