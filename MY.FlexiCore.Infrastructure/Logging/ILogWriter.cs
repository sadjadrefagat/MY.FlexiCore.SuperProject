namespace MY.FlexiCore.Infrastructure.Logging
{
	public interface ILogWriter
	{
		// زمان به صورت Nullable و با مقدار پیش‌فرض
		Task WriteLogAsync(
			string entityName,
			int? entityId,
			string hookName,
			string message,
			string level,
			DateTime? createdAt = null);
	}
}
