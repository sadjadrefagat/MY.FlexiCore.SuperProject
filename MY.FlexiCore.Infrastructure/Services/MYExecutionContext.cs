using MY.FlexiCore.Core.Interfaces;
using MY.FlexiCore.Infrastructure.Logging;

namespace MY.FlexiCore.Infrastructure.Services
{
	public class MYExecutionContext : IExecutionContext
	{
		private readonly MyDbContext _db;
		private readonly ILogWriter _logWriter;

		public object CurrentEntity { get; private set; }
		public string CurrentUser { get; private set; } = "system";

		// --- Constructor: db, entity, logWriter ---
		public MYExecutionContext(MyDbContext db, object currentEntity, ILogWriter logWriter)
		{
			_db = db ?? throw new ArgumentNullException(nameof(db));
			CurrentEntity = currentEntity ?? throw new ArgumentNullException(nameof(currentEntity));
			_logWriter = logWriter ?? throw new ArgumentNullException(nameof(logWriter));
		}

		// --- IExecutionContext members ---
		public IQueryable<T> Query<T>() where T : class => _db.Set<T>();

		public async Task<T?> FindAsync<T>(int id) where T : class
		{
			return await _db.Set<T>().FindAsync(id);
		}

		public async Task SaveAsync<T>(T entity) where T : class
		{
			_db.Set<T>().Add(entity);
			await _db.SaveChangesAsync();
		}

		public void Log(string message, string level = "Info")
		{
			var entityName = CurrentEntity.GetType().Name;
			var entityIdProp = CurrentEntity.GetType().GetProperty("Id");
			int? entityId = entityIdProp?.GetValue(CurrentEntity) as int?;

			var tehranTime = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(DateTime.UtcNow, "Asia/Tehran");

			_logWriter.WriteLogAsync(entityName, entityId, "Hook", message, level, tehranTime).Wait();
		}
	}
}
