using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace IFYB.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Admins",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Email = table.Column<string>(type: "text", nullable: false),
                    Password = table.Column<string>(type: "text", nullable: true),
                    Name = table.Column<string>(type: "text", nullable: true),
                    FailedLoginAtemptCount = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Admins", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ClientErrors",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    DateTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UserAgent = table.Column<string>(type: "text", nullable: false),
                    Data = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClientErrors", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Clients",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    RegistrationTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Password = table.Column<string>(type: "text", nullable: true),
                    FailedLoginAtemptCount = table.Column<int>(type: "integer", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: true),
                    Email = table.Column<string>(type: "text", nullable: false),
                    InvoiceName = table.Column<string>(type: "text", nullable: true),
                    InvoiceAddress = table.Column<string>(type: "text", nullable: true),
                    TaxNumber = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clients", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Emails",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ToEmail = table.Column<string>(type: "text", nullable: false),
                    Subject = table.Column<string>(type: "text", nullable: false),
                    Text = table.Column<string>(type: "text", nullable: false),
                    Html = table.Column<string>(type: "text", nullable: false),
                    Sent = table.Column<bool>(type: "boolean", nullable: false),
                    Created = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    RetryCount = table.Column<int>(type: "integer", nullable: false),
                    FileName = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Emails", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ServerErrors",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    DateTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Message = table.Column<string>(type: "text", nullable: false),
                    StackTrace = table.Column<string>(type: "text", nullable: true),
                    File = table.Column<string>(type: "text", nullable: true),
                    Line = table.Column<int>(type: "integer", nullable: false),
                    Source = table.Column<string>(type: "text", nullable: true),
                    HResult = table.Column<int>(type: "integer", nullable: false),
                    Data = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServerErrors", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Events",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    LogLevel = table.Column<int>(type: "integer", nullable: false),
                    ClientId = table.Column<int>(type: "integer", nullable: true),
                    AdminId = table.Column<int>(type: "integer", nullable: true),
                    DateTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Text = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Events", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Events_Admins_AdminId",
                        column: x => x.AdminId,
                        principalTable: "Admins",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Events_Clients_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Clients",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "GitAccesses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Url = table.Column<string>(type: "text", nullable: false),
                    AccessMode = table.Column<int>(type: "integer", nullable: false),
                    ClientId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GitAccesses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GitAccesses_Clients_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Clients",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Number = table.Column<int>(type: "integer", nullable: false),
                    CreationTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CreationDay = table.Column<int>(type: "integer", nullable: false),
                    Framework = table.Column<int>(type: "integer", nullable: false),
                    Version = table.Column<string>(type: "text", nullable: false),
                    ApplicationUrl = table.Column<string>(type: "text", nullable: true),
                    SpecificPlatform = table.Column<string>(type: "text", nullable: true),
                    SpecificPlatformVersion = table.Column<string>(type: "text", nullable: true),
                    ThirdPartyTool = table.Column<string>(type: "text", nullable: true),
                    BugDescription = table.Column<string>(type: "text", nullable: false),
                    State = table.Column<int>(type: "integer", nullable: false),
                    ClientId = table.Column<int>(type: "integer", nullable: false),
                    GitAccessId = table.Column<int>(type: "integer", nullable: false),
                    PaymentToken = table.Column<string>(type: "text", nullable: true),
                    StripeSessionId = table.Column<string>(type: "text", nullable: true),
                    StripeCustomerId = table.Column<string>(type: "text", nullable: true),
                    CustomerName = table.Column<string>(type: "text", nullable: true),
                    TaxId = table.Column<string>(type: "text", nullable: true),
                    TaxIdType = table.Column<string>(type: "text", nullable: true),
                    City = table.Column<string>(type: "text", nullable: true),
                    Country = table.Column<string>(type: "text", nullable: true),
                    Line1 = table.Column<string>(type: "text", nullable: true),
                    Line2 = table.Column<string>(type: "text", nullable: true),
                    PostalCode = table.Column<string>(type: "text", nullable: true),
                    AddressState = table.Column<string>(type: "text", nullable: true),
                    Currency = table.Column<string>(type: "text", nullable: true),
                    EurPriceId = table.Column<string>(type: "text", nullable: true),
                    UsdPriceId = table.Column<string>(type: "text", nullable: true),
                    EurPrice = table.Column<decimal>(type: "numeric", nullable: true),
                    UsdPrice = table.Column<decimal>(type: "numeric", nullable: true),
                    AmountTotal = table.Column<long>(type: "bigint", nullable: true),
                    AmountSubtotal = table.Column<long>(type: "bigint", nullable: true),
                    AmountTax = table.Column<long>(type: "bigint", nullable: true),
                    TaxCountry = table.Column<string>(type: "text", nullable: true),
                    TaxState = table.Column<string>(type: "text", nullable: true),
                    AutomaticTax = table.Column<string>(type: "text", nullable: true),
                    InvoiceNumber = table.Column<string>(type: "text", nullable: true),
                    PayedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Orders_Clients_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Clients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Orders_GitAccesses_GitAccessId",
                        column: x => x.GitAccessId,
                        principalTable: "GitAccesses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Images",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    OrderId = table.Column<int>(type: "integer", nullable: true),
                    Location = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Images", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Images_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Messages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ClientId = table.Column<int>(type: "integer", nullable: false),
                    OrderId = table.Column<int>(type: "integer", nullable: true),
                    DateTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    FromClient = table.Column<bool>(type: "boolean", nullable: false),
                    Text = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Messages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Messages_Clients_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Clients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Messages_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "Admins",
                columns: new[] { "Id", "Email", "FailedLoginAtemptCount", "Name", "Password" },
                values: new object[] { 1, "gabor@ifixyourbug.com", 0, null, null });

            migrationBuilder.CreateIndex(
                name: "IX_Events_AdminId",
                table: "Events",
                column: "AdminId");

            migrationBuilder.CreateIndex(
                name: "IX_Events_ClientId",
                table: "Events",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_GitAccesses_ClientId",
                table: "GitAccesses",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_Images_OrderId",
                table: "Images",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_Messages_ClientId",
                table: "Messages",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_Messages_OrderId",
                table: "Messages",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_ClientId",
                table: "Orders",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_GitAccessId",
                table: "Orders",
                column: "GitAccessId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ClientErrors");

            migrationBuilder.DropTable(
                name: "Emails");

            migrationBuilder.DropTable(
                name: "Events");

            migrationBuilder.DropTable(
                name: "Images");

            migrationBuilder.DropTable(
                name: "Messages");

            migrationBuilder.DropTable(
                name: "ServerErrors");

            migrationBuilder.DropTable(
                name: "Admins");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "GitAccesses");

            migrationBuilder.DropTable(
                name: "Clients");
        }
    }
}
