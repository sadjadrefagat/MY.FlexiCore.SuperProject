using MY.FlexiCore.Core.Interfaces;

namespace MY.FlexiCore.Core.Entities.FieldTypes
{
	public sealed class IntegerFieldType : IDataType
	{
		public string Title => "عدد صحیح";

		public int Length => 11;

		public Enums.FieldTypes FieldType => Enums.FieldTypes.Integer;

	}
}
