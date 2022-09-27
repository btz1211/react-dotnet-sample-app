using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace server.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Houses",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    Address = table.Column<string>(type: "text", nullable: true),
                    Country = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    Price = table.Column<int>(type: "integer", nullable: false),
                    Photo = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Houses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Bids",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    HouseId = table.Column<string>(type: "text", nullable: false),
                    Bidder = table.Column<string>(type: "text", nullable: false),
                    Amount = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bids", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Bids_Houses_HouseId",
                        column: x => x.HouseId,
                        principalTable: "Houses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Houses",
                columns: new[] { "Id", "Address", "Country", "Description", "Photo", "Price" },
                values: new object[,]
                {
                    { "45eeaef0-3e72-11ed-b878-0242ac120002", "234 Dummy st, Brooklyn", "USA", "An amazing investment property opportunity in this two-family house", null, 12000000 },
                    { "45eeb0f8-3e72-11ed-b878-0242ac120002", "123 Test st, New York", "USA", "A beautiful renovated Colonial in the heart of the best City, NYC!", null, 90000 }
                });

            migrationBuilder.InsertData(
                table: "Bids",
                columns: new[] { "Id", "Amount", "Bidder", "HouseId" },
                values: new object[,]
                {
                    { "45ee9f64-3e72-11ed-b878-0242ac120002", 200000, "Sonia Reading", "45eeb0f8-3e72-11ed-b878-0242ac120002" },
                    { "45eea252-3e72-11ed-b878-0242ac120002", 202400, "Dick Johnson", "45eeb0f8-3e72-11ed-b878-0242ac120002" },
                    { "45eea39c-3e72-11ed-b878-0242ac120002", 302400, "Mohammed Vahls", "45eeaef0-3e72-11ed-b878-0242ac120002" },
                    { "45eea4d2-3e72-11ed-b878-0242ac120002", 310500, "Jane Williams", "45eeaef0-3e72-11ed-b878-0242ac120002" },
                    { "45eea73e-3e72-11ed-b878-0242ac120002", 315400, "John Kepler", "45eeaef0-3e72-11ed-b878-0242ac120002" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Bids_HouseId",
                table: "Bids",
                column: "HouseId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Bids");

            migrationBuilder.DropTable(
                name: "Houses");
        }
    }
}
