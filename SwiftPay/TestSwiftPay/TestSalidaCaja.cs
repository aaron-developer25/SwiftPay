using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SwiftPay.DAL;
using SwiftPay.Models;
using SwiftPay.Services;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace TestJunta
{
    [TestClass]
    public class TestSalidaCaja
    {
        [TestMethod]
        public async Task VerificarSalidaCajaFalse()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<Context>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            using (var context = new Context(options))
            {
                var service = new SalidaCajaService(context);

                // Act
                var resultado = await service.Verificar(-1); // ID que no existe en la base de datos

                // Assert
                Assert.IsFalse(resultado);
            }
        }

        [TestMethod]
        public async Task VerificarSalidaCajaTrue()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<Context>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            using (var context = new Context(options))
            {
                var service = new SalidaCajaService(context);
                var nuevaSalidaCaja = new SalidaCaja
                {
                    UsuarioId = 1,
                    Monto = 100.0f,
                    Asunto = "Gastos varios",
                    Nota = "Pago de facturas",
                    Fecha = DateTime.Now
                };

                await service.Agregar(nuevaSalidaCaja);

                // Act
                var resultado = await service.Verificar(nuevaSalidaCaja.CajaId);

                // Assert
                Assert.IsTrue(resultado);
            }
        }

        [TestMethod]
        public async Task AgregarSalidaCaja()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<Context>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            using (var context = new Context(options))
            {
                var service = new SalidaCajaService(context);
                var nuevaSalidaCaja = new SalidaCaja
                {
                    UsuarioId = 1,
                    Monto = 100.0f,
                    Asunto = "Gastos varios",
                    Nota = "Pago de facturas",
                    Fecha = DateTime.Now
                };

                // Act
                var resultado = await service.Agregar(nuevaSalidaCaja);

                // Assert
                Assert.IsTrue(resultado);
            }
        }

        [TestMethod]
        public async Task ModificarSalidaCaja()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<Context>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            using (var context = new Context(options))
            {
                var service = new SalidaCajaService(context);
                var nuevaSalidaCaja = new SalidaCaja
                {
                    UsuarioId = 1,
                    Monto = 100.0f,
                    Asunto = "Gastos varios",
                    Nota = "Pago de facturas",
                    Fecha = DateTime.Now
                };

                await service.Agregar(nuevaSalidaCaja);

                // Modificamos la salida de caja
                nuevaSalidaCaja.Monto = 150.0f;

                // Act
                var resultado = await service.Modificar(nuevaSalidaCaja);

                // Assert
                Assert.IsTrue(resultado);

                // Verificamos que la salida de caja se haya modificado correctamente
                var salidaCajaModificada = await service.Buscar(nuevaSalidaCaja.CajaId);
                Assert.IsNotNull(salidaCajaModificada);
                Assert.AreEqual(150.0f, salidaCajaModificada.Monto);
            }
        }

        [TestMethod]
        public async Task GuardarSalidaCaja()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<Context>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            using (var context = new Context(options))
            {
                var service = new SalidaCajaService(context);
                var nuevaSalidaCaja = new SalidaCaja
                {
                    UsuarioId = 1,
                    Monto = 100.0f,
                    Asunto = "Gastos varios",
                    Nota = "Pago de facturas",
                    Fecha = DateTime.Now
                };

                // Act - Guardar la salida de caja por primera vez
                var resultadoGuardar = await service.Guardar(nuevaSalidaCaja);

                // Assert - Verificar que se guardó correctamente
                Assert.IsTrue(resultadoGuardar);
                var salidaCajaGuardada = await service.Buscar(nuevaSalidaCaja.CajaId);
                Assert.IsNotNull(salidaCajaGuardada);
                Assert.AreEqual(100.0f, salidaCajaGuardada.Monto);

                // Modificar la salida de caja y guardarla nuevamente
                nuevaSalidaCaja.Monto = 150.0f;

                // Act - Guardar la salida de caja modificada
                var resultadoModificar = await service.Guardar(nuevaSalidaCaja);

                // Assert - Verificar que se modificó correctamente
                Assert.IsTrue(resultadoModificar);
                var salidaCajaModificada = await service.Buscar(nuevaSalidaCaja.CajaId);
                Assert.IsNotNull(salidaCajaModificada);
                Assert.AreEqual(150.0f, salidaCajaModificada.Monto);
            }
        }

        [TestMethod]
        public async Task EliminarSalidaCaja()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<Context>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            using (var context = new Context(options))
            {
                var service = new SalidaCajaService(context);
                var nuevaSalidaCaja = new SalidaCaja
                {
                    UsuarioId = 1,
                    Monto = 100.0f,
                    Asunto = "Gastos varios",
                    Nota = "Pago de facturas",
                    Fecha = DateTime.Now
                };

                // Act - Agregar la salida de caja
                await service.Agregar(nuevaSalidaCaja);

                // Act - Eliminar la salida de caja
                var resultadoEliminar = await service.Eliminar(nuevaSalidaCaja);

                // Assert - Verificar que se eliminó correctamente
                Assert.IsTrue(resultadoEliminar);

                // Verificar que la salida de caja no se pueda encontrar después de la eliminación
                var salidaCajaEliminada = await service.Buscar(nuevaSalidaCaja.CajaId);
                Assert.IsNull(salidaCajaEliminada);
            }
        }

        [TestMethod]
        public async Task ListarSalidaCajas()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<Context>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            using (var context = new Context(options))
            {
                var service = new SalidaCajaService(context);

                // Agregar varias salidas de caja
                await service.Agregar(new SalidaCaja
                {
                    UsuarioId = 1,
                    Monto = 100.0f,
                    Asunto = "Gastos varios",
                    Nota = "Pago de facturas",
                    Fecha = DateTime.Now
                });
                await service.Agregar(new SalidaCaja
                {
                    UsuarioId = 2,
                    Monto = 150.0f,
                    Asunto = "Gastos varios",
                    Nota = "Pago de servicios",
                    Fecha = DateTime.Now
                });
                await service.Agregar(new SalidaCaja
                {
                    UsuarioId = 3,
                    Monto = 200.0f,
                    Asunto = "Gastos varios",
                    Nota = "Pago de nómina",
                    Fecha = DateTime.Now
                });

                // Act
                // Listar las salidas de caja que cumplen con el criterio
                var salidaCajas = await service.Listar(c => c.Monto > 100.0f);

                // Assert
                // Verificar que se hayan listado las salidas de caja esperadas
                Assert.AreEqual(2, salidaCajas.Count);
                Assert.IsTrue(salidaCajas.Any(c => c.Monto == 100.0f));
                Assert.IsTrue(salidaCajas.Any(c => c.Monto == 150.0f));
                Assert.IsTrue(salidaCajas.Any(c => c.Monto == 200.0f));
            }
        }
    }
}

