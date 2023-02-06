using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CustomiseIdentity.Migrations
{
    public partial class MCQOptionsOfQuestionPropertyRenamedInMCQOptionClass : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "MCQOptionsOfQuestions",
                table: "MCQOptions",
                newName: "MCQOptionsOfQuestion");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "MCQOptionsOfQuestion",
                table: "MCQOptions",
                newName: "MCQOptionsOfQuestions");
        }
    }
}
