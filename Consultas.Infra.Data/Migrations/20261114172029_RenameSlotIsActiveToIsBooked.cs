using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Consultas.Infra.Data.Migrations
{
    /// <inheritdoc />
    public partial class RenameSlotIsActiveToIsBooked : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "isActive",
                table: "AvailabilitySlot",
                newName: "isBooked");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "isBooked",
                table: "AvailabilitySlot",
                newName: "isActive");
        }
    }
}
