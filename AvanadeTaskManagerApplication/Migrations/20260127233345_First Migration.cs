using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AvanadeTaskManagerApplication.Migrations
{
    /// <inheritdoc />
    public partial class FirstMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TaskManagerTasks",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    title = table.Column<string>(type: "nvarchar(200)", nullable: true),
                    description = table.Column<string>(type: "nvarchar(400)", nullable: true),
                    category = table.Column<string>(type: "nvarchar(20)", nullable: true),
                    priority = table.Column<string>(type: "nvarchar(20)", nullable: true),
                    status = table.Column<string>(type: "nvarchar(20)", nullable: true),
                    dueDate = table.Column<string>(type: "nvarchar(20)", nullable: true),
                    createdAt = table.Column<string>(type: "nvarchar(20)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaskManagerTasks", x => x.id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TaskManagerTasks");
        }
    }
}
