using MY.FlexiCore.Core.Entities;

namespace MY.FlexiCore.Core.Interfaces
{
	public interface IDatabaseEngine
	{
		string Name { get; }
		string ConnectionString { get; }

		string GetFieldType<T>(T type)
			where T : IDataType;

		string GetCreateTableQuery<T>(T entity)
			where T : DynamicMasterEntity;
	}
}
