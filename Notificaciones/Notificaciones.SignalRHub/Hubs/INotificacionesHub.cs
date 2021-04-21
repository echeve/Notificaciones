using System.Threading.Tasks;

namespace Notificaciones.SignalRHub.Hubs
{
    public interface INotificacionesHub
    {
        Task AnadirLector();



        Task EnviarMensajeLectores(string mensaje);


    }
}
