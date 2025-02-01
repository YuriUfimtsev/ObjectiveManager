using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ObjectivesService.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class AddObjectiveTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Objectives",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    Definition = table.Column<string>(type: "text", nullable: false),
                    FinalDate = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    Comment = table.Column<string>(type: "text", nullable: true),
                    StatusObjectId = table.Column<Guid>(type: "uuid", nullable: true),
                    UserId = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Objectives", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Objectives_StatusObjects_StatusObjectId",
                        column: x => x.StatusObjectId,
                        principalTable: "StatusObjects",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Objectives_StatusObjectId",
                table: "Objectives",
                column: "StatusObjectId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Objectives");
        }
    }
}
