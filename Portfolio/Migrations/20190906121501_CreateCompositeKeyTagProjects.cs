using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Portfolio.Migrations
{
    public partial class CreateCompositeKeyTagProjects : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TagProject_Projecten_ProjectId",
                table: "TagProject");

            migrationBuilder.DropForeignKey(
                name: "FK_TagProject_Tags_TagId",
                table: "TagProject");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TagProject",
                table: "TagProject");

            migrationBuilder.DropIndex(
                name: "IX_TagProject_TagId",
                table: "TagProject");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "TagProject");

            migrationBuilder.AlterColumn<int>(
                name: "TagId",
                table: "TagProject",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ProjectId",
                table: "TagProject",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_TagProject",
                table: "TagProject",
                columns: new[] { "TagId", "ProjectId" });

            migrationBuilder.AddForeignKey(
                name: "FK_TagProject_Projecten_ProjectId",
                table: "TagProject",
                column: "ProjectId",
                principalTable: "Projecten",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TagProject_Tags_TagId",
                table: "TagProject",
                column: "TagId",
                principalTable: "Tags",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TagProject_Projecten_ProjectId",
                table: "TagProject");

            migrationBuilder.DropForeignKey(
                name: "FK_TagProject_Tags_TagId",
                table: "TagProject");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TagProject",
                table: "TagProject");

            migrationBuilder.AlterColumn<int>(
                name: "ProjectId",
                table: "TagProject",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "TagId",
                table: "TagProject",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "TagProject",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AddPrimaryKey(
                name: "PK_TagProject",
                table: "TagProject",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_TagProject_TagId",
                table: "TagProject",
                column: "TagId");

            migrationBuilder.AddForeignKey(
                name: "FK_TagProject_Projecten_ProjectId",
                table: "TagProject",
                column: "ProjectId",
                principalTable: "Projecten",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TagProject_Tags_TagId",
                table: "TagProject",
                column: "TagId",
                principalTable: "Tags",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
