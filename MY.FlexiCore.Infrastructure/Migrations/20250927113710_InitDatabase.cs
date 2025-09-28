using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MY.FlexiCore.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitDatabase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DynamicField_DynamicEntities_DynamicEntityId",
                table: "DynamicField");

            migrationBuilder.DropTable(
                name: "DynamicEntities");

            migrationBuilder.DropIndex(
                name: "IX_DynamicField_DynamicEntityId",
                table: "DynamicField");

            migrationBuilder.DropColumn(
                name: "DynamicEntityId",
                table: "DynamicField");

            migrationBuilder.RenameColumn(
                name: "DataType",
                table: "DynamicField",
                newName: "DataTypeId");

            migrationBuilder.AlterColumn<long>(
                name: "Id",
                table: "DynamicField",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn)
                .OldAnnotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AddColumn<long>(
                name: "DynamicDetailEntityId",
                table: "DynamicField",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "DynamicDetailItemEntityId",
                table: "DynamicField",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "DynamicMasterEntityId",
                table: "DynamicField",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "DynamicMasterEntityId1",
                table: "DynamicField",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "BaseDataType",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Discriminator = table.Column<string>(type: "varchar(21)", maxLength: 21, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BaseDataType", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "DynamicMasterEntity",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    IsPublished = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    Title = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Name = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    HasStateMachine = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    HasLogicalDelete = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DynamicMasterEntity", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "DynamicDetailEntity",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    DynamicMasterEntityId = table.Column<long>(type: "bigint", nullable: true),
                    Title = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Name = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    HasStateMachine = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    HasLogicalDelete = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DynamicDetailEntity", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DynamicDetailEntity_DynamicMasterEntity_DynamicMasterEntityId",
                        column: x => x.DynamicMasterEntityId,
                        principalTable: "DynamicMasterEntity",
                        principalColumn: "Id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "DynamicDetailItemEntity",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    DynamicDetailEntityId = table.Column<long>(type: "bigint", nullable: true),
                    Title = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Name = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    HasStateMachine = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    HasLogicalDelete = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DynamicDetailItemEntity", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DynamicDetailItemEntity_DynamicDetailEntity_DynamicDetailEnt~",
                        column: x => x.DynamicDetailEntityId,
                        principalTable: "DynamicDetailEntity",
                        principalColumn: "Id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_DynamicField_DataTypeId",
                table: "DynamicField",
                column: "DataTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_DynamicField_DynamicDetailEntityId",
                table: "DynamicField",
                column: "DynamicDetailEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_DynamicField_DynamicDetailItemEntityId",
                table: "DynamicField",
                column: "DynamicDetailItemEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_DynamicField_DynamicMasterEntityId",
                table: "DynamicField",
                column: "DynamicMasterEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_DynamicField_DynamicMasterEntityId1",
                table: "DynamicField",
                column: "DynamicMasterEntityId1");

            migrationBuilder.CreateIndex(
                name: "IX_DynamicDetailEntity_DynamicMasterEntityId",
                table: "DynamicDetailEntity",
                column: "DynamicMasterEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_DynamicDetailItemEntity_DynamicDetailEntityId",
                table: "DynamicDetailItemEntity",
                column: "DynamicDetailEntityId");

            migrationBuilder.AddForeignKey(
                name: "FK_DynamicField_BaseDataType_DataTypeId",
                table: "DynamicField",
                column: "DataTypeId",
                principalTable: "BaseDataType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DynamicField_DynamicDetailEntity_DynamicDetailEntityId",
                table: "DynamicField",
                column: "DynamicDetailEntityId",
                principalTable: "DynamicDetailEntity",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_DynamicField_DynamicDetailItemEntity_DynamicDetailItemEntity~",
                table: "DynamicField",
                column: "DynamicDetailItemEntityId",
                principalTable: "DynamicDetailItemEntity",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_DynamicField_DynamicMasterEntity_DynamicMasterEntityId",
                table: "DynamicField",
                column: "DynamicMasterEntityId",
                principalTable: "DynamicMasterEntity",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_DynamicField_DynamicMasterEntity_DynamicMasterEntityId1",
                table: "DynamicField",
                column: "DynamicMasterEntityId1",
                principalTable: "DynamicMasterEntity",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DynamicField_BaseDataType_DataTypeId",
                table: "DynamicField");

            migrationBuilder.DropForeignKey(
                name: "FK_DynamicField_DynamicDetailEntity_DynamicDetailEntityId",
                table: "DynamicField");

            migrationBuilder.DropForeignKey(
                name: "FK_DynamicField_DynamicDetailItemEntity_DynamicDetailItemEntity~",
                table: "DynamicField");

            migrationBuilder.DropForeignKey(
                name: "FK_DynamicField_DynamicMasterEntity_DynamicMasterEntityId",
                table: "DynamicField");

            migrationBuilder.DropForeignKey(
                name: "FK_DynamicField_DynamicMasterEntity_DynamicMasterEntityId1",
                table: "DynamicField");

            migrationBuilder.DropTable(
                name: "BaseDataType");

            migrationBuilder.DropTable(
                name: "DynamicDetailItemEntity");

            migrationBuilder.DropTable(
                name: "DynamicDetailEntity");

            migrationBuilder.DropTable(
                name: "DynamicMasterEntity");

            migrationBuilder.DropIndex(
                name: "IX_DynamicField_DataTypeId",
                table: "DynamicField");

            migrationBuilder.DropIndex(
                name: "IX_DynamicField_DynamicDetailEntityId",
                table: "DynamicField");

            migrationBuilder.DropIndex(
                name: "IX_DynamicField_DynamicDetailItemEntityId",
                table: "DynamicField");

            migrationBuilder.DropIndex(
                name: "IX_DynamicField_DynamicMasterEntityId",
                table: "DynamicField");

            migrationBuilder.DropIndex(
                name: "IX_DynamicField_DynamicMasterEntityId1",
                table: "DynamicField");

            migrationBuilder.DropColumn(
                name: "DynamicDetailEntityId",
                table: "DynamicField");

            migrationBuilder.DropColumn(
                name: "DynamicDetailItemEntityId",
                table: "DynamicField");

            migrationBuilder.DropColumn(
                name: "DynamicMasterEntityId",
                table: "DynamicField");

            migrationBuilder.DropColumn(
                name: "DynamicMasterEntityId1",
                table: "DynamicField");

            migrationBuilder.RenameColumn(
                name: "DataTypeId",
                table: "DynamicField",
                newName: "DataType");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "DynamicField",
                type: "int",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint")
                .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn)
                .OldAnnotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AddColumn<int>(
                name: "DynamicEntityId",
                table: "DynamicField",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "DynamicEntities",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Title = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DynamicEntities", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_DynamicField_DynamicEntityId",
                table: "DynamicField",
                column: "DynamicEntityId");

            migrationBuilder.AddForeignKey(
                name: "FK_DynamicField_DynamicEntities_DynamicEntityId",
                table: "DynamicField",
                column: "DynamicEntityId",
                principalTable: "DynamicEntities",
                principalColumn: "Id");
        }
    }
}
