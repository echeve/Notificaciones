using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using Notificaciones.SignalRHub.Code;
using Notificaciones.SignalRHub.Hubs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Notificaciones.SignalRHub.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SuscriptoresController : ControllerBase
    {
        private readonly IHubContext<NotificacionesHub> _notificaciones;
        private readonly ILectoresConectados _lectores;

        public SuscriptoresController(IHubContext<NotificacionesHub> notificaciones
            , ILectoresConectados lectores)
        {
            _notificaciones = notificaciones ?? throw new ArgumentNullException(nameof(notificaciones));
            _lectores = lectores ?? throw new ArgumentNullException(nameof(lectores));


        }

        // GET: api/<SuscriptoresController>
        [HttpGet]
        public string Get()
        {
            var lectoresConectados = _lectores.ObtenerLectoresConectados();
            return lectoresConectados;
        }


        // POST api/<SuscriptoresController>
        [HttpPost]
        [Route("Notificar")]
        public void Post(string value)
        {
            _notificaciones.Clients.Group(Constantes.LECTOR).SendAsync("EnviarMensajeLectores", value);

        }

    }
}
