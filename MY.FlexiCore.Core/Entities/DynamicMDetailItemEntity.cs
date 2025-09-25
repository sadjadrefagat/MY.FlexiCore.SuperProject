using System.ComponentModel;

namespace MY.FlexiCore.Core.Entities
{
	public class DynamicDetailItemEntity : BaseDynamicEntity
	{
		[Description("فیلدهای موجودیت")]
		public IEnumerable<DynamicField> Fields { get; set; }
	}
}
