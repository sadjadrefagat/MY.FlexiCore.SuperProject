using MY.FlexiCore.Core.Entities;
using MY.FlexiCore.Core.Interfaces;
using System.ComponentModel;
using System.Text;

namespace MY.FlexiCore.Infrastructure.SystemEntities.DtatabaseEngines
{
	sealed public class MariaDBEngine : IDatabaseEngine
	{
		public MariaDBEngine(string connStr)
		{
			ConnectionString = connStr;
		}

		public string Name => "MariaDB";

		public string ConnectionString { get; }

		public string GetCreateTableQuery<T>(T entity)
			where T : DynamicMasterEntity
		{
			var sb = new StringBuilder();

			sb.Append(_getCreateTableQuery(entity, entity.HeaderFields.Union(entity.FooterFields)));

			foreach (var detail in entity.Details)
			{
				sb.Append(_getCreateTableQuery(detail, detail.Fields, entity.Name));

				foreach (var item in detail.Items)
					sb.Append(_getCreateTableQuery(item, item.Fields, detail.Name));
			}

			return sb.ToString();
		}

		private string _getCreateTableQuery(BaseDynamicEntity entity, IEnumerable<DynamicField> fields, string masterTable = null)
		{
			var sb = new StringBuilder();

			sb.Append($"SET NAMES utf8mb4;");
			sb.Append($"-- ----------------------------");
			sb.Append($"-- Table structure for {entity.Name}");
			sb.Append($"-- ----------------------------");
			sb.Append($"CREATE TABLE `{entity.Name}` (");

			//ستون کلید اصلی جدول
			sb.Append($"`id` bigint(20) NOT NULL AUTO_INCREMENT,");

			if (masterTable != null)
				//افزودن کلید خارجی جدول بالادستی
				sb.Append($"`_masterRef` bigint(20) NOT NULL,");

			//افزودن فیلدهای اختصاصی موجودیت
			foreach (var field in fields)
				sb.Append($"`{field.Name}` {GetFieldType(field.DataType)} {(field.IsRequired ? "NOT NULL" : "")},");

			//ماشین-وضعیت
			if (entity.HasStateMachine)
				sb.Append($"`state` int(11) NOT NULL DEFAULT 1,");

			//حذف منطقی
			if (entity.HasLogicalDelete)
				sb.Append($"`isDeleted` bit(1) NOT NULL DEFAULT b'0',");

			//فیلدهای عمومی
			sb.Append($"`hasNote` bit(1) NOT NULL DEFAULT b'0',");
			sb.Append($"`hasAttachment` bit(1) NOT NULL DEFAULT b'0',");
			sb.Append($"`createionTime` datetime NOT NULL DEFAULT current_timestamp(),");
			sb.Append($"`creator` bigint(20) NOT NULL,");
			sb.Append($"`lastModificationTime` datetime NOT NULL,");
			sb.Append($"`lastModifier` bigint(20) NOT NULL,");

			//ساخت کلید اصلی جدول
			sb.Append($"PRIMARY KEY (`id`) USING BTREE,");

			//ایندکس‌های عمومی
			sb.Append($"INDEX `user_{entity.Name}_creator`(`creator` ASC) USING BTREE,");
			sb.Append($"INDEX `user_{entity.Name}_lastModifier`(`lastModifier` ASC) USING BTREE,");
			if (masterTable != null)
				sb.Append($"INDEX `{masterTable}_{entity.Name}_masterRef`(`_masterRef` ASC) USING BTREE,");

			//ارتباطات عمومی
			sb.Append($"CONSTRAINT `user_{entity.Name}_creator` FOREIGN KEY (`creator`) REFERENCES `user` (`id`) ON DELETE NO ACTION ON UPDATE NO ACTION,");
			sb.Append($"CONSTRAINT `user_{entity.Name}_lastModifier` FOREIGN KEY (`lastModifier`) REFERENCES `user` (`id`) ON DELETE NO ACTION ON UPDATE NO ACTION");
			if (masterTable != null)
				sb.Append($"CONSTRAINT `{masterTable}_{entity.Name}_masterRef` FOREIGN KEY (`_masterRef`) REFERENCES `{masterTable}` (`id`) ON DELETE NO ACTION ON UPDATE NO ACTION");

			sb.Append($") ENGINE = InnoDB AUTO_INCREMENT = 1 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_uca1400_ai_ci ROW_FORMAT = Dynamic;");

			return sb.ToString();
		}

		public string GetFieldType<T>(T type)
			where T : BaseDataType
		{
			switch (type.FieldType)
			{
				case Core.Enums.FieldTypes.Boolean:
					return $"bit({type.Length})";
				case Core.Enums.FieldTypes.DateTime:
					return "datetime";
				case Core.Enums.FieldTypes.Long:
					return $"bigint({type.Length})";
				case Core.Enums.FieldTypes.Integer:
					return $"int({type.Length})";
				case Core.Enums.FieldTypes.String:
					return $"varchar({type.Length}) CHARACTER SET utf8mb4 COLLATE utf8mb4_uca1400_ai_ci";
				default:
					throw new NotImplementedException("Unsupported type.");
			}

		}
	}
}
