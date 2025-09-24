using MY.FlexiCore.Core.Entities;

namespace MY.FlexiCore.Infrastructure.Services
{
	static public class DynamicTableCreator<T>
		where T : DynamicMasterEntity
	{
		static public bool CreateTable(T entity)
		{
			var fields = "";
			foreach (var field in entity.HeaderFields.Union(entity.FooterFields))
			{
				//TODO:تبدیل انواع داده ای
				fields += $@"`{field.Name}` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_uca1400_ai_ci {(field.IsRequired ? "NOT NULL" : "")},";
			}
			var query = $@"
SET NAMES utf8mb4;

-- ----------------------------
-- Table structure for {entity.Name}
-- ----------------------------
CREATE TABLE `{entity.Name}` (
  `id` bigint(20) NOT NULL AUTO_INCREMENT,
  
  {fields}
  
  {(entity.HasStateMachine ? "`state` int(11) NOT NULL DEFAULT 1," : "")}
  {(entity.HasLogicalDelete ? "`isDeleted` bit(1) NOT NULL DEFAULT b'0'," : "")}
  `hasNote` bit(1) NOT NULL DEFAULT b'0',
  `hasAttachment` bit(1) NOT NULL DEFAULT b'0',
  `createionTime` datetime NOT NULL DEFAULT current_timestamp(),
  `creator` bigint(20) NOT NULL,
  `lastModificationTime` datetime NOT NULL,
  `lastModifier` bigint(20) NOT NULL,
  PRIMARY KEY (`id`) USING BTREE,

  INDEX `user_{entity.Name}_creator`(`creator` ASC) USING BTREE,
  INDEX `user_{entity.Name}_lastModifier`(`lastModifier` ASC) USING BTREE,

  CONSTRAINT `user_{entity.Name}_creator` FOREIGN KEY (`creator`) REFERENCES `user` (`id`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `user_{entity.Name}_lastModifier` FOREIGN KEY (`lastModifier`) REFERENCES `user` (`id`) ON DELETE NO ACTION ON UPDATE NO ACTION

) ENGINE = InnoDB AUTO_INCREMENT = 1 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_uca1400_ai_ci ROW_FORMAT = Dynamic;";

			return false;
		}
	}
}
