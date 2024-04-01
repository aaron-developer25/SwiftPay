using Microsoft.JSInterop;
using Moq;
using SwiftPay.Models;
using SwiftPay.Services;
using Radzen;
using Xunit;

namespace TestJunta
{
    public class TestAutentificacion
    {
        [Fact]
        public async Task Acceder_ValidCredentials_CallsJavaScriptInteropAndReturnsUser()
        {
            // Arrange
            var jsRuntimeMock = new Mock<IJSRuntime>();
            var usuariosServiceMock = new Mock<UsuariosService>();
            var notificationServiceMock = new Mock<NotificationService>();

            var autentificacionService = new AutentificacionService();

            // Setup UsuariosService mock retorna un usuario valido
            usuariosServiceMock.Setup(x => x.IniciarSesion(It.IsAny<string>(), It.IsAny<string>()))
                .ReturnsAsync(new Usuarios { UsuarioId = 1 });

            // Act
            await autentificacionService.Acceder(jsRuntimeMock.Object, usuariosServiceMock.Object, notificationServiceMock.Object, "user", "password");

            // Assert
            jsRuntimeMock.Verify(x => x.InvokeVoidAsync(It.IsAny<string>(), It.IsAny<object[]>()), Times.Exactly(3));

            notificationServiceMock.Verify(x => x.Notify(It.IsAny<NotificationMessage>()), Times.Never());
        }
    }
}
