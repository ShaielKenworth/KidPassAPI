using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EscuelaAPI.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Alumnos",
                columns: table => new
                {
                    AlumnoID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Grado = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Alumnos", x => x.AlumnoID);
                });

            migrationBuilder.CreateTable(
                name: "Profesores",
                columns: table => new
                {
                    ProfesorID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Materia = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Profesores", x => x.ProfesorID);
                });

            migrationBuilder.CreateTable(
                name: "Tutores",
                columns: table => new
                {
                    TutorID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Parentesco = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tutores", x => x.TutorID);
                });

            migrationBuilder.CreateTable(
                name: "RegistrosSalida",
                columns: table => new
                {
                    RegistroSalidaID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AlumnoID = table.Column<int>(type: "int", nullable: false),
                    ProfesorID = table.Column<int>(type: "int", nullable: false),
                    TutorID = table.Column<int>(type: "int", nullable: false),
                    ProfesorVerificado = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RegistrosSalida", x => x.RegistroSalidaID);
                    table.ForeignKey(
                        name: "FK_RegistrosSalida_Alumnos_AlumnoID",
                        column: x => x.AlumnoID,
                        principalTable: "Alumnos",
                        principalColumn: "AlumnoID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RegistrosSalida_Profesores_ProfesorID",
                        column: x => x.ProfesorID,
                        principalTable: "Profesores",
                        principalColumn: "ProfesorID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RegistrosSalida_Tutores_TutorID",
                        column: x => x.TutorID,
                        principalTable: "Tutores",
                        principalColumn: "TutorID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RegistrosSalida_AlumnoID",
                table: "RegistrosSalida",
                column: "AlumnoID");

            migrationBuilder.CreateIndex(
                name: "IX_RegistrosSalida_ProfesorID",
                table: "RegistrosSalida",
                column: "ProfesorID");

            migrationBuilder.CreateIndex(
                name: "IX_RegistrosSalida_TutorID",
                table: "RegistrosSalida",
                column: "TutorID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RegistrosSalida");

            migrationBuilder.DropTable(
                name: "Alumnos");

            migrationBuilder.DropTable(
                name: "Profesores");

            migrationBuilder.DropTable(
                name: "Tutores");
        }
    }
}
