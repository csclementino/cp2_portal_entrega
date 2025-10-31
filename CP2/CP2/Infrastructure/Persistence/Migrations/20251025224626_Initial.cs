using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CP2.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "C2NET_TURMA",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "RAW(16)", nullable: false),
                    Identificador = table.Column<string>(type: "NVARCHAR2(20)", maxLength: 20, nullable: false),
                    Semestre = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    Turno = table.Column<string>(type: "NVARCHAR2(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_C2NET_TURMA", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "C2NET_ALUNO",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "RAW(16)", nullable: false),
                    Nome = table.Column<string>(type: "NVARCHAR2(120)", maxLength: 120, nullable: false),
                    Email = table.Column<string>(type: "NVARCHAR2(120)", maxLength: 120, nullable: false),
                    Rm = table.Column<string>(type: "NVARCHAR2(20)", maxLength: 20, nullable: false),
                    DataIngresso = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false),
                    Status = table.Column<string>(type: "NVARCHAR2(20)", maxLength: 20, nullable: false),
                    TurmaId = table.Column<Guid>(type: "RAW(16)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_C2NET_ALUNO", x => x.Id);
                    table.ForeignKey(
                        name: "FK_C2NET_ALUNO_C2NET_TURMA_TurmaId",
                        column: x => x.TurmaId,
                        principalTable: "C2NET_TURMA",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "C2NET_PROFESSOR",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "RAW(16)", nullable: false),
                    Nome = table.Column<string>(type: "NVARCHAR2(120)", maxLength: 120, nullable: false),
                    Email = table.Column<string>(type: "NVARCHAR2(120)", maxLength: 120, nullable: false),
                    Pf = table.Column<string>(type: "NVARCHAR2(20)", maxLength: 20, nullable: false),
                    TurmaId = table.Column<Guid>(type: "RAW(16)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_C2NET_PROFESSOR", x => x.Id);
                    table.ForeignKey(
                        name: "FK_C2NET_PROFESSOR_C2NET_TURMA_TurmaId",
                        column: x => x.TurmaId,
                        principalTable: "C2NET_TURMA",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_C2NET_ALUNO_Email",
                table: "C2NET_ALUNO",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_C2NET_ALUNO_TurmaId",
                table: "C2NET_ALUNO",
                column: "TurmaId");

            migrationBuilder.CreateIndex(
                name: "IX_C2NET_PROFESSOR_Email",
                table: "C2NET_PROFESSOR",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_C2NET_PROFESSOR_TurmaId",
                table: "C2NET_PROFESSOR",
                column: "TurmaId");

            migrationBuilder.CreateIndex(
                name: "IX_C2NET_TURMA_Identificador",
                table: "C2NET_TURMA",
                column: "Identificador",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "C2NET_ALUNO");

            migrationBuilder.DropTable(
                name: "C2NET_PROFESSOR");

            migrationBuilder.DropTable(
                name: "C2NET_TURMA");
        }
    }
}
