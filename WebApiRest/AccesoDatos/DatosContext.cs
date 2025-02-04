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
        public List<Evento> EventoObtener(int? IdEvento = null) 
        {
            List<Evento> lista = _eventoDAO.EventoObtener(IdEvento);
            return lista;
        } 

        public Respuesta EventoInsertar(Evento Evento)
        {
            Respuesta respuesta = _eventoDAO.EventoInsertar(Evento);
            return respuesta;
        }

        public Respuesta EventoActualizar(Evento Evento, int? Nofilas = null, bool? Insertar = null)
        {
            Respuesta respuesta = _eventoDAO.EventoActualizar(Evento, Nofilas, Insertar);
            return respuesta;
        }

        public Respuesta EventoEliminar(int IdEvento)
        {
            Respuesta respuesta = _eventoDAO.EventoEliminar(IdEvento);
            return respuesta;
        }
        #endregion

        public Respuesta BoletoActualizar(Boleto Boleto)
        {
            Respuesta respuesta = _eventoDAO.BoletoActualizar(Boleto);
            return respuesta;
        }

        public List<Boleto> BoletoObtener(int? IdBoleto = null)
        {
            List<Boleto> lista = _eventoDAO.BoletoObtener(IdBoleto);
            return lista;
        }
    }
}
