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
    public class TestPagos
    {
        [TestMethod]
        public async Task VerificarPagoFalse()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<Context>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            using (var context = new Context(options))
            {
                var service = new PagosService(context);

                // Act
                var resultado = await service.Verificar(-1); // ID que no existe en la base de datos

                // Assert
                Assert.IsFalse(resultado);
            }
        }

        [TestMethod]
        public async Task VerificarPagoTrue()
        {
            // Permite usar una base de datos en memoria para no alterar o usar la base de datos actual
            var options = new DbContextOptionsBuilder<Context>()
            .UseInMemoryDatabase(databaseName: "TestDatabase")
            .Options;

            using (var context = new Context(options))
            {
                var service = new PagosService(context);
                var nuevoPago = new Pagos
                {
                    ReganteId = 1,
                    Codigo = "A123",
                    MetodoPago = "Efectivo",
                    MontoPagado = 452,
                    Devuelta = 100,
                    Recargos = 100,
                    Suplementaria = 100,
                    SubTotal = 100,
                    Estado = "Activo"
                };

                await service.Insertar(nuevoPago);

                // Verificamos si la asociación existe
                var resultado = await service.Verificar(nuevoPago.PagoId);

                // Assert
                Assert.IsTrue(resultado);
            }
        }

        [TestMethod]
        public async Task AgregarPago()
        {
            // Permite usar una base de datos en memoria para no alterar o usar la base de datos actual
            var options = new DbContextOptionsBuilder<Context>()
            .UseInMemoryDatabase(databaseName: "TestDatabase")
            .Options;

            using (var context = new Context(options))
            {
                var service = new PagosService(context);
                var nuevoPago = new Pagos
                {
                    ReganteId = 1,
                    Codigo = "A123",
                    MetodoPago = "Efectivo",
                    MontoPagado = 452,
                    Devuelta = 100,
                    Recargos = 100,
                    Suplementaria = 100,
                    SubTotal = 100,
                    Estado = "Activo"
                };

                // Act -- El resultado que se espera 
                var resultado = await service.Insertar(nuevoPago);

                // Assert -- Si se ha guardado
                Assert.IsTrue(resultado > 0);
            }
        }

        [TestMethod]
        public async Task ModificarPago()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<Context>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            using (var context = new Context(options))
            {
                var service = new PagosService(context);
                var nuevoPago = new Pagos
                {
                    ReganteId = 1,
                    Codigo = "A123",
                    MetodoPago = "Efectivo",
                    MontoPagado = 452,
                    Devuelta = 100,
                    Recargos = 100,
                    Suplementaria = 100,
                    SubTotal = 100,
                    Estado = "Activo"
                };

                await service.Insertar(nuevoPago);

                // Modificamos 
                nuevoPago.Estado = "Inactivo";

                // Act
                var resultado = await service.Modificar(nuevoPago);

                // Assert
                Assert.IsTrue(resultado);

                // Verificamos 
                var pagoModificado = await service.Buscar(nuevoPago.PagoId);
                Assert.IsNotNull(pagoModificado);
                Assert.AreEqual("Inactivo", pagoModificado.Estado); //aqui
            }
        }

        [TestMethod]
        public async Task ListarAsociaciones()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<Context>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            using (var context = new Context(options))
            {
                var service = new PagosService(context);

                // Agregar 
                await service.Insertar(new Pagos {
                    PagoId = 11,
                    ReganteId = 1,
                    Codigo = "A123",
                    MetodoPago = "Efectivo",
                    MontoPagado = 452,
                    Devuelta = 100,
                    Recargos = 100,
                    Suplementaria = 100,
                    SubTotal = 100,
                    Estado = "Activo1"
                });
                await service.Insertar(new Pagos {
                    PagoId = 12,
                    ReganteId = 1,
                    Codigo = "A123",
                    MetodoPago = "Efectivo",
                    MontoPagado = 452,
                    Devuelta = 100,
                    Recargos = 100,
                    Suplementaria = 100,
                    SubTotal = 100,
                    Estado = "Activo2"
                });

                // Act
                // Listar 
                var pagos = await service.Listar(p => p.Estado.StartsWith("Activo"));

                // Assert
                // Verificar que se hayan listado 
                Assert.AreEqual(2, pagos.Count);
                Assert.IsTrue(pagos.Any(p => p.Estado == "Activo1"));
                Assert.IsTrue(pagos.Any(p => p.Estado == "Activo2"));
            }
        }
    }
}
