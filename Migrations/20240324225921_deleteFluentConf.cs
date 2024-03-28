using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Reddit.Migrations
{
    /// <inheritdoc />
    public partial class deleteFluentConf : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Communities_Users_OwnerId",
                table: "Communities");

            migrationBuilder.DropForeignKey(
                name: "FK_CommunitySubscriptions_Communities_SubscribedCommunitiesId",
                table: "CommunitySubscriptions");

            migrationBuilder.DropForeignKey(
                name: "FK_CommunitySubscriptions_Users_SubscribersId",
                table: "CommunitySubscriptions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CommunitySubscriptions",
                table: "CommunitySubscriptions");

            migrationBuilder.RenameTable(
                name: "CommunitySubscriptions",
                newName: "CommunityUser");

            migrationBuilder.RenameIndex(
                name: "IX_CommunitySubscriptions_SubscribersId",
                table: "CommunityUser",
                newName: "IX_CommunityUser_SubscribersId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CommunityUser",
                table: "CommunityUser",
                columns: new[] { "SubscribedCommunitiesId", "SubscribersId" });

            migrationBuilder.AddForeignKey(
                name: "FK_Communities_Users_OwnerId",
                table: "Communities",
                column: "OwnerId",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CommunityUser_Communities_SubscribedCommunitiesId",
                table: "CommunityUser",
                column: "SubscribedCommunitiesId",
                principalTable: "Communities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CommunityUser_Users_SubscribersId",
                table: "CommunityUser",
                column: "SubscribersId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Communities_Users_OwnerId",
                table: "Communities");

            migrationBuilder.DropForeignKey(
                name: "FK_CommunityUser_Communities_SubscribedCommunitiesId",
                table: "CommunityUser");

            migrationBuilder.DropForeignKey(
                name: "FK_CommunityUser_Users_SubscribersId",
                table: "CommunityUser");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CommunityUser",
                table: "CommunityUser");

            migrationBuilder.RenameTable(
                name: "CommunityUser",
                newName: "CommunitySubscriptions");

            migrationBuilder.RenameIndex(
                name: "IX_CommunityUser_SubscribersId",
                table: "CommunitySubscriptions",
                newName: "IX_CommunitySubscriptions_SubscribersId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CommunitySubscriptions",
                table: "CommunitySubscriptions",
                columns: new[] { "SubscribedCommunitiesId", "SubscribersId" });

            migrationBuilder.AddForeignKey(
                name: "FK_Communities_Users_OwnerId",
                table: "Communities",
                column: "OwnerId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_CommunitySubscriptions_Communities_SubscribedCommunitiesId",
                table: "CommunitySubscriptions",
                column: "SubscribedCommunitiesId",
                principalTable: "Communities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CommunitySubscriptions_Users_SubscribersId",
                table: "CommunitySubscriptions",
                column: "SubscribersId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
