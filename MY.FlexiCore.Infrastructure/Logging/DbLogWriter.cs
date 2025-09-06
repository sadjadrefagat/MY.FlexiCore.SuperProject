using MY.FlexiCore.Core.Entities;

namespace MY.FlexiCore.Infrastructure.Logging
{
	public class DbLogWriter : ILogWriter
	{
		private readonly MyDbContext _db;

		public DbLogWriter(MyDbContext db)
		{
			_db = db;
		}

		public async Task WriteLogAsync(string entityName, int? entityId, string hookName, string message, string level = "Info")
		{
			var log = new ExecutionLog
			{
				EntityName = entityName,
				EntityId = entityId,
				HookName = hookName,
				Message = message,
				Level = level,
				CreatedAt = DateTime.UtcNow
			};

			_db.ExecutionLogs.Add(log);
			await _db.SaveChangesAsync();
		}
	}
}
