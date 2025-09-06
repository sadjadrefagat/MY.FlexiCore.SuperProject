using MY.FlexiCore.Manager.Core.Enums;
using System.ComponentModel;

namespace MY.FlexiCore.Core.Entities
{
	public class DynamicField
	{
		[Description("شناسه فیلد")]
		public int Id { get; set; }

		[Description("عنوان فیلد")]
		public string Title { get; set; } = "";

		[Description("نام سیستمی فیلد")]
		public string Name { get; set; } = "";

		[Description("نوع فیلد")]
		public FieldTypes DataType { get; set; } = FieldTypes.None;

		[Description("فیلد اجباری")]
		public bool IsRequired { get; set; } = false;
	}
}
