using System.ComponentModel;

namespace MY.FlexiCore.Core.Entities
{
	public class DynamicMasterEntity : BaseDynamicEntity
	{
		[Description("فیلدهای موجودیت در بخش هدر")]
		required public IEnumerable<DynamicField> HeaderFields { get; set; }

		[Description("فیلدهای موجودیت در بخش فوتر")]
		required public IEnumerable<DynamicField> FooterFields { get; set; }

		[Description("اقلام/Detail")]
		required public IEnumerable<DynamicDetailEntity> Details { get; set; }

		[Description("منتشر شده")]
		public bool IsPublished { get; set; } = false;
	}
}
