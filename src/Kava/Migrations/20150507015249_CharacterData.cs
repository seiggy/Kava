using System.Collections.Generic;
using Microsoft.Data.Entity.Relational.Migrations;
using Microsoft.Data.Entity.Relational.Migrations.Builders;
using Microsoft.Data.Entity.Relational.Migrations.Operations;

namespace Kava.Migrations
{
    public partial class CharacterData : Migration
    {
        public override void Up(MigrationBuilder migration)
        {
            migration.AddColumn(
                name: "CharacterName",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);
            migration.AddColumn(
                name: "Name",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);
            migration.AddColumn(
                name: "Server",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);
        }
        
        public override void Down(MigrationBuilder migration)
        {
            migration.DropColumn(name: "CharacterName", table: "AspNetUsers");
            migration.DropColumn(name: "Name", table: "AspNetUsers");
            migration.DropColumn(name: "Server", table: "AspNetUsers");
        }
    }
}
