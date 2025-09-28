namespace MY.FlexiCore.Core.Entities.FieldTypes
{
	sealed public class BooleanFieldType : BaseDataType
	{
		public override string Title => "منطقی";

		public override int Length => 1;

		public override Enums.FieldTypes FieldType => Enums.FieldTypes.Boolean;
	}
}
