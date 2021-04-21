using System;
using System.Collections.Generic;
using System.Linq;

namespace Notificaciones.SignalRHub.Code
{
    public class LectoresConectados : ILectoresConectados
    {
        private static IList<string> lectoresConectados ;
        public LectoresConectados()
        {
            lectoresConectados = new List<string>();
        }

        public void AnadirLector(string nuevoLector)
        {
            if (!lectoresConectados.Any(lc => string.Equals(lc, nuevoLector)))
            {
                lectoresConectados.Add(nuevoLector);
            }
        }

        public void EliminarLector(string lector)
        {
            if (lectoresConectados.Any(lc => string.Equals(lc, lector)))
            {
                lectoresConectados.Remove(lector);
            }
        }

        public string ObtenerLectoresConectados()
        {
            var lectores = string.Empty;
            foreach (var item in lectoresConectados)
            {
                lectores += $"usuario: {item} {Environment.NewLine}" ;
            }
            return lectores;
        }

        public IList<string> ObtenerLectores()
        {
            return lectoresConectados;
        }
    }
}
