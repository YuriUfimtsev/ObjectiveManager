using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ObjectivesService.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class AddStatusValueTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "StatusValues",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StatusValues", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "StatusValues",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1L, "Создана" },
                    { 2L, "Приостановлена" },
                    { 3L, "Не достигнута" },
                    { 4L, "Достигнута" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StatusValues");
        }
    }
}
