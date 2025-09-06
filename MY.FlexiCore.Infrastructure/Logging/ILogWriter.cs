namespace MY.FlexiCore.Infrastructure.Logging
{
	public interface ILogWriter
	{
		Task WriteLogAsync(string entityName, int? entityId, string hookName, string message, string level = "Info");
	}
}
