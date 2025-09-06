using MY.FlexiCore.Core.Interfaces;

namespace MY.FlexiCore.Manager.Core.Interfaces
{
	public interface IEntityHooks
	{
		Task BeforeSaveAsync(IExecutionContext ctx);
		Task AfterSaveAsync(IExecutionContext ctx);
		Task BeforeDeleteAsync(IExecutionContext ctx);
		Task AfterDeleteAsync(IExecutionContext ctx);
	}
}
