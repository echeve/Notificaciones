using Microsoft.Extensions.Logging;
using Moq;
using Notificaciones.SignalRHub.Code;
using Notificaciones.SignalRHub.Hubs;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SignalR_UnitTestingSupport.Hubs;
using SignalR_UnitTestingSupportCommon.Hubs;
using System.Threading;

namespace Test
{
    [TestFixture]
    public class NotificacionesHubTest 
    {
        public NotificacionesHub _sut;
        public Mock<ILogger<NotificacionesHub>> _logger;
        public Mock<ILectoresConectados> _lectores;
        public HubUnitTestsSupport support;

        [SetUp]
        public void SetUp()
        {
            _logger = new Mock<ILogger<NotificacionesHub>>();
            _lectores = new Mock<ILectoresConectados>();

            support = new HubUnitTestsSupport();
            support.SetUp();
            _sut = new NotificacionesHub(_logger.Object, _lectores.Object);
            support.AssignToHubRequiredProperties(_sut);
        }

        [Test]
        public void DADO_un_Hub_CUANDO_inserto_un_lector_ENTONCES_lo_inserto_en_la_lista()
        {

            var resultado = _sut.AnadirLector();

            _lectores.Verify(mock => mock.AnadirLector(It.IsAny<string>()), Times.Once);

        }

    }
}
