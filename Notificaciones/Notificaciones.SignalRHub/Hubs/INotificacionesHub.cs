using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Notificaciones.SignalRHub.Hubs
{
    public interface INotificacionesHub
    {
        Task AnadirLector();



        Task EnviarMensajeLectores(string mensaje);


    }
}
