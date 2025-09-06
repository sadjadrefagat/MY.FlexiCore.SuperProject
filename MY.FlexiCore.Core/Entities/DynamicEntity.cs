using System.ComponentModel;

namespace MY.FlexiCore.Core.Entities
{
	public class DynamicEntity
	{
		[Description("شناسه موجودیت")]
		public int Id { get; set; }

		[Description("عنوان موجودیت")]
		public string Title { get; set; } = "";

		[Description("نام سیستمی موجودیت")]
		public string Name { get; set; } = "";

		[Description("فیلدهای موجودیت")]
		public List<DynamicField> Fields { get; set; } = new();
	}
}
