using System.ComponentModel;

namespace MY.FlexiCore.Core.Entities
{
	public class DynamicDetailEntity : BaseDynamicEntity
	{
		[Description("فیلدهای موجودیت")]
		public List<DynamicField> Fields { get; set; } = new();

		[Description("اقلام")]
		public List<DynamicDetailItemEntity> Items { get; set; } = new();
	}
}
