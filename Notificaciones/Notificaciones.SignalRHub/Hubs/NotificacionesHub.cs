using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using Notificaciones.SignalRHub.Code;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Notificaciones.SignalRHub.Hubs
{
    public class NotificacionesHub : Hub<INotificacionesHub>
    {

        private readonly ILogger<NotificacionesHub> _logger;
        private readonly ILectoresConectados _lectores;
        public NotificacionesHub(ILogger<NotificacionesHub> logger
            , ILectoresConectados lectores)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _lectores = lectores ?? throw new ArgumentNullException(nameof(lectores));
        }
        public override Task OnConnectedAsync()
        {
            _logger.LogInformation($"Nuevo usuario conectado: {Context.ConnectionId}");
            return base.OnConnectedAsync();
        }

        public override Task OnDisconnectedAsync(Exception exception)
        {
            _logger.LogInformation($"Usuario desconectado: {Context.ConnectionId}");
            _lectores.EliminarLector(Context.ConnectionId);
            return base.OnDisconnectedAsync(exception);
        }


        public async Task AnadirLector()
        {
            _logger.LogInformation($"Nuevo lector conectado: {Context.ConnectionId}");
            _lectores.AnadirLector(Context.ConnectionId);
            await Groups.AddToGroupAsync(Context.ConnectionId, Constantes.LECTOR);
        }



        public async Task EnviarMensajeLectores(string mensaje)
        {
            _logger.LogInformation($"Mensaje enviado a los lectores: {mensaje}");
            await Clients.Group(Constantes.LECTOR).EnviarMensajeLectores(mensaje);
        }
    }
}
