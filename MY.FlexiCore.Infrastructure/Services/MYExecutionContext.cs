using MY.FlexiCore.Core.Interfaces;
using MY.FlexiCore.Infrastructure;
using MY.FlexiCore.Infrastructure.Logging;

namespace MY.FlexiCore.Manager.Infrastructure.Services
{
	public class MYExecutionContext : IExecutionContext
	{
		private readonly MyDbContext _db;
		private readonly ILogWriter _logWriter;

		public object CurrentEntity { get; private set; }
		public string CurrentUser { get; private set; } = "system";

		public MYExecutionContext(MyDbContext db, ILogWriter logWriter, object currentEntity)
		{
			_db = db;
			_logWriter = logWriter;
			CurrentEntity = currentEntity;
		}

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
			_logWriter.WriteLogAsync(entityName, entityId, "Hook", message, level).Wait();
		}
	}
}
