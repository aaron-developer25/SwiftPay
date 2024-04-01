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
    public class TestDetalleRegantes
    {
        [TestMethod]
        public async Task VerificarDetalleFalse()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<Context>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            using (var context = new Context(options))
            {
                var service = new DetalleReganteService(context);

                // Act
                var resultado = await service.Verificar(-1); // ID que no existe en la base de datos

                // Assert
                Assert.IsFalse(resultado);
            }
        }

        [TestMethod]
        public async Task VerificarDetalleTrue()
        {
            var options = new DbContextOptionsBuilder<Context>()
            .UseInMemoryDatabase(databaseName: "TestDatabase")
            .Options;

            using (var context = new Context(options))
            {
                var service = new DetalleReganteService(context);
                var nuevoDetalle = new DetalleRegante
                {
                    ReganteId = 1,
                    CodigoParcela = "L123",
                    Tareas = 12,
                    TipoIrrigacion = "Bomba"
                };

                await service.Agregar(nuevoDetalle);

                var resultado = await service.Verificar(nuevoDetalle.DetalleReganteId);

                // Assert
                Assert.IsTrue(resultado);
            }
        }

        [TestMethod]
        public async Task AgregarDetalle()
        {
            // Permite usar una base de datos en memoria para no alterar o usar la base de datos actual
            var options = new DbContextOptionsBuilder<Context>()
            .UseInMemoryDatabase(databaseName: "TestDatabase")
            .Options;

            using (var context = new Context(options))
            {
                var service = new DetalleReganteService(context);
                var nuevoDetalle = new DetalleRegante
                {
                    ReganteId = 2,
                    CodigoParcela = "D123",
                    Tareas = 10,
                    TipoIrrigacion = "Gravedad"
                };

                // Act -- El resultado que se espera 
                var resultado = await service.Agregar(nuevoDetalle);

                // Assert -- Si se ha guardado
                Assert.IsTrue(resultado);
            }
        }

        [TestMethod]
        public async Task ModificarDetalle()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<Context>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            using (var context = new Context(options))
            {
                var service = new DetalleReganteService(context);
                var nuevoDetalle = new DetalleRegante
                {
                    ReganteId = 3,
                    CodigoParcela = "S12",
                    Tareas = 5,
                    TipoIrrigacion = "Bomba"
                };

                await service.Agregar(nuevoDetalle);

                // Modificamos 
                nuevoDetalle.TipoIrrigacion = "Gravedad";

                // Act
                var resultado = await service.Modificar(nuevoDetalle);

                // Assert
                Assert.IsTrue(resultado);

                // Verificamos
                var detalleModificado = await service.Buscar(nuevoDetalle.DetalleReganteId);
                Assert.IsNotNull(detalleModificado);
                Assert.AreEqual("Gravedad", detalleModificado.TipoIrrigacion); //aqui
            }
        }

        [TestMethod]
        public async Task GuardarDetalle()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<Context>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            using (var context = new Context(options))
            {
                var service = new DetalleReganteService(context);
                var nuevoDetalle = new DetalleRegante
                {
                    ReganteId = 4,
                    CodigoParcela = "M123",
                    Tareas = 7,
                    TipoIrrigacion = "Bomba"
                };

                // Act - Guardar 
                var resultadoGuardar = await service.Guardar(nuevoDetalle);

                // Assert - Verificar que se guardó correctamente
                Assert.IsTrue(resultadoGuardar);
                var detalleGuardado = await service.Buscar(nuevoDetalle.DetalleReganteId);
                Assert.IsNotNull(detalleGuardado);
                Assert.AreEqual("Bomba", detalleGuardado.TipoIrrigacion);

                // Modificar
                nuevoDetalle.TipoIrrigacion = "Gravedad";

                // Act - Guardar 
                var resultadoModificar = await service.Guardar(nuevoDetalle);

                // Assert - Verificar 
                Assert.IsTrue(resultadoModificar);
                var detalleModificado = await service.Buscar(nuevoDetalle.DetalleReganteId);
                Assert.IsNotNull(detalleModificado);
                Assert.AreEqual("Gravedad", detalleModificado.TipoIrrigacion);
            }
        }

        [TestMethod]
        public async Task EliminarDetalle()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<Context>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            using (var context = new Context(options))
            {
                var service = new DetalleReganteService(context);
                var nuevoDetalle = new DetalleRegante
                {
                    ReganteId = 5,
                    CodigoParcela = "L123",
                    Tareas = 9,
                    TipoIrrigacion = "Gravedad"
                };

                // Act - Agregar 
                await service.Agregar(nuevoDetalle);

                // Act - Eliminar 
                var resultadoEliminar = await service.Eliminar(nuevoDetalle);

                // Assert - Verificar que se eliminó correctamente
                Assert.IsTrue(resultadoEliminar);

                // Verificar 
                var detalleEliminado = await service.Buscar(nuevoDetalle.DetalleReganteId);
                Assert.IsNull(detalleEliminado);
            }
        }

        [TestMethod]
        public async Task ListarDetalle()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<Context>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            using (var context = new Context(options))
            {
                var service = new DetalleReganteService(context);

                // Agregar 
                await service.Agregar(new DetalleRegante
                {
                    DetalleReganteId = 11,
                    ReganteId = 6,
                    CodigoParcela = "L123",
                    Tareas = 9,
                    TipoIrrigacion = "Gravedad1"
                });
                await service.Agregar(new DetalleRegante
                {
                    DetalleReganteId = 12,
                    ReganteId = 7,
                    CodigoParcela = "L123",
                    Tareas = 9,
                    TipoIrrigacion = "Gravedad2"
                });

                // Act
                // Listar 
                var detalle = await service.Listar(d => d.TipoIrrigacion.StartsWith("Gravedad"));

                // Assert
                // Verificar 
                Assert.AreEqual(3, detalle.Count);
                Assert.IsTrue(detalle.Any(d => d.TipoIrrigacion == "Gravedad1"));
                Assert.IsTrue(detalle.Any(d => d.TipoIrrigacion == "Gravedad2"));
            }
        }
    }
}
