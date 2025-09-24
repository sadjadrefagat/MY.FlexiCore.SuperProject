using System.ComponentModel;

namespace MY.FlexiCore.Core.Entities
{
	public class BaseDynamicEntity
	{
		[Description("شناسه موجودیت")]
		public int Id { get; set; }

		[Description("عنوان موجودیت")]
		public string Title { get; set; } = "";

		[Description("نام سیستمی موجودیت")]
		public string Name { get; set; } = "";

		[Description("دارای ماشین وضعیت")]
		public bool HasStateMachine { get; set; }

		[Description("حذف منطقی")]
		public bool HasLogicalDelete { get; set; }
	}
}
