using MY.FlexiCore.Core.Interfaces;
using System.ComponentModel;

namespace MY.FlexiCore.Core.Entities
{
	public class DynamicField
	{
		[Description("شناسه فیلد")]
		public long Id { get; set; }

		[Description("عنوان فیلد")]
		required public string Title { get; set; }

		[Description("نام سیستمی فیلد")]
		required public string Name { get; set; }

		[Description("نوع فیلد")]
		required public IDataType DataType { get; set; }

		[Description("فیلد اجباری")]
		public bool IsRequired { get; set; } = false;
	}
}
