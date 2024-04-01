using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SwiftPay.DAL;
using SwiftPay.Models;
using SwiftPay.Services;
using System.Threading.Tasks;

namespace TestJunta
{
    [TestClass]
    public class TestBloques
    {
        [TestMethod]
        public async Task VerificarBloqueFalse()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<Context>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            using (var context = new Context(options))
            {
                var service = new BloquesService(context);

                // Act
                var resultado = await service.Verificar(-1); // ID que no existe en la base de datos

                // Assert
                Assert.IsFalse(resultado);
            }
        }

        [TestMethod]
        public async Task VerificarBloqueTrue()
        {
            // Permite usar una base de datos en memoria para no alterar o usar la base de datos actual
            var options = new DbContextOptionsBuilder<Context>()
            .UseInMemoryDatabase(databaseName: "TestDatabase")
            .Options;

            using (var context = new Context(options))
            {
                var service = new BloquesService(context);
                var nuevoBloque = new Bloques
                {
                    Nombre = "Bloque1"
                };

                await service.Agregar(nuevoBloque);

                // Verificamos si el bloque existe
                var resultado = await service.Verificar(nuevoBloque.BloqueId);

                // Assert
                Assert.IsTrue(resultado);
            }
        }

        [TestMethod]
        public async Task AgregarBloque()
        {
            // Permite usar una base de datos en memoria para no alterar o usar la base de datos actual
            var options = new DbContextOptionsBuilder<Context>()
            .UseInMemoryDatabase(databaseName: "TestDatabase")
            .Options;

            using (var context = new Context(options))
            {
                var service = new BloquesService(context);
                var nuevoBloque = new Bloques
                {
                    Nombre = "Bloque2"
                };

                // Act -- El resultado que se espera 
                var resultado = await service.Agregar(nuevoBloque);

                // Assert -- Si se ha guardado
                Assert.IsTrue(resultado);
            }
        }

        [TestMethod]
        public async Task ModificarBloque()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<Context>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            using (var context = new Context(options))
            {
                var service = new BloquesService(context);
                var nuevoBloque = new Bloques
                {
                    Nombre = "Bloque3"
                };

                await service.Agregar(nuevoBloque);

                // Modificamos el bloque
                nuevoBloque.Nombre = "Bloque4";

                // Act
                var resultado = await service.Modificar(nuevoBloque);

                // Assert
                Assert.IsTrue(resultado);

                // Verificamos que el bloque se haya modificado correctamente
                var bloqueModificado = await service.Buscar(nuevoBloque.BloqueId);
                Assert.IsNotNull(bloqueModificado);
                Assert.AreEqual("Bloque4", bloqueModificado.Nombre); //aqui
            }
        }

        [TestMethod]
        public async Task GuardarBloque()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<Context>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            using (var context = new Context(options))
            {
                var service = new BloquesService(context);
                var nuevoBloque = new Bloques
                {
                    Nombre = "Bloque5"
                };

                // Act - Guardar la bloque por primera vez
                var resultadoGuardar = await service.Guardar(nuevoBloque);

                // Assert - Verificar que se guardó correctamente
                Assert.IsTrue(resultadoGuardar);
                var bloqueGuardado = await service.Buscar(nuevoBloque.BloqueId);
                Assert.IsNotNull(bloqueGuardado);
                Assert.AreEqual("Bloque5", bloqueGuardado.Nombre);

                // Modificar el bloque y guardarla nuevamente
                nuevoBloque.Nombre = "Bloque3";

                // Act - Guardar el bloque modificado
                var resultadoModificar = await service.Guardar(nuevoBloque);

                // Assert - Verificar que se modificó correctamente
                Assert.IsTrue(resultadoModificar);
                var bloqueModificado = await service.Buscar(nuevoBloque.BloqueId);
                Assert.IsNotNull(bloqueModificado);
                Assert.AreEqual("Bloque3", bloqueModificado.Nombre);
            }
        }

        [TestMethod]
        public async Task EliminarBloque()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<Context>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            using (var context = new Context(options))
            {
                var service = new BloquesService(context);
                var nuevoBloque = new Bloques
                {
                    Nombre = "Bloque12"
                };

                // Act - Agregar el bloque
                await service.Agregar(nuevoBloque);

                // Act - Eliminar el bloque
                var resultadoEliminar = await service.Eliminar(nuevoBloque);

                // Assert - Verificar que se eliminó correctamente
                Assert.IsTrue(resultadoEliminar);

                // Verificar que el bloque no se pueda encontrar después de la eliminación
                var bloqueEliminado = await service.Buscar(nuevoBloque.BloqueId);
                Assert.IsNull(bloqueEliminado);
            }
        }

        [TestMethod]
        public async Task ListarBloque()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<Context>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            using (var context = new Context(options))
            {
                var service = new BloquesService(context);

                // Agregar varios Bloques
                await service.Agregar(new Bloques { Nombre = "Bloque1" });
                await service.Agregar(new Bloques { Nombre = "Bloque2" });
                await service.Agregar(new Bloques { Nombre = "Bloque3" });

                // Act
                var bloques = await service.Listar(b => b.Nombre.StartsWith("Bloque"));

                // Assert
                Assert.AreEqual(3, bloques.Count);
                Assert.IsTrue(bloques.Any(b => b.Nombre == "Bloque1"));
                Assert.IsTrue(bloques.Any(b => b.Nombre == "Bloque2"));
                Assert.IsTrue(bloques.Any(b => b.Nombre == "Bloque3"));
            }
        }


    }
}
