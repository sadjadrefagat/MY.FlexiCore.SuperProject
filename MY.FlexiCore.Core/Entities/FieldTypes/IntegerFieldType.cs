namespace MY.FlexiCore.Core.Entities.FieldTypes
{
	sealed public class IntegerFieldType : BaseDataType
	{
		public IntegerFieldType()
		{
			FieldType = Enums.FieldTypes.Integer;
		}

		override public string Title => "عدد صحیح";

		override public int Length => 11;

		override public Enums.FieldTypes FieldType { get; }
	}
}
