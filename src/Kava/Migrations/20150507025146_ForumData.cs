using System.Collections.Generic;
using Microsoft.Data.Entity.Relational.Migrations;
using Microsoft.Data.Entity.Relational.Migrations.Builders;
using Microsoft.Data.Entity.Relational.Migrations.Operations;

namespace Kava.Migrations
{
    public partial class ForumData : Migration
    {
        public override void Up(MigrationBuilder migration)
        {
            migration.CreateTable(
                name: "ForumCategory",
                columns: table => new
                {
                    CategoryDescription = table.Column(type: "nvarchar(max)", nullable: true),
                    CategoryName = table.Column(type: "nvarchar(max)", nullable: true),
                    ForumCategoryID = table.Column(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ForumCategory", x => x.ForumCategoryID);
                });
            migration.CreateTable(
                name: "ForumTopic",
                columns: table => new
                {
                    ForumTopicID = table.Column(type: "uniqueidentifier", nullable: false),
                    LastPostDate = table.Column(type: "datetimeoffset", nullable: false),
                    OriginatingUserId = table.Column(type: "nvarchar(450)", nullable: true),
                    TopicCreatedDate = table.Column(type: "datetimeoffset", nullable: false),
                    TopicLocked = table.Column(type: "bit", nullable: false),
                    TopicTitle = table.Column(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ForumTopic", x => x.ForumTopicID);
                    table.ForeignKey(
                        name: "FK_ForumTopic_AspNetUsers_OriginatingUserId",
                        columns: x => x.OriginatingUserId,
                        referencedTable: "AspNetUsers",
                        referencedColumn: "Id");
                });
            migration.CreateTable(
                name: "ForumPost",
                columns: table => new
                {
                    ForumCategoryForumCategoryID = table.Column(type: "uniqueidentifier", nullable: true),
                    ForumPostID = table.Column(type: "uniqueidentifier", nullable: false),
                    ForumTopicForumTopicID = table.Column(type: "uniqueidentifier", nullable: true),
                    PostContent = table.Column(type: "nvarchar(max)", nullable: true),
                    PostCreatedDateTime = table.Column(type: "datetimeoffset", nullable: false),
                    PostUpdatedDateTime = table.Column(type: "datetimeoffset", nullable: false),
                    UserId = table.Column(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ForumPost", x => x.ForumPostID);
                    table.ForeignKey(
                        name: "FK_ForumPost_ForumCategory_ForumCategoryForumCategoryID",
                        columns: x => x.ForumCategoryForumCategoryID,
                        referencedTable: "ForumCategory",
                        referencedColumn: "ForumCategoryID");
                    table.ForeignKey(
                        name: "FK_ForumPost_ForumTopic_ForumTopicForumTopicID",
                        columns: x => x.ForumTopicForumTopicID,
                        referencedTable: "ForumTopic",
                        referencedColumn: "ForumTopicID");
                    table.ForeignKey(
                        name: "FK_ForumPost_AspNetUsers_UserId",
                        columns: x => x.UserId,
                        referencedTable: "AspNetUsers",
                        referencedColumn: "Id");
                });
        }
        
        public override void Down(MigrationBuilder migration)
        {
            migration.DropTable("ForumCategory");
            migration.DropTable("ForumPost");
            migration.DropTable("ForumTopic");
        }
    }
}
