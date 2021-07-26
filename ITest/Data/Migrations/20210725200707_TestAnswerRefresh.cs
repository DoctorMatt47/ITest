using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ITest.Migrations
{
    public partial class TestAnswerRefresh : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TestAnswers_Accounts_UserId",
                table: "TestAnswers");

            migrationBuilder.DropForeignKey(
                name: "FK_Tests_Accounts_AccountId",
                table: "Tests");

            migrationBuilder.DropTable(
                name: "AnswerChoiceUserQuestionAnswer");

            migrationBuilder.DropTable(
                name: "QuestionAnswers");

            migrationBuilder.DropIndex(
                name: "IX_TestAnswers_UserId",
                table: "TestAnswers");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "TestAnswers");

            migrationBuilder.RenameColumn(
                name: "AnswerString",
                table: "AnswerChoices",
                newName: "ChoiceString");

            migrationBuilder.AlterColumn<Guid>(
                name: "AccountId",
                table: "Tests",
                type: "TEXT",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "AccountId",
                table: "TestAnswers",
                type: "TEXT",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<string>(
                name: "Answer",
                table: "TestAnswers",
                type: "TEXT",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ChoiceId",
                table: "TestAnswers",
                type: "TEXT",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "QuestionId",
                table: "TestAnswers",
                type: "TEXT",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AlterColumn<string>(
                name: "QuestionString",
                table: "Questions",
                type: "TEXT",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Password",
                table: "Accounts",
                type: "TEXT",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Mail",
                table: "Accounts",
                type: "TEXT",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Login",
                table: "Accounts",
                type: "TEXT",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_TestAnswers_AccountId",
                table: "TestAnswers",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_TestAnswers_ChoiceId",
                table: "TestAnswers",
                column: "ChoiceId");

            migrationBuilder.CreateIndex(
                name: "IX_TestAnswers_QuestionId",
                table: "TestAnswers",
                column: "QuestionId");

            migrationBuilder.AddForeignKey(
                name: "FK_TestAnswers_Accounts_AccountId",
                table: "TestAnswers",
                column: "AccountId",
                principalTable: "Accounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TestAnswers_AnswerChoices_ChoiceId",
                table: "TestAnswers",
                column: "ChoiceId",
                principalTable: "AnswerChoices",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TestAnswers_Questions_QuestionId",
                table: "TestAnswers",
                column: "QuestionId",
                principalTable: "Questions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Tests_Accounts_AccountId",
                table: "Tests",
                column: "AccountId",
                principalTable: "Accounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TestAnswers_Accounts_AccountId",
                table: "TestAnswers");

            migrationBuilder.DropForeignKey(
                name: "FK_TestAnswers_AnswerChoices_ChoiceId",
                table: "TestAnswers");

            migrationBuilder.DropForeignKey(
                name: "FK_TestAnswers_Questions_QuestionId",
                table: "TestAnswers");

            migrationBuilder.DropForeignKey(
                name: "FK_Tests_Accounts_AccountId",
                table: "Tests");

            migrationBuilder.DropIndex(
                name: "IX_TestAnswers_AccountId",
                table: "TestAnswers");

            migrationBuilder.DropIndex(
                name: "IX_TestAnswers_ChoiceId",
                table: "TestAnswers");

            migrationBuilder.DropIndex(
                name: "IX_TestAnswers_QuestionId",
                table: "TestAnswers");

            migrationBuilder.DropColumn(
                name: "AccountId",
                table: "TestAnswers");

            migrationBuilder.DropColumn(
                name: "Answer",
                table: "TestAnswers");

            migrationBuilder.DropColumn(
                name: "ChoiceId",
                table: "TestAnswers");

            migrationBuilder.DropColumn(
                name: "QuestionId",
                table: "TestAnswers");

            migrationBuilder.RenameColumn(
                name: "ChoiceString",
                table: "AnswerChoices",
                newName: "AnswerString");

            migrationBuilder.AlterColumn<Guid>(
                name: "AccountId",
                table: "Tests",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "TEXT");

            migrationBuilder.AddColumn<Guid>(
                name: "UserId",
                table: "TestAnswers",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "QuestionString",
                table: "Questions",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.AlterColumn<string>(
                name: "Password",
                table: "Accounts",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.AlterColumn<string>(
                name: "Mail",
                table: "Accounts",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.AlterColumn<string>(
                name: "Login",
                table: "Accounts",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.CreateTable(
                name: "QuestionAnswers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    CreatedDateTime = table.Column<DateTime>(type: "TEXT", nullable: false),
                    ModifiedDateTime = table.Column<DateTime>(type: "TEXT", nullable: true),
                    QuestionId = table.Column<Guid>(type: "TEXT", nullable: false),
                    UserTestAnswerId = table.Column<Guid>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuestionAnswers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_QuestionAnswers_Questions_QuestionId",
                        column: x => x.QuestionId,
                        principalTable: "Questions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_QuestionAnswers_TestAnswers_UserTestAnswerId",
                        column: x => x.UserTestAnswerId,
                        principalTable: "TestAnswers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AnswerChoiceUserQuestionAnswer",
                columns: table => new
                {
                    AnswerChoicesId = table.Column<Guid>(type: "TEXT", nullable: false),
                    QuestionAnswersId = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnswerChoiceUserQuestionAnswer", x => new { x.AnswerChoicesId, x.QuestionAnswersId });
                    table.ForeignKey(
                        name: "FK_AnswerChoiceUserQuestionAnswer_AnswerChoices_AnswerChoicesId",
                        column: x => x.AnswerChoicesId,
                        principalTable: "AnswerChoices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AnswerChoiceUserQuestionAnswer_QuestionAnswers_QuestionAnswersId",
                        column: x => x.QuestionAnswersId,
                        principalTable: "QuestionAnswers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TestAnswers_UserId",
                table: "TestAnswers",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AnswerChoiceUserQuestionAnswer_QuestionAnswersId",
                table: "AnswerChoiceUserQuestionAnswer",
                column: "QuestionAnswersId");

            migrationBuilder.CreateIndex(
                name: "IX_QuestionAnswers_QuestionId",
                table: "QuestionAnswers",
                column: "QuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_QuestionAnswers_UserTestAnswerId",
                table: "QuestionAnswers",
                column: "UserTestAnswerId");

            migrationBuilder.AddForeignKey(
                name: "FK_TestAnswers_Accounts_UserId",
                table: "TestAnswers",
                column: "UserId",
                principalTable: "Accounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Tests_Accounts_AccountId",
                table: "Tests",
                column: "AccountId",
                principalTable: "Accounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
