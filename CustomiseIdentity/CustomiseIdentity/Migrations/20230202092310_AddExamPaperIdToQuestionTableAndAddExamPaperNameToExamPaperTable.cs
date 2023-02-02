using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CustomiseIdentity.Migrations
{
    public partial class AddExamPaperIdToQuestionTableAndAddExamPaperNameToExamPaperTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ExamPaperName",
                table: "ExamPapers",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ExamPaperName",
                table: "ExamPapers");
        }
    }
}
