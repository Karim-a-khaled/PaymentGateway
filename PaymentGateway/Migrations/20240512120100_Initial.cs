using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PaymentGateway.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TrainingOrganizationInvoices",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TotalAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TrainingOrganizationId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrainingOrganizationInvoices", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Token = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TransactionInvoices",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BillAccount = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BillCycle = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DueTo = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    ExpiryDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    ReferenceInfo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TitleAr = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TitleEn = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PaymentOptions = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TrainingOrganizationInvoiceId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TransactionInvoices", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TransactionInvoices_TrainingOrganizationInvoices_TrainingOrganizationInvoiceId",
                        column: x => x.TrainingOrganizationInvoiceId,
                        principalTable: "TrainingOrganizationInvoices",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "BillSummary",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    AmountCode = table.Column<int>(type: "int", nullable: false),
                    BillId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BillSummary", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BillSummary_TransactionInvoices_BillId",
                        column: x => x.BillId,
                        principalTable: "TransactionInvoices",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "RevenueTypeEntry",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BeneficiaryAgencyId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GfsCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    BillId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RevenueTypeEntry", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RevenueTypeEntry_TransactionInvoices_BillId",
                        column: x => x.BillId,
                        principalTable: "TransactionInvoices",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_BillSummary_BillId",
                table: "BillSummary",
                column: "BillId");

            migrationBuilder.CreateIndex(
                name: "IX_RevenueTypeEntry_BillId",
                table: "RevenueTypeEntry",
                column: "BillId");

            migrationBuilder.CreateIndex(
                name: "IX_TransactionInvoices_TrainingOrganizationInvoiceId",
                table: "TransactionInvoices",
                column: "TrainingOrganizationInvoiceId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BillSummary");

            migrationBuilder.DropTable(
                name: "RevenueTypeEntry");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "TransactionInvoices");

            migrationBuilder.DropTable(
                name: "TrainingOrganizationInvoices");
        }
    }
}
