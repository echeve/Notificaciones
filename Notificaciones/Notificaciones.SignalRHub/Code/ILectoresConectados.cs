using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Notificaciones.SignalRHub.Code
{
    public interface ILectoresConectados
    {
        void AnadirLector(string nuevoLector);

        void EliminarLector(string lector);

        string ObtenerLectoresConectados();
    }
}
