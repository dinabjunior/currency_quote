using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Br.Com.Company.CurrencyQuote.Data.Infraestructure.Data.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "dbo");

            migrationBuilder.CreateTable(
                name: "segment_rate",
                schema: "dbo",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    rate = table.Column<decimal>(type: "numeric(18,4)", precision: 18, scale: 4, nullable: false),
                    segment = table.Column<int>(type: "integer", nullable: false),
                    creation_date = table.Column<DateTime>(type: "timestamp without time zone", nullable: false, defaultValueSql: "now()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_segment_rate", x => x.id);
                });

            migrationBuilder.CreateIndex(
                name: "ix_segment_rate_segment",
                schema: "dbo",
                table: "segment_rate",
                column: "segment",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "segment_rate",
                schema: "dbo");
        }
    }
}
