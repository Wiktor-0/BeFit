using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BeFit.Migrations
{
    /// <inheritdoc />
    public partial class migracja4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cwiczenia_TypCwiczenia_TypCwiczeniaId",
                table: "Cwiczenia");

            migrationBuilder.DropColumn(
                name: "ExerciseTypeId",
                table: "Cwiczenia");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "TypCwiczenia",
                newName: "Nazwa");

            migrationBuilder.AlterColumn<int>(
                name: "TypCwiczeniaId",
                table: "Cwiczenia",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "AspNetUsers",
                type: "TEXT",
                maxLength: 13,
                nullable: false,
                defaultValue: "");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "500", null, "Admin", "ADMIN" });

            migrationBuilder.CreateIndex(
                name: "IX_SesjeCwiczenia_CreatedById",
                table: "SesjeCwiczenia",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_Cwiczenia_CreatedById",
                table: "Cwiczenia",
                column: "CreatedById");

            migrationBuilder.AddForeignKey(
                name: "FK_Cwiczenia_AspNetUsers_CreatedById",
                table: "Cwiczenia",
                column: "CreatedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Cwiczenia_TypCwiczenia_TypCwiczeniaId",
                table: "Cwiczenia",
                column: "TypCwiczeniaId",
                principalTable: "TypCwiczenia",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SesjeCwiczenia_AspNetUsers_CreatedById",
                table: "SesjeCwiczenia",
                column: "CreatedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cwiczenia_AspNetUsers_CreatedById",
                table: "Cwiczenia");

            migrationBuilder.DropForeignKey(
                name: "FK_Cwiczenia_TypCwiczenia_TypCwiczeniaId",
                table: "Cwiczenia");

            migrationBuilder.DropForeignKey(
                name: "FK_SesjeCwiczenia_AspNetUsers_CreatedById",
                table: "SesjeCwiczenia");

            migrationBuilder.DropIndex(
                name: "IX_SesjeCwiczenia_CreatedById",
                table: "SesjeCwiczenia");

            migrationBuilder.DropIndex(
                name: "IX_Cwiczenia_CreatedById",
                table: "Cwiczenia");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "500");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "AspNetUsers");

            migrationBuilder.RenameColumn(
                name: "Nazwa",
                table: "TypCwiczenia",
                newName: "Name");

            migrationBuilder.AlterColumn<int>(
                name: "TypCwiczeniaId",
                table: "Cwiczenia",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AddColumn<int>(
                name: "ExerciseTypeId",
                table: "Cwiczenia",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_Cwiczenia_TypCwiczenia_TypCwiczeniaId",
                table: "Cwiczenia",
                column: "TypCwiczeniaId",
                principalTable: "TypCwiczenia",
                principalColumn: "Id");
        }
    }
}
