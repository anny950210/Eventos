using System.Collections.Generic;
using WebApiRest.AccesoDatos;
using WebApiRest.BaseDatos.DAO;
using WebApiRest.Models;

namespace WebApiRest.Negocio
{
    public class BusinessContext : IBusinessContext
    {
        private readonly IDatosContext DatosContext;
        public BusinessContext(IDatosContext datosContext)
        {
            DatosContext = datosContext;
        }
        #region Evento
        public Respuesta EventoInsertar(Evento Evento)
        {
            if(Evento.NoBoletos > Constantes.Numeros.Trescientos)
            {
                Respuesta respuesta = new Respuesta();
                respuesta.IdError = 0;
                respuesta.Descripcion = Constantes.Mensajes.MaximoBoletos;
            }
            return DatosContext.EventoInsertar(Evento);
        }

        
        #endregion
        
    }
}
