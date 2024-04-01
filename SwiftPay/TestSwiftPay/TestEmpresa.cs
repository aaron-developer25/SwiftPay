using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SwiftPay.DAL;
using SwiftPay.Models;
using SwiftPay.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestJunta
{
    [TestClass]
    public class TestEmpresa
    {
        [TestMethod]
        public async Task VerificarEmpresaFalse()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<Context>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            using (var context = new Context(options))
            {
                var service = new EmpresaService(context);

                // Act
                var resultado = await service.Existe(-1); // ID que no existe en la base de datos

                // Assert
                Assert.IsFalse(resultado);
            }
        }

        [TestMethod]
        public async Task VerificarEmpresaTrue()
        {
            // Permite usar una base de datos en memoria para no alterar o usar la base de datos actual
            var options = new DbContextOptionsBuilder<Context>()
            .UseInMemoryDatabase(databaseName: "TestDatabase")
            .Options;

            using (var context = new Context(options))
            {
                var service = new EmpresaService(context);
                var nuevaEmpresa = new Empresa
                {
                    Nombre = "Venta",
                    RNC = "12345",
                    Telefono = "12345789",
                    Direccion = "Nagua",
                    CorreoElectronico = "ruth@gmail.com",
                    Impresora = "Epson",
                    NotaFactura = "Facil"
                };

                await service.Insertar(nuevaEmpresa);

                // Verificamos 
                var resultado = await service.Existe(nuevaEmpresa.EmpresaId);

                // Assert
                Assert.IsTrue(resultado);
            }
        }

        [TestMethod]
        public async Task AgregarEmpresa()
        {
            // Permite usar una base de datos en memoria para no alterar o usar la base de datos actual
            var options = new DbContextOptionsBuilder<Context>()
            .UseInMemoryDatabase(databaseName: "TestDatabase")
            .Options;

            using (var context = new Context(options))
            {
                var service = new EmpresaService(context);
                var nuevaEmpresa = new Empresa
                {
                    Nombre = "Venta",
                    RNC = "12345",
                    Telefono = "12345789",
                    Direccion = "Nagua",
                    CorreoElectronico = "ruth@gmail.com",
                    Impresora = "Epson",
                    NotaFactura = "Facil"
                };

                // Act -- El resultado que se espera 
                var resultado = await service.Insertar(nuevaEmpresa);

                // Assert -- Si se ha guardado
                Assert.IsTrue(resultado);
            }
        }

        [TestMethod]
        public async Task ModificarEmpresa()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<Context>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            using (var context = new Context(options))
            {
                var service = new EmpresaService(context);
                var nuevaEmpresa = new Empresa
                {
                    Nombre = "Venta",
                    RNC = "12345",
                    Telefono = "12345789",
                    Direccion = "Nagua",
                    CorreoElectronico = "ruth@gmail.com",
                    Impresora = "Epson",
                    NotaFactura = "Facil"
                };

                await service.Insertar(nuevaEmpresa);

                // Modificamos 
                nuevaEmpresa.Nombre = "Cobro";

                // Act
                var resultado = await service.Modificar(nuevaEmpresa);

                // Assert
                Assert.IsTrue(resultado);

                // Verificamos 
                var empresaModificada = await service.Buscar(nuevaEmpresa.EmpresaId);
                Assert.IsNotNull(empresaModificada);
                Assert.AreEqual("Cobro", empresaModificada.Nombre); //aqui
            }
        }

        [TestMethod]
        public async Task GuardarEmpresa()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<Context>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            using (var context = new Context(options))
            {
                var service = new EmpresaService(context);
                var nuevaEmpresa = new Empresa
                {
                    Nombre = "Venta",
                    RNC = "12345",
                    Telefono = "12345789",
                    Direccion = "Nagua",
                    CorreoElectronico = "ruth@gmail.com",
                    Impresora = "Epson",
                    NotaFactura = "Facil"
                };

                // Act - Guardar 
                var resultadoGuardar = await service.Guardar(nuevaEmpresa);

                // Assert - Verificar que se guardó correctamente
                Assert.IsTrue(resultadoGuardar);
                var empresaGuardada = await service.Buscar(nuevaEmpresa.EmpresaId);
                Assert.IsNotNull(empresaGuardada);
                Assert.AreEqual("Venta", empresaGuardada.Nombre);

                // Modificar 
                nuevaEmpresa.Nombre = "Salon";

                // Act - Guardar 
                var resultadoModificar = await service.Guardar(nuevaEmpresa);

                // Assert - Verificar que se modificó correctamente
                Assert.IsTrue(resultadoModificar);
                var empresaModificada = await service.Buscar(nuevaEmpresa.EmpresaId);
                Assert.IsNotNull(empresaModificada);
                Assert.AreEqual("Salon", empresaModificada.Nombre);
            }
        }

        [TestMethod]
        public async Task EliminarEmpresa()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<Context>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            using (var context = new Context(options))
            {
                var service = new EmpresaService(context);
                var nuevaEmpresa = new Empresa
                {
                    Nombre = "Venta",
                    RNC = "12345",
                    Telefono = "12345789",
                    Direccion = "Nagua",
                    CorreoElectronico = "ruth@gmail.com",
                    Impresora = "Epson",
                    NotaFactura = "Facil"
                };

                // Act - Agregar 
                await service.Insertar(nuevaEmpresa);

                // Act - Eliminar 
                var resultadoEliminar = await service.Eliminar(nuevaEmpresa);

                // Assert - Verificar que se eliminó correctamente
                Assert.IsTrue(resultadoEliminar);

                // Verificar 
                var empresaEliminada = await service.Buscar(nuevaEmpresa.EmpresaId);
                Assert.IsNull(empresaEliminada);
            }
        }

        [TestMethod]
        public async Task ListarEmpresa()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<Context>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            using (var context = new Context(options))
            {
                var service = new EmpresaService(context);

                // Agregar varias 
                await service.Insertar(new Empresa {
                    EmpresaId = 11,
                    Nombre = "Cobro1",
                    RNC = "12345",
                    Telefono = "12345789",
                    Direccion = "Nagua",
                    CorreoElectronico = "ruth@gmail.com",
                    Impresora = "Epson",
                    NotaFactura = "Facil"
                });
                await service.Insertar(new Empresa {
                    EmpresaId = 12,
                    Nombre = "Cobro2",
                    RNC = "12345",
                    Telefono = "12345789",
                    Direccion = "Villa Riva",
                    CorreoElectronico = "ruth@gmail.com",
                    Impresora = "Epson",
                    NotaFactura = "Facil"
                });
                await service.Insertar(new Empresa {
                    EmpresaId = 13,
                    Nombre = "Cobro3",
                    RNC = "12345",
                    Telefono = "12345789",
                    Direccion = "Factor",
                    CorreoElectronico = "ruth@gmail.com",
                    Impresora = "Epson",
                    NotaFactura = "Facil"
                });

                // Act
                // Listar 
                var empresas = await service.Listar(e => e.Nombre.StartsWith("Cobro"));

                // Assert
                // Verificar 
                Assert.AreEqual(3, empresas.Count);
                Assert.IsTrue(empresas.Any(e => e.Nombre == "Cobro1"));
                Assert.IsTrue(empresas.Any(e => e.Nombre == "Cobro2"));
                Assert.IsTrue(empresas.Any(e => e.Nombre == "Cobro3"));
            }
        }
    }
}
