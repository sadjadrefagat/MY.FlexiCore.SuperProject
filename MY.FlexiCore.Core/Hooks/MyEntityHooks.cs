using MY.FlexiCore.Core.Entities;
using MY.FlexiCore.Core.Interfaces;
using MY.FlexiCore.Manager.Core.Interfaces;

namespace MY.FlexiCore.Manager.Core.Hooks
{
	public class MyEntityHooks : IEntityHooks
	{
		public Task BeforeSaveAsync(IExecutionContext ctx)
		{
			var entity = ctx.CurrentEntity as DynamicEntity;
			if (entity != null)
			{
				ctx.Log($"BeforeSave برای {entity.Title}");
			}
			return Task.CompletedTask;
		}

		public Task AfterSaveAsync(IExecutionContext ctx)
		{
			var entity = ctx.CurrentEntity as DynamicEntity;
			if (entity != null)
			{
				ctx.Log($"AfterSave برای {entity.Title}");
			}
			return Task.CompletedTask;
		}

		public Task BeforeDeleteAsync(IExecutionContext ctx)
		{
			ctx.Log("BeforeDelete اجرا شد");
			return Task.CompletedTask;
		}

		public Task AfterDeleteAsync(IExecutionContext ctx)
		{
			ctx.Log("AfterDelete اجرا شد");
			return Task.CompletedTask;
		}
	}
}
