using System.ComponentModel;

namespace MY.FlexiCore.Core.Entities
{
	public class DynamicDetailItemEntity : BaseDynamicEntity
	{
		[Description("فیلدهای موجودیت")]
		public List<DynamicField> Fields { get; set; } = new();
	}
}
