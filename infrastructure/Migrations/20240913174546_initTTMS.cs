using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class initTTMS : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TTMS",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Sarjam = table.Column<bool>(type: "bit", nullable: true),
                    IsHagholAmalKari = table.Column<bool>(type: "bit", nullable: true),
                    KalaType = table.Column<int>(type: "int", nullable: true),
                    KalaKhadamatName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    KalaCode = table.Column<int>(type: "int", nullable: true),
                    BargashtType = table.Column<bool>(type: "bit", nullable: true),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    MaliatArzeshAfzoodeh = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AvarezArzeshAfzoodeh = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SayerAvarez = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TakhfifPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    HCKharidarTypeCode = table.Column<int>(type: "int", nullable: true),
                    FactorNo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FactorDate = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SanadNO = table.Column<long>(type: "bigint", nullable: true),
                    SanadDate = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TTMS", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TTMS");
        }
    }
}
