﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace projekt.Migrations
{
    /// <inheritdoc />
    public partial class migraTest0 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Uzytkownicy",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Imie = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Nazwisko = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Uzytkownicy", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Konta",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Login = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Haslo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TypKonta = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UzytkownikId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Konta", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Konta_Uzytkownicy_UzytkownikId",
                        column: x => x.UzytkownikId,
                        principalTable: "Uzytkownicy",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Ogloszenia",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Tytul = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Opis = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DataStworzenia = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataWygasniecia = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AutorId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ogloszenia", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Ogloszenia_Uzytkownicy_AutorId",
                        column: x => x.AutorId,
                        principalTable: "Uzytkownicy",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Skrzynki",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    WlascicielId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Skrzynki", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Skrzynki_Uzytkownicy_WlascicielId",
                        column: x => x.WlascicielId,
                        principalTable: "Uzytkownicy",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Obserwacje",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UzytkownikId = table.Column<long>(type: "bigint", nullable: false),
                    OgloszenieId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Obserwacje", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Obserwacje_Ogloszenia_OgloszenieId",
                        column: x => x.OgloszenieId,
                        principalTable: "Ogloszenia",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Obserwacje_Uzytkownicy_UzytkownikId",
                        column: x => x.UzytkownikId,
                        principalTable: "Uzytkownicy",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Wiadomosci",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Tekst = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Data = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AutorId = table.Column<long>(type: "bigint", nullable: true),
                    SkrzynkaId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Wiadomosci", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Wiadomosci_Skrzynki_SkrzynkaId",
                        column: x => x.SkrzynkaId,
                        principalTable: "Skrzynki",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Wiadomosci_Uzytkownicy_AutorId",
                        column: x => x.AutorId,
                        principalTable: "Uzytkownicy",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Konta_UzytkownikId",
                table: "Konta",
                column: "UzytkownikId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Obserwacje_OgloszenieId",
                table: "Obserwacje",
                column: "OgloszenieId");

            migrationBuilder.CreateIndex(
                name: "IX_Obserwacje_UzytkownikId",
                table: "Obserwacje",
                column: "UzytkownikId");

            migrationBuilder.CreateIndex(
                name: "IX_Ogloszenia_AutorId",
                table: "Ogloszenia",
                column: "AutorId");

            migrationBuilder.CreateIndex(
                name: "IX_Skrzynki_WlascicielId",
                table: "Skrzynki",
                column: "WlascicielId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Wiadomosci_AutorId",
                table: "Wiadomosci",
                column: "AutorId");

            migrationBuilder.CreateIndex(
                name: "IX_Wiadomosci_SkrzynkaId",
                table: "Wiadomosci",
                column: "SkrzynkaId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Konta");

            migrationBuilder.DropTable(
                name: "Obserwacje");

            migrationBuilder.DropTable(
                name: "Wiadomosci");

            migrationBuilder.DropTable(
                name: "Ogloszenia");

            migrationBuilder.DropTable(
                name: "Skrzynki");

            migrationBuilder.DropTable(
                name: "Uzytkownicy");
        }
    }
}
