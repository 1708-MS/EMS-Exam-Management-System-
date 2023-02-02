using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CustomiseIdentity.Migrations
{
    public partial class NullAllowChangesInAllModels : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Answers_AnswerSheets_AnswerSheetId",
                table: "Answers");

            migrationBuilder.DropForeignKey(
                name: "FK_Answers_Questions_QuestionId",
                table: "Answers");

            migrationBuilder.DropForeignKey(
                name: "FK_AnswerSheets_ExamPapers_ExamPaperId",
                table: "AnswerSheets");

            migrationBuilder.DropForeignKey(
                name: "FK_MCQOptions_Answers_AnswerId",
                table: "MCQOptions");

            migrationBuilder.DropIndex(
                name: "IX_Answers_QuestionId",
                table: "Answers");

            migrationBuilder.AlterColumn<int>(
                name: "AnswerId",
                table: "Questions",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "AnswerId",
                table: "MCQOptions",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "SubjectId",
                table: "ExamPapers",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "ExamPaperId",
                table: "AnswerSheets",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "QuestionId",
                table: "Answers",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "AnswerSheetId",
                table: "Answers",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<float>(
                name: "AnswerMarks",
                table: "Answers",
                type: "real",
                nullable: true,
                oldClrType: typeof(float),
                oldType: "real");

            migrationBuilder.CreateIndex(
                name: "IX_Answers_QuestionId",
                table: "Answers",
                column: "QuestionId",
                unique: true,
                filter: "[QuestionId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Answers_AnswerSheets_AnswerSheetId",
                table: "Answers",
                column: "AnswerSheetId",
                principalTable: "AnswerSheets",
                principalColumn: "AnswerSheetId");

            migrationBuilder.AddForeignKey(
                name: "FK_Answers_Questions_QuestionId",
                table: "Answers",
                column: "QuestionId",
                principalTable: "Questions",
                principalColumn: "QuestionId");

            migrationBuilder.AddForeignKey(
                name: "FK_AnswerSheets_ExamPapers_ExamPaperId",
                table: "AnswerSheets",
                column: "ExamPaperId",
                principalTable: "ExamPapers",
                principalColumn: "ExamPaperId");

            migrationBuilder.AddForeignKey(
                name: "FK_MCQOptions_Answers_AnswerId",
                table: "MCQOptions",
                column: "AnswerId",
                principalTable: "Answers",
                principalColumn: "AnswerId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Answers_AnswerSheets_AnswerSheetId",
                table: "Answers");

            migrationBuilder.DropForeignKey(
                name: "FK_Answers_Questions_QuestionId",
                table: "Answers");

            migrationBuilder.DropForeignKey(
                name: "FK_AnswerSheets_ExamPapers_ExamPaperId",
                table: "AnswerSheets");

            migrationBuilder.DropForeignKey(
                name: "FK_MCQOptions_Answers_AnswerId",
                table: "MCQOptions");

            migrationBuilder.DropIndex(
                name: "IX_Answers_QuestionId",
                table: "Answers");

            migrationBuilder.AlterColumn<int>(
                name: "AnswerId",
                table: "Questions",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "AnswerId",
                table: "MCQOptions",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "SubjectId",
                table: "ExamPapers",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ExamPaperId",
                table: "AnswerSheets",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "QuestionId",
                table: "Answers",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "AnswerSheetId",
                table: "Answers",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<float>(
                name: "AnswerMarks",
                table: "Answers",
                type: "real",
                nullable: false,
                defaultValue: 0f,
                oldClrType: typeof(float),
                oldType: "real",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Answers_QuestionId",
                table: "Answers",
                column: "QuestionId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Answers_AnswerSheets_AnswerSheetId",
                table: "Answers",
                column: "AnswerSheetId",
                principalTable: "AnswerSheets",
                principalColumn: "AnswerSheetId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Answers_Questions_QuestionId",
                table: "Answers",
                column: "QuestionId",
                principalTable: "Questions",
                principalColumn: "QuestionId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AnswerSheets_ExamPapers_ExamPaperId",
                table: "AnswerSheets",
                column: "ExamPaperId",
                principalTable: "ExamPapers",
                principalColumn: "ExamPaperId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MCQOptions_Answers_AnswerId",
                table: "MCQOptions",
                column: "AnswerId",
                principalTable: "Answers",
                principalColumn: "AnswerId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
