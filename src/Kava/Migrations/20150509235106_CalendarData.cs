using System.Collections.Generic;
using Microsoft.Data.Entity.Relational.Migrations;
using Microsoft.Data.Entity.Relational.Migrations.Builders;
using Microsoft.Data.Entity.Relational.Migrations.Operations;

namespace Kava.Migrations
{
    public partial class CalendarData : Migration
    {
        public override void Up(MigrationBuilder migration)
        {
            migration.CreateTable(
                name: "Event",
                columns: table => new
                {
                    CreatedByUserId = table.Column(type: "nvarchar(450)", nullable: true),
                    EndDateTime = table.Column(type: "datetimeoffset", nullable: false),
                    EventDescription = table.Column(type: "nvarchar(max)", nullable: true),
                    EventID = table.Column(type: "uniqueidentifier", nullable: false),
                    EventName = table.Column(type: "nvarchar(max)", nullable: true),
                    MaxSlots = table.Column(type: "int", nullable: false),
                    RepeatInterval = table.Column(type: "int", nullable: false),
                    Repeats = table.Column(type: "bit", nullable: false),
                    StartDateTime = table.Column(type: "datetimeoffset", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Event", x => x.EventID);
                    table.ForeignKey(
                        name: "FK_Event_AspNetUsers_CreatedByUserId",
                        columns: x => x.CreatedByUserId,
                        referencedTable: "AspNetUsers",
                        referencedColumn: "Id");
                });
            migration.AddColumn(
                name: "EventEventID",
                table: "AspNetUsers",
                type: "uniqueidentifier",
                nullable: true);
            migration.AddForeignKey(
                name: "FK_AspNetUsers_Event_EventEventID",
                table: "AspNetUsers",
                column: "EventEventID",
                referencedTable: "Event",
                referencedColumn: "EventID");
        }
        
        public override void Down(MigrationBuilder migration)
        {
            migration.DropForeignKey(name: "FK_AspNetUsers_Event_EventEventID", table: "AspNetUsers");
            migration.DropColumn(name: "EventEventID", table: "AspNetUsers");
            migration.DropTable("Event");
        }
    }
}
