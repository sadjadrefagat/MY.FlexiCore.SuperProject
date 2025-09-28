
namespace MY.FlexiCore.Core.Entities.FieldTypes
{
	sealed public class DateTimeFieldType : BaseDataType
	{
		public override string Title => "تاریخ و ساعت";

		public override int Length => 10;

		public override Enums.FieldTypes FieldType => Enums.FieldTypes.DateTime;
	}
}
