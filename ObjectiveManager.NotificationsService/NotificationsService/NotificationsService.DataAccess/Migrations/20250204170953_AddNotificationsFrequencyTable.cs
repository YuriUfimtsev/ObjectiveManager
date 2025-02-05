using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace NotificationsService.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class AddNotificationsFrequencyTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FrequencyValues",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    IntervalInHours = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FrequencyValues", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "FrequencyValues",
                columns: new[] { "Id", "IntervalInHours", "Name" },
                values: new object[,]
                {
                    { 1L, 24L, "Раз в день" },
                    { 2L, 168L, "Раз в неделю" },
                    { 3L, 336L, "Раз в две недели" },
                    { 4L, 672L, "Раз в четыре недели" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FrequencyValues");
        }
    }
}
