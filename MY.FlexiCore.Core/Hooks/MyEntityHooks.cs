using MY.FlexiCore.Core.Entities;
using MY.FlexiCore.Core.Interfaces;
using MY.FlexiCore.Manager.Core.Interfaces;

namespace MY.FlexiCore.Core.Hooks
{
	public class MyEntityHooks : IEntityHooks
	{
		public async Task BeforeSaveAsync(IExecutionContext ctx)
		{
			var entity = ctx.CurrentEntity as DynamicEntity;
			if (entity != null)
			{
				ctx.Log("BeforeSave اجرا شد");
			}
		}

		public async Task AfterSaveAsync(IExecutionContext ctx)
		{
			var entity = ctx.CurrentEntity as DynamicEntity;
			if (entity != null)
			{
				ctx.Log("AfterSave اجرا شد");
			}
		}

		public async Task BeforeDeleteAsync(IExecutionContext ctx)
		{
			ctx.Log("BeforeDelete اجرا شد");
		}

		public async Task AfterDeleteAsync(IExecutionContext ctx)
		{
			ctx.Log("AfterDelete اجرا شد");
		}
	}
}
