using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace CryptoTaxCalculator.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CoinModel",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Cap24hrChange = table.Column<decimal>(nullable: false),
                    MarketCap = table.Column<long>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Percent = table.Column<decimal>(nullable: false),
                    Price = table.Column<double>(nullable: false),
                    Shapeshift = table.Column<bool>(nullable: false),
                    Supply = table.Column<long>(nullable: false),
                    Symbol = table.Column<string>(nullable: true),
                    TimeStamp = table.Column<DateTime>(nullable: false),
                    UsdVolume = table.Column<long>(nullable: false),
                    Volume = table.Column<long>(nullable: false),
                    VwapData = table.Column<double>(nullable: false),
                    VwapDataBtc = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CoinModel", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CoinModel");
        }
    }
}
