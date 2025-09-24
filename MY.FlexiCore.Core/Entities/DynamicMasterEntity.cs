using System.ComponentModel;

namespace MY.FlexiCore.Core.Entities
{
	public class DynamicMasterEntity : BaseDynamicEntity
	{
		[Description("فیلدهای موجودیت در بخش هدر")]
		public List<DynamicField> HeaderFields { get; set; } = new();

		[Description("فیلدهای موجودیت در بخش فوتر")]
		public List<DynamicField> FooterFields { get; set; } = new();

		[Description("اقلام/Detail")]
		public List<DynamicDetailEntity> Details { get; set; } = new();

		[Description("منتشر شده")]
		public bool IsPublished { get; set; } = false;
	}
}
