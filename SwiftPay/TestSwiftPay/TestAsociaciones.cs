using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SwiftPay.DAL;
using SwiftPay.Models;
using SwiftPay.Services;
using System.Threading.Tasks;

namespace TestJunta
{
    [TestClass]
    public class TestAsociaciones
    {
        [TestMethod]
        public async Task VerificarAsocicionFalse()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<Context>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            using (var context = new Context(options))
            {
                var service = new AsociacionesService(context);

                // Act
                var resultado = await service.Verificar(-1); // ID que no existe en la base de datos

                // Assert
                Assert.IsFalse(resultado);
            }
        }

        [TestMethod]
        public async Task VerificarAsocicionTrue()
        {
            // Permite usar una base de datos en memoria para no alterar o usar la base de datos actual
            var options = new DbContextOptionsBuilder<Context>()
            .UseInMemoryDatabase(databaseName: "TestDatabase")
            .Options;

            using (var context = new Context(options))
            {
                var service = new AsociacionesService(context);
                var nuevaAsociacion = new Asociaciones
                {
                    Nombre = "Aa123"
                };

                await service.Agregar(nuevaAsociacion);

                // Verificamos si la asociación existe
                var resultado = await service.Verificar(nuevaAsociacion.AsociacionId);

                // Assert
                Assert.IsTrue(resultado);
            }
        }

        [TestMethod]
        public async Task AgregarAsociacion()
        {
            // Permite usar una base de datos en memoria para no alterar o usar la base de datos actual
            var options = new DbContextOptionsBuilder<Context>()
            .UseInMemoryDatabase(databaseName: "TestDatabase")
            .Options;

            using (var context = new Context(options))
            {
                var service = new AsociacionesService(context);
                var nuevaAsociacion = new Asociaciones
                {
                    Nombre = "Aa123"
                };

                // Act -- El resultado que se espera 
                var resultado = await service.Agregar(nuevaAsociacion);

                // Assert -- Si se ha guardado
                Assert.IsTrue(resultado);
            }
        }

        [TestMethod]
        public async Task ModificarAsociacion()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<Context>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            using (var context = new Context(options))
            {
                var service = new AsociacionesService(context);
                var nuevaAsociacion = new Asociaciones
                {
                    Nombre = "BB123"
                };

                await service.Agregar(nuevaAsociacion);

                // Modificamos la asociación
                nuevaAsociacion.Nombre = "CC123";

                // Act
                var resultado = await service.Modificar(nuevaAsociacion);

                // Assert
                Assert.IsTrue(resultado);

                // Verificamos que la asociación se haya modificado correctamente
                var asociacionModificada = await service.Buscar(nuevaAsociacion.AsociacionId);
                Assert.IsNotNull(asociacionModificada);
                Assert.AreEqual("CC123", asociacionModificada.Nombre); //aqui
            }
        }

        [TestMethod]
        public async Task GuardarAsociacion()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<Context>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            using (var context = new Context(options))
            {
                var service = new AsociacionesService(context);
                var nuevaAsociacion = new Asociaciones
                {
                    Nombre = "BB123"
                };

                // Act - Guardar la asociación por primera vez
                var resultadoGuardar = await service.Guardar(nuevaAsociacion);

                // Assert - Verificar que se guardó correctamente
                Assert.IsTrue(resultadoGuardar);
                var asociacionGuardada = await service.Buscar(nuevaAsociacion.AsociacionId);
                Assert.IsNotNull(asociacionGuardada);
                Assert.AreEqual("BB123", asociacionGuardada.Nombre);

                // Modificar la asociación y guardarla nuevamente
                nuevaAsociacion.Nombre = "CC123";

                // Act - Guardar la asociación modificada
                var resultadoModificar = await service.Guardar(nuevaAsociacion);

                // Assert - Verificar que se modificó correctamente
                Assert.IsTrue(resultadoModificar);
                var asociacionModificada = await service.Buscar(nuevaAsociacion.AsociacionId);
                Assert.IsNotNull(asociacionModificada);
                Assert.AreEqual("CC123", asociacionModificada.Nombre);
            }
        }

        [TestMethod]
        public async Task EliminarAsociacion()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<Context>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            using (var context = new Context(options))
            {
                var service = new AsociacionesService(context);
                var nuevaAsociacion = new Asociaciones
                {
                    Nombre = "BB123"
                };

                // Act - Agregar la asociación
                await service.Agregar(nuevaAsociacion);

                // Act - Eliminar la asociación
                var resultadoEliminar = await service.Eliminar(nuevaAsociacion);

                // Assert - Verificar que se eliminó correctamente
                Assert.IsTrue(resultadoEliminar);

                // Verificar que la asociación no se pueda encontrar después de la eliminación
                var asociacionEliminada = await service.Buscar(nuevaAsociacion.AsociacionId);
                Assert.IsNull(asociacionEliminada);
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
                var service = new AsociacionesService(context);

                // Agregar varias asociaciones
                await service.Agregar(new Asociaciones { Nombre = "Asociacion1" });
                await service.Agregar(new Asociaciones { Nombre = "Asociacion2" });
                await service.Agregar(new Asociaciones { Nombre = "Asociacion3" });

                // Act
                // Listar las asociaciones que cumplen con el criterio
                var asociaciones = await service.Listar(a => a.Nombre.StartsWith("Asociacion"));

                // Assert
                // Verificar que se hayan listado las asociaciones esperadas
                Assert.AreEqual(3, asociaciones.Count);
                Assert.IsTrue(asociaciones.Any(a => a.Nombre == "Asociacion1"));
                Assert.IsTrue(asociaciones.Any(a => a.Nombre == "Asociacion2"));
                Assert.IsTrue(asociaciones.Any(a => a.Nombre == "Asociacion3"));
            }
        }






    }
}