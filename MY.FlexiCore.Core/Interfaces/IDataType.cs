namespace MY.FlexiCore.Core.Interfaces
{
	public interface IDataType
	{
		string Title { get; }
		int Length { get; }
		Enums.FieldTypes FieldType { get; }
	}
}
