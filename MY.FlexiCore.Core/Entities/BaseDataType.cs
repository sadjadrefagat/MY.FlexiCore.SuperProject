using MY.FlexiCore.Core.Entities.FieldTypes;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace MY.FlexiCore.Core.Entities
{
	[JsonPolymorphic(TypeDiscriminatorPropertyName = "$type")]
	[JsonDerivedType(typeof(StringFieldType), "string")]
	[JsonDerivedType(typeof(IntegerFieldType), "number")]
	abstract public class BaseDataType
	{
		[Key]
		public int Id { get; set; }

		abstract public string Title { get; }
		abstract public int Length { get; }
		abstract public Enums.FieldTypes FieldType { get; }
	}
}
