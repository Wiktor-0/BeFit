using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BeFit.Migrations
{
    /// <inheritdoc />
    public partial class migracja3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SesjeCwiczenia_Cwiczenia_CwiczeniaId",
                table: "SesjeCwiczenia");

            migrationBuilder.DropForeignKey(
                name: "FK_SesjeCwiczenia_Sesje_SesjaId",
                table: "SesjeCwiczenia");

            migrationBuilder.DropTable(
                name: "Sesje");

            migrationBuilder.DropIndex(
                name: "IX_SesjeCwiczenia_CwiczeniaId",
                table: "SesjeCwiczenia");

            migrationBuilder.DropIndex(
                name: "IX_SesjeCwiczenia_SesjaId",
                table: "SesjeCwiczenia");

            migrationBuilder.DropIndex(
                name: "IX_Cwiczenia_Name",
                table: "Cwiczenia");

            migrationBuilder.DropColumn(
                name: "CwiczeniaId",
                table: "SesjeCwiczenia");

            migrationBuilder.DropColumn(
                name: "Powtorzenia",
                table: "SesjeCwiczenia");

            migrationBuilder.DropColumn(
                name: "Serie",
                table: "SesjeCwiczenia");

            migrationBuilder.DropColumn(
                name: "SesjaId",
                table: "SesjeCwiczenia");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Cwiczenia");

            migrationBuilder.RenameColumn(
                name: "CiezarKg",
                table: "SesjeCwiczenia",
                newName: "Start");

            migrationBuilder.AddColumn<string>(
                name: "CreatedById",
                table: "SesjeCwiczenia",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "End",
                table: "SesjeCwiczenia",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "Ciezar",
                table: "Cwiczenia",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "CreatedById",
                table: "Cwiczenia",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "ExerciseTypeId",
                table: "Cwiczenia",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Powtorzenia",
                table: "Cwiczenia",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Seria",
                table: "Cwiczenia",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SesjaCwiczeniaId",
                table: "Cwiczenia",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TypCwiczeniaId",
                table: "Cwiczenia",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "TypCwiczenia",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TypCwiczenia", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Cwiczenia_SesjaCwiczeniaId",
                table: "Cwiczenia",
                column: "SesjaCwiczeniaId");

            migrationBuilder.CreateIndex(
                name: "IX_Cwiczenia_TypCwiczeniaId",
                table: "Cwiczenia",
                column: "TypCwiczeniaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Cwiczenia_SesjeCwiczenia_SesjaCwiczeniaId",
                table: "Cwiczenia",
                column: "SesjaCwiczeniaId",
                principalTable: "SesjeCwiczenia",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Cwiczenia_TypCwiczenia_TypCwiczeniaId",
                table: "Cwiczenia",
                column: "TypCwiczeniaId",
                principalTable: "TypCwiczenia",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cwiczenia_SesjeCwiczenia_SesjaCwiczeniaId",
                table: "Cwiczenia");

            migrationBuilder.DropForeignKey(
                name: "FK_Cwiczenia_TypCwiczenia_TypCwiczeniaId",
                table: "Cwiczenia");

            migrationBuilder.DropTable(
                name: "TypCwiczenia");

            migrationBuilder.DropIndex(
                name: "IX_Cwiczenia_SesjaCwiczeniaId",
                table: "Cwiczenia");

            migrationBuilder.DropIndex(
                name: "IX_Cwiczenia_TypCwiczeniaId",
                table: "Cwiczenia");

            migrationBuilder.DropColumn(
                name: "CreatedById",
                table: "SesjeCwiczenia");

            migrationBuilder.DropColumn(
                name: "End",
                table: "SesjeCwiczenia");

            migrationBuilder.DropColumn(
                name: "Ciezar",
                table: "Cwiczenia");

            migrationBuilder.DropColumn(
                name: "CreatedById",
                table: "Cwiczenia");

            migrationBuilder.DropColumn(
                name: "ExerciseTypeId",
                table: "Cwiczenia");

            migrationBuilder.DropColumn(
                name: "Powtorzenia",
                table: "Cwiczenia");

            migrationBuilder.DropColumn(
                name: "Seria",
                table: "Cwiczenia");

            migrationBuilder.DropColumn(
                name: "SesjaCwiczeniaId",
                table: "Cwiczenia");

            migrationBuilder.DropColumn(
                name: "TypCwiczeniaId",
                table: "Cwiczenia");

            migrationBuilder.RenameColumn(
                name: "Start",
                table: "SesjeCwiczenia",
                newName: "CiezarKg");

            migrationBuilder.AddColumn<int>(
                name: "CwiczeniaId",
                table: "SesjeCwiczenia",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Powtorzenia",
                table: "SesjeCwiczenia",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Serie",
                table: "SesjeCwiczenia",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SesjaId",
                table: "SesjeCwiczenia",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Cwiczenia",
                type: "TEXT",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "Sesje",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    End = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Start = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sesje", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SesjeCwiczenia_CwiczeniaId",
                table: "SesjeCwiczenia",
                column: "CwiczeniaId");

            migrationBuilder.CreateIndex(
                name: "IX_SesjeCwiczenia_SesjaId",
                table: "SesjeCwiczenia",
                column: "SesjaId");

            migrationBuilder.CreateIndex(
                name: "IX_Cwiczenia_Name",
                table: "Cwiczenia",
                column: "Name");

            migrationBuilder.AddForeignKey(
                name: "FK_SesjeCwiczenia_Cwiczenia_CwiczeniaId",
                table: "SesjeCwiczenia",
                column: "CwiczeniaId",
                principalTable: "Cwiczenia",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SesjeCwiczenia_Sesje_SesjaId",
                table: "SesjeCwiczenia",
                column: "SesjaId",
                principalTable: "Sesje",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
