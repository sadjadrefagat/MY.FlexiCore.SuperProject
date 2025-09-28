namespace MY.FlexiCore.Core.Entities.FieldTypes
{
	sealed public class StringFieldType : BaseDataType
	{
		public StringFieldType()
		{
			FieldType = Enums.FieldTypes.String;
		}

		override public string Title => "متن";

		override public int Length => 255;

		override public Enums.FieldTypes FieldType { get; }
	}
}
