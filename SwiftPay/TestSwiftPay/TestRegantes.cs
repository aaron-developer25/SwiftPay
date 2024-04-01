using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SwiftPay.DAL;
using SwiftPay.Models;
using SwiftPay.Services;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace TestJunta
{
    [TestClass]
    public class TestRegantes
    {
        [TestMethod]
        public async Task VerificarReganteFalse()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<Context>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            using (var context = new Context(options))
            {
                var service = new RegantesService(context);

                // Act
                var resultado = await service.Verificar(-1); // ID que no existe en la base de datos

                // Assert
                Assert.IsFalse(resultado);
            }
        }

        [TestMethod]
        public async Task VerificarReganteTrue()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<Context>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            using (var context = new Context(options))
            {
                var service = new RegantesService(context);
                var nuevoRegante = new Regantes
                {
                    Nombre = "John",
                    Apellido = "Doe",
                    Cedula = "1234567890", // Ejemplo: Proporciona valores para las propiedades requeridas
                    Contrasena = "password",
                    CorreoElectronico = "john@example.com",
                    Direccion = "123 Street",
                    Nacionalidad = "USA",
                    Telefono = "123456789",
                    Usuario = "john.doe"
                };

                // Act
                var resultado = await service.Agregar(nuevoRegante);

                // Assert
                Assert.IsTrue(resultado);
            }
        }


        [TestMethod]
        public async Task ModificarRegante()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<Context>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            using (var context = new Context(options))
            {
                var service = new RegantesService(context);
                var nuevoRegante = new Regantes
                {
                    Nombre = "John",
                    Apellido = "Doe",
                    Cedula = "1234567890", // Ejemplo: Proporciona valores para las propiedades requeridas
                    Contrasena = "password",
                    CorreoElectronico = "john@example.com",
                    Direccion = "123 Street",
                    Nacionalidad = "USA",
                    Telefono = "123456789",
                    Usuario = "john.doe"
                };

                await service.Agregar(nuevoRegante);

                // Modificamos el regante
                nuevoRegante.Nombre = "Jane";

                // Act
                var resultado = await service.Modificar(nuevoRegante);

                // Assert
                Assert.IsTrue(resultado);

                // Verificamos que el regante se haya modificado correctamente
                var reganteModificado = await service.Buscar(nuevoRegante.ReganteId);
                Assert.IsNotNull(reganteModificado);
                Assert.AreEqual("Jane", reganteModificado.Nombre);
            }
        }

        [TestMethod]
        public async Task GuardarRegante()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<Context>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            using (var context = new Context(options))
            {
                var service = new RegantesService(context);
                var nuevoRegante = new Regantes
                {
                    Nombre = "John",
                    Apellido = "Doe",
                    Cedula = "1234567890", // Ejemplo: Proporciona valores para las propiedades requeridas
                    Contrasena = "password",
                    CorreoElectronico = "john@example.com",
                    Direccion = "123 Street",
                    Nacionalidad = "USA",
                    Telefono = "123456789",
                    Usuario = "john.doe"
                };

                // Act - Guardar el regante por primera vez
                var resultadoGuardar = await service.Guardar(nuevoRegante);

                // Assert - Verificar que se guardó correctamente
                Assert.IsTrue(resultadoGuardar);
                var reganteGuardado = await service.Buscar(nuevoRegante.ReganteId);
                Assert.IsNotNull(reganteGuardado);
                Assert.AreEqual("John", reganteGuardado.Nombre);

                // Modificar el regante y guardarlo nuevamente
                nuevoRegante.Nombre = "Jane";

                // Act - Guardar el regante modificado
                var resultadoModificar = await service.Guardar(nuevoRegante);

                // Assert - Verificar que se modificó correctamente
                Assert.IsTrue(resultadoModificar);
                var reganteModificado = await service.Buscar(nuevoRegante.ReganteId);
                Assert.IsNotNull(reganteModificado);
                Assert.AreEqual("Jane", reganteModificado.Nombre);
            }
        }

        [TestMethod]
        public async Task EliminarRegante()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<Context>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            using (var context = new Context(options))
            {
                var service = new RegantesService(context);
                var nuevoRegante = new Regantes
                {
                    Nombre = "John",
                    Apellido = "Doe",
                    Cedula = "1234567890", // Ejemplo: Proporciona valores para las propiedades requeridas
                    Contrasena = "password",
                    CorreoElectronico = "john@example.com",
                    Direccion = "123 Street",
                    Nacionalidad = "USA",
                    Telefono = "123456789",
                    Usuario = "john.doe"
                };

                // Act - Agregar el regante
                await service.Agregar(nuevoRegante);

                // Act - Eliminar el regante
                var resultadoEliminar = await service.Eliminar(nuevoRegante);

                // Assert - Verificar que se eliminó correctamente
                Assert.IsTrue(resultadoEliminar);

                // Verificar que el regante no se pueda encontrar después de la eliminación
                var reganteEliminado = await service.Buscar(nuevoRegante.ReganteId);
                Assert.IsNull(reganteEliminado);
            }
        }

        [TestMethod]
        public async Task ListarRegantes()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<Context>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            using (var context = new Context(options))
            {
                var service = new RegantesService(context);

                // Agregar varios regantes con todas las propiedades requeridas
                await service.Agregar(new Regantes
                {
                    Nombre = "John",
                    Apellido = "Doe",
                    Cedula = "1234567890",
                    Contrasena = "password",
                    CorreoElectronico = "john@example.com",
                    Direccion = "123 Street",
                    Nacionalidad = "USA",
                    Telefono = "123456789",
                    Usuario = "john.doe"
                });
                await service.Agregar(new Regantes
                {
                    Nombre = "Jane",
                    Apellido = "Doe",
                    Cedula = "0987654321",
                    Contrasena = "password",
                    CorreoElectronico = "jane@example.com",
                    Direccion = "456 Street",
                    Nacionalidad = "USA",
                    Telefono = "987654321",
                    Usuario = "jane.doe"
                });
                await service.Agregar(new Regantes
                {
                    Nombre = "Alice",
                    Apellido = "Smith",
                    Cedula = "1357924680",
                    Contrasena = "password",
                    CorreoElectronico = "alice@example.com",
                    Direccion = "789 Street",
                    Nacionalidad = "USA",
                    Telefono = "135792468",
                    Usuario = "alice.smith"
                });

                // Act
                // Listar los regantes que cumplen con el criterio
                var regantes = await service.Listar(r => r.Apellido == "Doe");

                // Assert
                // Verificar que se hayan listado los regantes esperados
                Assert.AreEqual(2, regantes.Count);
                Assert.IsTrue(regantes.Any(r => r.Nombre == "John"));
                Assert.IsTrue(regantes.Any(r => r.Nombre == "Jane"));
            }
        }

    }
}
