using MY.FlexiCore.Core.Entities;
using MY.FlexiCore.Core.Interfaces;

namespace MY.FlexiCore.Infrastructure.SystemEntities.DtatabaseEngines
{
	sealed public class SqlServerEngine : IDatabaseEngine
	{
		public SqlServerEngine(string connStr)
		{
			ConnectionString = connStr;
		}

		public string Name => "SqlServer";

		public string ConnectionString { get; }

		public string GetCreateTableQuery<T>(T entity) where T : DynamicMasterEntity
		{
			throw new NotImplementedException();
		}

		public string GetFieldType<T>(T type)
			where T : BaseDataType
		{
			switch (type.FieldType)
			{
				case Core.Enums.FieldTypes.Integer:
					return "INT";
				case Core.Enums.FieldTypes.String:
					return "NVARCHAR";
				default:
					return "";
			}
		}
	}
}
