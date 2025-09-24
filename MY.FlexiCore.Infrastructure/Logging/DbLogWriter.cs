using MY.FlexiCore.Core.Entities;
using MY.FlexiCore.Infrastructure;
using System;
using System.Threading.Tasks;

namespace MY.FlexiCore.Infrastructure.Logging
{
	public class DbLogWriter : ILogWriter
	{
		private readonly MyDbContext _db;

		public DbLogWriter(MyDbContext db)
		{
			_db = db;
		}

		public async Task WriteLogAsync(string entityName, int? entityId, string hookName, string message, string level, DateTime? createdAt = null) // nullable
		{
			var log = new ExecutionLog
			{
				EntityName = entityName,
				EntityId = entityId,
				HookName = hookName,
				Message = message,
				Level = level,
				CreatedAt = createdAt ?? TimeZoneInfo.ConvertTimeBySystemTimeZoneId(DateTime.UtcNow, "Asia/Tehran")
			};

			_db.ExecutionLogs.Add(log);
			await _db.SaveChangesAsync();
		}

	}
}
