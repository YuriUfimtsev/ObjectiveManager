using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ObjectivesService.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class AddStatusObjectTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "StatusObjects",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    ObjectiveId = table.Column<Guid>(type: "uuid", nullable: false),
                    StatusValueId = table.Column<long>(type: "bigint", nullable: false),
                    Comment = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StatusObjects", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StatusObjects_StatusValues_StatusValueId",
                        column: x => x.StatusValueId,
                        principalTable: "StatusValues",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_StatusObjects_StatusValueId",
                table: "StatusObjects",
                column: "StatusValueId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StatusObjects");
        }
    }
}
