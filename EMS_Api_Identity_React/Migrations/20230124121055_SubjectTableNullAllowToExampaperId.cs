using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EMSApiIdentityReact.Migrations
{
    /// <inheritdoc />
    public partial class SubjectTableNullAllowToExampaperId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Subjects_ExamPapers_ExamPaperId",
                table: "Subjects");

            migrationBuilder.DropIndex(
                name: "IX_Subjects_ExamPaperId",
                table: "Subjects");

            migrationBuilder.AlterColumn<int>(
                name: "ExamPaperId",
                table: "Subjects",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_Subjects_ExamPaperId",
                table: "Subjects",
                column: "ExamPaperId",
                unique: true,
                filter: "[ExamPaperId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Subjects_ExamPapers_ExamPaperId",
                table: "Subjects",
                column: "ExamPaperId",
                principalTable: "ExamPapers",
                principalColumn: "ExamPaperId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Subjects_ExamPapers_ExamPaperId",
                table: "Subjects");

            migrationBuilder.DropIndex(
                name: "IX_Subjects_ExamPaperId",
                table: "Subjects");

            migrationBuilder.AlterColumn<int>(
                name: "ExamPaperId",
                table: "Subjects",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Subjects_ExamPaperId",
                table: "Subjects",
                column: "ExamPaperId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Subjects_ExamPapers_ExamPaperId",
                table: "Subjects",
                column: "ExamPaperId",
                principalTable: "ExamPapers",
                principalColumn: "ExamPaperId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
