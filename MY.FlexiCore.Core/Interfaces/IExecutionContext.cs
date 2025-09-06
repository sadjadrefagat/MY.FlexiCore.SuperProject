namespace MY.FlexiCore.Core.Interfaces
{
	public interface IExecutionContext
	{
		IQueryable<T> Query<T>() where T : class;
		Task<T?> FindAsync<T>(int id) where T : class;
		Task SaveAsync<T>(T entity) where T : class;
		object CurrentEntity { get; }
		string CurrentUser { get; }
		void Log(string message, string level = "Info");
	}
}
