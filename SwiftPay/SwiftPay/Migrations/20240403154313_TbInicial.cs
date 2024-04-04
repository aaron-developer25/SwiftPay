using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SwiftPay.Migrations
{
    /// <inheritdoc />
    public partial class TbInicial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DetallePagos",
                columns: table => new
                {
                    DetallePagoId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    PagoId = table.Column<int>(type: "INTEGER", nullable: false),
                    CodigoParcela = table.Column<string>(type: "TEXT", nullable: false),
                    Tareas = table.Column<float>(type: "REAL", nullable: false),
                    TipoIrrigacion = table.Column<string>(type: "TEXT", nullable: true),
                    Periodos = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DetallePagos", x => x.DetallePagoId);
                });

            migrationBuilder.CreateTable(
                name: "DetalleRegantes",
                columns: table => new
                {
                    DetalleReganteId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ReganteId = table.Column<int>(type: "INTEGER", nullable: false),
                    CodigoParcela = table.Column<string>(type: "TEXT", nullable: false),
                    Tareas = table.Column<float>(type: "REAL", nullable: false),
                    TipoIrrigacion = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DetalleRegantes", x => x.DetalleReganteId);
                });

            migrationBuilder.CreateTable(
                name: "Empresas",
                columns: table => new
                {
                    EmpresaId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Imagen = table.Column<byte[]>(type: "BLOB", nullable: true),
                    Nombre = table.Column<string>(type: "TEXT", nullable: false),
                    RNC = table.Column<string>(type: "TEXT", nullable: false),
                    Telefono = table.Column<string>(type: "TEXT", maxLength: 10, nullable: false),
                    Direccion = table.Column<string>(type: "TEXT", nullable: false),
                    CorreoElectronico = table.Column<string>(type: "TEXT", nullable: false),
                    Impresora = table.Column<string>(type: "TEXT", nullable: false),
                    NotaFactura = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Empresas", x => x.EmpresaId);
                });

            migrationBuilder.CreateTable(
                name: "Pagos",
                columns: table => new
                {
                    PagoId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ReganteId = table.Column<int>(type: "INTEGER", nullable: false),
                    Codigo = table.Column<string>(type: "TEXT", nullable: false),
                    Fecha = table.Column<DateTime>(type: "TEXT", nullable: false),
                    MetodoPago = table.Column<string>(type: "TEXT", nullable: false),
                    MontoPagado = table.Column<float>(type: "REAL", nullable: false),
                    Devuelta = table.Column<float>(type: "REAL", nullable: false),
                    Recargos = table.Column<float>(type: "REAL", nullable: false),
                    Suplementaria = table.Column<float>(type: "REAL", nullable: false),
                    SubTotal = table.Column<float>(type: "REAL", nullable: false),
                    Estado = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pagos", x => x.PagoId);
                });

            migrationBuilder.CreateTable(
                name: "Parametros",
                columns: table => new
                {
                    ParametroOperatacionalesId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    MontoGravedad = table.Column<float>(type: "REAL", nullable: false),
                    MontoBomba = table.Column<float>(type: "REAL", nullable: false),
                    PorcentajeRecargos = table.Column<float>(type: "REAL", nullable: false),
                    TiempoProximoPago = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Parametros", x => x.ParametroOperatacionalesId);
                });

            migrationBuilder.CreateTable(
                name: "Regantes",
                columns: table => new
                {
                    ReganteId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CodigoRegante = table.Column<string>(type: "TEXT", nullable: true),
                    Fecha = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Usuario = table.Column<string>(type: "TEXT", nullable: false),
                    Contrasena = table.Column<string>(type: "TEXT", nullable: false),
                    Nombre = table.Column<string>(type: "TEXT", nullable: false),
                    Apellido = table.Column<string>(type: "TEXT", nullable: false),
                    Apodo = table.Column<string>(type: "TEXT", nullable: true),
                    CorreoElectronico = table.Column<string>(type: "TEXT", nullable: false),
                    Telefono = table.Column<string>(type: "TEXT", nullable: false),
                    Nacionalidad = table.Column<string>(type: "TEXT", nullable: false),
                    Cedula = table.Column<string>(type: "TEXT", nullable: false),
                    Direccion = table.Column<string>(type: "TEXT", nullable: false),
                    Estado = table.Column<bool>(type: "INTEGER", nullable: false),
                    FechaUltimoPago = table.Column<DateTime>(type: "TEXT", nullable: false),
                    MontoSuplementerio = table.Column<float>(type: "REAL", nullable: false),
                    Bloque = table.Column<string>(type: "TEXT", nullable: false),
                    Asociacion = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Regantes", x => x.ReganteId);
                });

            migrationBuilder.CreateTable(
                name: "SalidaCaja",
                columns: table => new
                {
                    CajaId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    UsuarioId = table.Column<int>(type: "INTEGER", nullable: false),
                    Monto = table.Column<float>(type: "REAL", nullable: false),
                    Asunto = table.Column<string>(type: "TEXT", nullable: false),
                    Nota = table.Column<string>(type: "TEXT", nullable: false),
                    Fecha = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SalidaCaja", x => x.CajaId);
                });

            migrationBuilder.CreateTable(
                name: "Usuarios",
                columns: table => new
                {
                    UsuarioId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Fecha = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Nombre = table.Column<string>(type: "TEXT", nullable: false),
                    Apellido = table.Column<string>(type: "TEXT", nullable: false),
                    Cedula = table.Column<string>(type: "TEXT", nullable: false),
                    Usuario = table.Column<string>(type: "TEXT", nullable: false),
                    Contrasena = table.Column<string>(type: "TEXT", nullable: false),
                    Telefono = table.Column<string>(type: "TEXT", nullable: false),
                    CorreoElectronico = table.Column<string>(type: "TEXT", nullable: false),
                    Direccion = table.Column<string>(type: "TEXT", nullable: false),
                    Rol = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuarios", x => x.UsuarioId);
                });

            migrationBuilder.InsertData(
                table: "Usuarios",
                columns: new[] { "UsuarioId", "Apellido", "Cedula", "Contrasena", "CorreoElectronico", "Direccion", "Fecha", "Nombre", "Rol", "Telefono", "Usuario" },
                values: new object[] { 1, "Del Sistema", "00000000000", "Admin12345!!", "administrador-sistema@gmail.com", "Ninguna", new DateTime(2024, 4, 3, 11, 43, 13, 651, DateTimeKind.Local).AddTicks(2230), "Administrador", "Administrador", "8090000000", "administrador" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DetallePagos");

            migrationBuilder.DropTable(
                name: "DetalleRegantes");

            migrationBuilder.DropTable(
                name: "Empresas");

            migrationBuilder.DropTable(
                name: "Pagos");

            migrationBuilder.DropTable(
                name: "Parametros");

            migrationBuilder.DropTable(
                name: "Regantes");

            migrationBuilder.DropTable(
                name: "SalidaCaja");

            migrationBuilder.DropTable(
                name: "Usuarios");
        }
    }
}
