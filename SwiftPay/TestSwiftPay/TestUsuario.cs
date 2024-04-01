using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SwiftPay.DAL;
using SwiftPay.Models;
using SwiftPay.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace TestJunta
{
    [TestClass]
    public class TestUsuarios
    {
        [TestMethod]
        public async Task IniciarSesion_ValidCredentials_ReturnsUser()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<Context>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            using (var context = new Context(options))
            {
                var service = new UsuariosService(context);
                var usuario = new Usuarios
                {
                    Nombre = "John",
                    Apellido = "Doe",
                    Cedula = "123456789",
                    Usuario = "john_doe",
                    Contrasena = "password",
                    Telefono = "1234567890",
                    CorreoElectronico = "john.doe@example.com",
                    Direccion = "123 Main St",
                    Rol = "Usuario"
                };

                await service.Agregar(usuario);

                // Act
                var resultado = await service.IniciarSesion("john_doe", "password");

                // Assert
                Assert.IsNotNull(resultado);
                Assert.AreEqual("John", resultado.Nombre);
                Assert.AreEqual("Doe", resultado.Apellido);
            }
        }

        [TestMethod]
        public async Task VerificarUsuarioFalse()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<Context>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            using (var context = new Context(options))
            {
                var service = new UsuariosService(context);

                // Act
                var resultado = await service.Verificar(-1); // ID que no existe en la base de datos

                // Assert
                Assert.IsFalse(resultado);
            }
        }

        [TestMethod]
        public async Task VerificarUsuarioTrue()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<Context>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            using (var context = new Context(options))
            {
                var service = new UsuariosService(context);
                var nuevoUsuario = new Usuarios
                {
                    Nombre = "Jane",
                    Apellido = "Doe",
                    Cedula = "987654321",
                    Usuario = "jane_doe",
                    Contrasena = "password",
                    Telefono = "9876543210",
                    CorreoElectronico = "jane.doe@example.com",
                    Direccion = "456 Elm St",
                    Rol = "Usuario"
                };

                await service.Agregar(nuevoUsuario);

                // Act
                var resultado = await service.Verificar(nuevoUsuario.UsuarioId);

                // Assert
                Assert.IsTrue(resultado);
            }
        }

        [TestMethod]
        public async Task AgregarUsuario()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<Context>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            using (var context = new Context(options))
            {
                var service = new UsuariosService(context);
                var nuevoUsuario = new Usuarios
                {
                    Nombre = "Jane",
                    Apellido = "Doe",
                    Cedula = "987654321",
                    Usuario = "jane_doe",
                    Contrasena = "password",
                    Telefono = "9876543210",
                    CorreoElectronico = "jane.doe@example.com",
                    Direccion = "456 Elm St",
                    Rol = "Usuario"
                };

                // Act
                var resultado = await service.Agregar(nuevoUsuario);

                // Assert
                Assert.IsTrue(resultado);
            }
        }

        [TestMethod]
        public async Task ModificarUsuario()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<Context>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            using (var context = new Context(options))
            {
                var service = new UsuariosService(context);
                var nuevoUsuario = new Usuarios
                {
                    Nombre = "Jane",
                    Apellido = "Doe",
                    Cedula = "987654321",
                    Usuario = "jane_doe",
                    Contrasena = "password",
                    Telefono = "9876543210",
                    CorreoElectronico = "jane.doe@example.com",
                    Direccion = "456 Elm St",
                    Rol = "Usuario"
                };

                await service.Agregar(nuevoUsuario);

                // Modificar el usuario
                nuevoUsuario.Nombre = "Janet";

                // Act
                var resultado = await service.Modificar(nuevoUsuario);

                // Assert
                Assert.IsTrue(resultado);

                // Verificar que el usuario se haya modificado correctamente
                var usuarioModificado = await service.Buscar(nuevoUsuario.UsuarioId);
                Assert.IsNotNull(usuarioModificado);
                Assert.AreEqual("Janet", usuarioModificado.Nombre);
            }
        }

        [TestMethod]
        public async Task GuardarUsuario()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<Context>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            using (var context = new Context(options))
            {
                var service = new UsuariosService(context);
                var nuevoUsuario = new Usuarios
                {
                    Nombre = "Jane",
                    Apellido = "Doe",
                    Cedula = "987654321",
                    Usuario = "jane_doe",
                    Contrasena = "password",
                    Telefono = "9876543210",
                    CorreoElectronico = "jane.doe@example.com",
                    Direccion = "456 Elm St",
                    Rol = "Usuario"
                };

                // Act - Guardar el usuario por primera vez
                var resultadoGuardar = await service.Guardar(nuevoUsuario);

                // Assert - Verificar que se guardó correctamente
                Assert.IsTrue(resultadoGuardar);
                var usuarioGuardado = await service.Buscar(nuevoUsuario.UsuarioId);
                Assert.IsNotNull(usuarioGuardado);
                Assert.AreEqual("Jane", usuarioGuardado.Nombre);

                // Modificar el usuario y guardarlo nuevamente
                nuevoUsuario.Nombre = "Janet";

                // Act - Guardar el usuario modificado
                var resultadoModificar = await service.Guardar(nuevoUsuario);

                // Assert - Verificar que se modificó correctamente
                Assert.IsTrue(resultadoModificar);
                var usuarioModificado = await service.Buscar(nuevoUsuario.UsuarioId);
                Assert.IsNotNull(usuarioModificado);
                Assert.AreEqual("Janet", usuarioModificado.Nombre);
            }
        }

        [TestMethod]
        public async Task EliminarUsuario()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<Context>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            using (var context = new Context(options))
            {
                var service = new UsuariosService(context);
                var nuevoUsuario = new Usuarios
                {
                    Nombre = "Jane",
                    Apellido = "Doe",
                    Cedula = "987654321",
                    Usuario = "jane_doe",
                    Contrasena = "password",
                    Telefono = "9876543210",
                    CorreoElectronico = "jane.doe@example.com",
                    Direccion = "456 Elm St",
                    Rol = "Usuario"
                };

                // Act - Agregar el usuario
                await service.Agregar(nuevoUsuario);

                // Act - Eliminar el usuario
                var resultadoEliminar = await service.Eliminar(nuevoUsuario);

                // Assert - Verificar que se eliminó correctamente
                Assert.IsTrue(resultadoEliminar);

                // Verificar que el usuario no se pueda encontrar después de la eliminación
                var usuarioEliminado = await service.Buscar(nuevoUsuario.UsuarioId);
                Assert.IsNull(usuarioEliminado);
            }
        }

        [TestMethod]
        public async Task ListarUsuarios()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<Context>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            using (var context = new Context(options))
            {
                var service = new UsuariosService(context);

                // Agregar varios usuarios con todas las propiedades requeridas
                await service.Agregar(new Usuarios
                {
                    Nombre = "John",
                    Apellido = "Doe",
                    Cedula = "123456789",
                    Contrasena = "password",
                    CorreoElectronico = "john.doe@example.com",
                    Direccion = "123 Main St",
                    Rol = "Usuario",
                    Telefono = "1234567890",
                    Usuario = "johndoe"
                });
                await service.Agregar(new Usuarios
                {
                    Nombre = "Jane",
                    Apellido = "Doe",
                    Cedula = "987654321",
                    Contrasena = "password",
                    CorreoElectronico = "jane.doe@example.com",
                    Direccion = "456 Elm St",
                    Rol = "Usuario",
                    Telefono = "9876543210",
                    Usuario = "janedoe"
                });
                await service.Agregar(new Usuarios
                {
                    Nombre = "Alice",
                    Apellido = "Smith",
                    Cedula = "654321987",
                    Contrasena = "password",
                    CorreoElectronico = "alice.smith@example.com",
                    Direccion = "789 Oak St",
                    Rol = "Usuario",
                    Telefono = "4567891230",
                    Usuario = "alicesmith"
                });

                // Act
                // Listar los usuarios que cumplen con el criterio
                var usuarios = await service.Listar(u => u.Apellido == "Doe");

                // Assert
                // Verificar que se hayan listado los usuarios esperados
                Assert.AreEqual(3, usuarios.Count);
                Assert.IsTrue(usuarios.Any(u => u.Nombre == "John"));
                Assert.IsTrue(usuarios.Any(u => u.Nombre == "Jane"));
                Assert.IsTrue(usuarios.Any(u => u.Nombre == "Alice"));
            }
        }

    }
}

