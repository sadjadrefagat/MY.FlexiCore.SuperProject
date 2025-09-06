namespace MY.FlexiCore.Core.Entities
{
	public class ExecutionLog
	{
		public int Id { get; set; }
		public string EntityName { get; set; } = "";
		public int? EntityId { get; set; }
		public string HookName { get; set; } = "";
		public string Message { get; set; } = "";
		public string Level { get; set; } = "Info";
		public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
	}
}
