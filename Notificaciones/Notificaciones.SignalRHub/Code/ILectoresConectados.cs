namespace Notificaciones.SignalRHub.Code
{
    public interface ILectoresConectados
    {
        void AnadirLector(string nuevoLector);

        void EliminarLector(string lector);

        string ObtenerLectoresConectados();
    }
}
