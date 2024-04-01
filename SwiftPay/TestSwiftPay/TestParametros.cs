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
    public class TestParametros
    {
        [TestMethod]
        public async Task VerificarPOFalse()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<Context>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            using (var context = new Context(options))
            {
                var service = new ParametrosService(context);

                // Act
                var resultado = await service.Verificar(-1); // ID que no existe en la base de datos

                // Assert
                Assert.IsFalse(resultado);
            }
        }

        [TestMethod]
        public async Task VerificarPOTrue()
        {
            // Permite usar una base de datos en memoria para no alterar o usar la base de datos actual
            var options = new DbContextOptionsBuilder<Context>()
            .UseInMemoryDatabase(databaseName: "TestDatabase")
            .Options;

            using (var context = new Context(options))
            {
                var service = new ParametrosService(context);
                var nuevoPO = new Parametros
                {
                     MontoBomba = 8,
                     MontoGravedad = 8,
                     PorcentajeRecargos = 8,
                     TiempoProximoPago = 8,
                };

                await service.Agregar(nuevoPO);

                // Verificamos
                var resultado = await service.Verificar(nuevoPO.ParametroOperatacionalesId);

                // Assert
                Assert.IsTrue(resultado);
            }
        }

        [TestMethod]
        public async Task AgregarPO()
        {
            // Permite usar una base de datos en memoria para no alterar o usar la base de datos actual
            var options = new DbContextOptionsBuilder<Context>()
            .UseInMemoryDatabase(databaseName: "TestDatabase")
            .Options;

            using (var context = new Context(options))
            {
                var service = new ParametrosService(context);
                var nuevoPO = new Parametros
                {
                    MontoBomba = 8,
                    MontoGravedad = 8,
                    PorcentajeRecargos = 8,
                    TiempoProximoPago = 8
                };

                // Act -- El resultado que se espera 
                var resultado = await service.Agregar(nuevoPO);

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
                var service = new ParametrosService(context);
                var nuevoPO = new Parametros
                {
                    MontoBomba = 8,
                    MontoGravedad = 8,
                    PorcentajeRecargos = 8,
                    TiempoProximoPago = 8,
                };

                await service.Agregar(nuevoPO);

                // Modificamos 
                nuevoPO.MontoBomba = 10;

                // Act
                var resultado = await service.Modificar(nuevoPO);

                // Assert
                Assert.IsTrue(resultado);

                // Verificamos 
                var POModificado = await service.Buscar(nuevoPO.ParametroOperatacionalesId);
                Assert.IsNotNull(POModificado);
                Assert.AreEqual(10, POModificado.MontoBomba); //aqui
            }
        }

        [TestMethod]
        public async Task GuardarPO()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<Context>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            using (var context = new Context(options))
            {
                var service = new ParametrosService(context);
                var nuevoPO = new Parametros
                {
                    MontoBomba = 8,
                    MontoGravedad = 8,
                    PorcentajeRecargos = 8,
                    TiempoProximoPago = 8,
                };

                // Act - Guardar 
                var resultadoGuardar = await service.Guardar(nuevoPO);

                // Assert - Verificar que se guardó correctamente
                Assert.IsTrue(resultadoGuardar);
                var POGuardado = await service.Buscar(nuevoPO.ParametroOperatacionalesId);
                Assert.IsNotNull(POGuardado);
                Assert.AreEqual(8, POGuardado.MontoBomba);

                // Modificar
                nuevoPO.MontoBomba = 85;

                // Act - Guardar 
                var resultadoModificar = await service.Guardar(nuevoPO);

                // Assert - Verificar que se modificó correctamente
                Assert.IsTrue(resultadoModificar);
                var POnModificado = await service.Buscar(nuevoPO.ParametroOperatacionalesId);
                Assert.IsNotNull(POnModificado);
                Assert.AreEqual(85, POnModificado.MontoBomba);
            }
        }

        [TestMethod]
        public async Task EliminarParametros()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<Context>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            using (var context = new Context(options))
            {
                var service = new ParametrosService(context);
                var nuevoPO = new Parametros
                {
                    MontoBomba = 8,
                    MontoGravedad = 8,
                    PorcentajeRecargos = 8,
                    TiempoProximoPago = 8,
                };

                // Act - Agregar 
                await service.Agregar(nuevoPO);

                // Act - Eliminar la asociación
                var resultadoEliminar = await service.Eliminar(nuevoPO);

                // Assert - Verificar que se eliminó correctamente
                Assert.IsTrue(resultadoEliminar);

                // Verificar 
                var Eliminado = await service.Buscar(nuevoPO.ParametroOperatacionalesId);
                Assert.IsNull(Eliminado);
            }
        }

        [TestMethod]
        public async Task ListarPO()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<Context>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            using (var context = new Context(options))
            {
                var service = new ParametrosService(context);

                // Agregar varias asociaciones
                await service.Agregar(new Parametros
                {
                    ParametroOperatacionalesId = 10,
                    MontoBomba = 8,
                    MontoGravedad = 8,
                    PorcentajeRecargos = 8,
                    TiempoProximoPago = 8,
                });
                await service.Agregar(new Parametros
                {
                    ParametroOperatacionalesId = 11,
                    MontoBomba = 8,
                    MontoGravedad = 8,
                    PorcentajeRecargos = 8,
                    TiempoProximoPago = 8,
                });
                await service.Agregar(new Parametros
                {
                    ParametroOperatacionalesId = 12,
                    MontoBomba = 8,
                    MontoGravedad = 8,
                    PorcentajeRecargos = 8,
                    TiempoProximoPago = 8,
                });

                // Act
                // Listar 
                var PO = await service.Listar(po => po.ParametroOperatacionalesId >= 10 && po.ParametroOperatacionalesId <= 12);

                // Assert
                // Verificar que se hayan listado las asociaciones esperadas
                Assert.AreEqual(3, PO.Count); // Verificar el número de elementos en la lista
                Assert.IsTrue(PO.Any(po => po.ParametroOperatacionalesId == 10)); // Verificar la presencia del ID 10
                Assert.IsTrue(PO.Any(po => po.ParametroOperatacionalesId == 11)); // Verificar la presencia del ID 11
                Assert.IsTrue(PO.Any(po => po.ParametroOperatacionalesId == 12)); // Verificar la presencia del ID 12
            }
        }

    }
}

