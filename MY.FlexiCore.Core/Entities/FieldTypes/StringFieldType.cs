using MY.FlexiCore.Core.Interfaces;

namespace MY.FlexiCore.Core.Entities.FieldTypes
{
	sealed public class StringFieldType : IDataType
	{
		public string Title => "متن";

		public int Length => 255;

		public Enums.FieldTypes FieldType => Enums.FieldTypes.String;
	}
}
