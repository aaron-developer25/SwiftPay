using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.EntityFrameworkCore;
using SwiftPay.DAL;
using SwiftPay.Models;
using SwiftPay.Services;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace TestJunta
{
    [TestClass]
    public class TestDetallePagos
    {
        [TestMethod]
        public async Task InsertarDetalle()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<Context>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            using (var context = new Context(options))
            {
                var service = new DetallePagosService(context);
                var nuevoDetallePago = new DetallePagos();

                // Act
                var resultado = await service.Insertar(nuevoDetallePago);

                // Assert
                Assert.IsTrue(resultado);
            }
        }


        [TestMethod]
        public async Task BuscarDetalle()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<Context>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            using (var context = new Context(options))
            {
                var service = new DetallePagosService(context);
                var detallePago = new DetallePagos { DetallePagoId = 1 };
                context.Add(detallePago);
                await context.SaveChangesAsync();

                // Act
                var result = await service.Buscar(1);

                // Assert
                Assert.IsNotNull(result);
                Assert.AreEqual(1, result.DetallePagoId);
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
                var service = new DetallePagosService(context);

                // Agregar algunas entidades de ejemplo con claves únicas
                var detallePago1 = new DetallePagos { DetallePagoId = 2 };
                var detallePago2 = new DetallePagos { DetallePagoId = 3 };

                context.AddRange(detallePago1, detallePago2);
                await context.SaveChangesAsync();

                // Act
                var result = await service.Listar(d => d.DetallePagoId > 0);

                // Assert
                Assert.AreEqual(2, result.Count);
            }
        }

    }


}





