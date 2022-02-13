using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BikeRental.Repository.Migrations
{
    public partial class InitialSeed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Bikes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Model = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Color = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Location = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PerDayRate = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    IsAvailable = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bikes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Mobile = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserRole = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BikeRatings",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Rating = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BikeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BikeRatings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BikeRatings_Bikes_BikeId",
                        column: x => x.BikeId,
                        principalTable: "Bikes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BikeRatings_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Reservations",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PerDayRate = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Total = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsCancled = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    CancelReason = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BikeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reservations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Reservations_Bikes_BikeId",
                        column: x => x.BikeId,
                        principalTable: "Bikes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Reservations_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Bikes",
                columns: new[] { "Id", "Color", "CreatedDate", "IsAvailable", "IsDeleted", "Location", "Model", "ModifiedDate", "Name", "PerDayRate" },
                values: new object[,]
                {
                    { new Guid("10d9d5ae-4081-42db-ad22-b816dc83f95c"), "Red", new DateTime(2022, 2, 13, 14, 32, 1, 304, DateTimeKind.Utc).AddTicks(564), true, false, "Lahore", "2020", null, "Bike 2", 20m },
                    { new Guid("67318c6b-6b0d-447e-bff4-8f10f01f9038"), "Green", new DateTime(2022, 2, 13, 14, 32, 1, 304, DateTimeKind.Utc).AddTicks(675), true, false, "Islamabad", "2022", null, "Bike 7", 29m },
                    { new Guid("9d0d6bc7-ceba-4ea0-95ec-523d6abb9bfc"), "Blue", new DateTime(2022, 2, 13, 14, 32, 1, 304, DateTimeKind.Utc).AddTicks(623), true, false, "Islamabad", "2022", null, "Bike 5", 30m },
                    { new Guid("a18ec84e-cb5c-4f43-94ef-1bacc3e233b5"), "Black", new DateTime(2022, 2, 13, 14, 32, 1, 304, DateTimeKind.Utc).AddTicks(585), true, false, "Lahore", "2021", null, "Bike 3", 22m },
                    { new Guid("a4402ec5-5776-45d7-af9f-955a1d791bd7"), "Red", new DateTime(2022, 2, 13, 14, 32, 1, 304, DateTimeKind.Utc).AddTicks(535), true, false, "Lahore", "2020", null, "Bike 1", 20m },
                    { new Guid("be19ac72-c39d-40ac-a381-f3e8e822a330"), "Red", new DateTime(2022, 2, 13, 14, 32, 1, 304, DateTimeKind.Utc).AddTicks(604), true, false, "Islamabad", "2021", null, "Bike 4", 20m },
                    { new Guid("fd1e5955-1894-4099-9b56-b88d44719c3d"), "Blue", new DateTime(2022, 2, 13, 14, 32, 1, 304, DateTimeKind.Utc).AddTicks(655), true, false, "Lahore", "2022", null, "Bike 6", 28m }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreatedDate", "Email", "FirstName", "IsDeleted", "LastName", "Mobile", "ModifiedDate", "Password", "UserRole", "Username" },
                values: new object[,]
                {
                    { new Guid("6e65acda-ef76-4aa5-84a5-51e6ab71f416"), new DateTime(2022, 2, 13, 14, 32, 1, 303, DateTimeKind.Utc).AddTicks(9749), "admin@test.com", "Admin", false, "Manager", "+92", null, "97mm7aQpKjwGWKfNn8O2bA==", 1, "admin" },
                    { new Guid("77ded07d-b5aa-4475-9f52-1f415513a5f5"), new DateTime(2022, 2, 13, 14, 32, 1, 304, DateTimeKind.Utc).AddTicks(492), "user3@test.com", "User", false, "3", "+92", null, "xj1lmXKkp0EiWeynZIt1SA==", 2, "user3" },
                    { new Guid("8ddba530-b73a-4670-aef2-6f36f44a1021"), new DateTime(2022, 2, 13, 14, 32, 1, 304, DateTimeKind.Utc).AddTicks(211), "user2@test.com", "User", false, "2", "+92", null, "xj1lmXKkp0EiWeynZIt1SA==", 2, "user2" },
                    { new Guid("e826556b-2ebd-44bc-a55e-0fbafd638e4a"), new DateTime(2022, 2, 13, 14, 32, 1, 304, DateTimeKind.Utc).AddTicks(53), "user1@test.com", "User", false, "1", "+92", null, "xj1lmXKkp0EiWeynZIt1SA==", 2, "user1" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_BikeRatings_BikeId",
                table: "BikeRatings",
                column: "BikeId");

            migrationBuilder.CreateIndex(
                name: "IX_BikeRatings_UserId",
                table: "BikeRatings",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_BikeId",
                table: "Reservations",
                column: "BikeId");

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_UserId",
                table: "Reservations",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BikeRatings");

            migrationBuilder.DropTable(
                name: "Reservations");

            migrationBuilder.DropTable(
                name: "Bikes");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
