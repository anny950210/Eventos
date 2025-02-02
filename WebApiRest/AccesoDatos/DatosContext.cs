using WebApiRest.BaseDatos.DAO;
using WebApiRest.Models;

namespace WebApiRest.AccesoDatos
{
    public class DatosContext : IDatosContext
    {
        private readonly EventosDAO _eventoDAO;
        public DatosContext(EventosDAO eventoDAO)
        {
            _eventoDAO = eventoDAO;
        }
        #region Eventos
        public Respuesta EventoInsertar(Evento Evento)
        {
            Respuesta respuesta = _eventoDAO.EventoInsertar(Evento);
            return respuesta;
        }

        
        #endregion

    }
}
