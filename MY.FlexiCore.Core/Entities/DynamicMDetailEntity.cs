using System.ComponentModel;

namespace MY.FlexiCore.Core.Entities
{
	public class DynamicDetailEntity : BaseDynamicEntity
	{
		[Description("فیلدهای موجودیت")]
		required public IEnumerable<DynamicField> Fields { get; set; }

		[Description("اقلام")]
		required public IEnumerable<DynamicDetailItemEntity> Items { get; set; }
	}
}
