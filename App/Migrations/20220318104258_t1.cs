using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace App.Migrations
{
    public partial class t1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "sp_roles",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false),
                    user_access = table.Column<string>(type: "text", nullable: true),
                    name = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    status = table.Column<int>(type: "integer", nullable: false),
                    create_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    update_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    create_user = table.Column<int>(type: "integer", nullable: false),
                    update_user = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_sp_roles", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "tb_datas",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    data_model = table.Column<string>(type: "json", nullable: false),
                    name = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    status = table.Column<int>(type: "integer", nullable: false),
                    create_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    update_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    create_user = table.Column<int>(type: "integer", nullable: false),
                    update_user = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_tb_datas", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "tb_users",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    sur_name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    patronymic = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    login = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: true),
                    password = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    role_id = table.Column<int>(type: "integer", nullable: false),
                    phone = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                    status = table.Column<int>(type: "integer", nullable: false),
                    create_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    update_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    create_user = table.Column<int>(type: "integer", nullable: false),
                    update_user = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_tb_users", x => x.id);
                    table.ForeignKey(
                        name: "fk_tb_users_sp_roles_role_id",
                        column: x => x.role_id,
                        principalTable: "sp_roles",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "ix_tb_users_login",
                table: "tb_users",
                column: "login",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_tb_users_role_id",
                table: "tb_users",
                column: "role_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tb_datas");

            migrationBuilder.DropTable(
                name: "tb_users");

            migrationBuilder.DropTable(
                name: "sp_roles");
        }
    }
}
