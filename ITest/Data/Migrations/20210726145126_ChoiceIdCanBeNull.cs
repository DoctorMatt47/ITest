using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ITest.Migrations
{
    public partial class ChoiceIdCanBeNull : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AnswerChoices_Questions_QuestionId",
                table: "AnswerChoices");

            migrationBuilder.DropForeignKey(
                name: "FK_TestAnswers_AnswerChoices_ChoiceId",
                table: "TestAnswers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AnswerChoices",
                table: "AnswerChoices");

            migrationBuilder.RenameTable(
                name: "AnswerChoices",
                newName: "Choices");

            migrationBuilder.RenameIndex(
                name: "IX_AnswerChoices_QuestionId",
                table: "Choices",
                newName: "IX_Choices_QuestionId");

            migrationBuilder.AlterColumn<Guid>(
                name: "ChoiceId",
                table: "TestAnswers",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "TEXT");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Choices",
                table: "Choices",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Choices_Questions_QuestionId",
                table: "Choices",
                column: "QuestionId",
                principalTable: "Questions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TestAnswers_Choices_ChoiceId",
                table: "TestAnswers",
                column: "ChoiceId",
                principalTable: "Choices",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Choices_Questions_QuestionId",
                table: "Choices");

            migrationBuilder.DropForeignKey(
                name: "FK_TestAnswers_Choices_ChoiceId",
                table: "TestAnswers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Choices",
                table: "Choices");

            migrationBuilder.RenameTable(
                name: "Choices",
                newName: "AnswerChoices");

            migrationBuilder.RenameIndex(
                name: "IX_Choices_QuestionId",
                table: "AnswerChoices",
                newName: "IX_AnswerChoices_QuestionId");

            migrationBuilder.AlterColumn<Guid>(
                name: "ChoiceId",
                table: "TestAnswers",
                type: "TEXT",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_AnswerChoices",
                table: "AnswerChoices",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AnswerChoices_Questions_QuestionId",
                table: "AnswerChoices",
                column: "QuestionId",
                principalTable: "Questions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TestAnswers_AnswerChoices_ChoiceId",
                table: "TestAnswers",
                column: "ChoiceId",
                principalTable: "AnswerChoices",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
