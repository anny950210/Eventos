using Microsoft.CodeAnalysis.CSharp.Syntax;
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
            Respuesta respuesta = new Respuesta();
            if (Evento.NoBoletos > Constantes.Numeros.Trescientos)
            { 
                respuesta.IdError = 0;
                respuesta.Descripcion = Constantes.Mensajes.MaximoBoletos;
            }
            else if (Evento.FechaFin < Evento.FechaInicio)
            { 
                respuesta.IdError = 0;
                respuesta.Descripcion = Constantes.Mensajes.ValidacionFecha;
            }
            else
            {
                respuesta = DatosContext.EventoInsertar(Evento);
            }
        
            return respuesta;
        }

        public List<Evento> EventoObtener(int? IdEvento = null)
        {
            return DatosContext.EventoObtener(IdEvento);
        }

        public Respuesta EventoActualizar(Evento Evento, int? Nofilas = null, bool? Insertar = null)
        {

            Respuesta respuesta = new Respuesta();
            Evento Even = EventoObtener(Evento.IdEvento).FirstOrDefault();

            int BoletosTotal = Even.NoBoletos + Evento.NoBoletos;
            int BoletosDif = Evento.NoBoletos - Even.NoBoletos;
            int numfilas = 0;
            bool insertarfilas = false;

            if(BoletosDif < 0)
            {
                numfilas = (BoletosDif)*(-1);
                insertarfilas = false;
            }
            else
            {
                numfilas = BoletosDif;
                insertarfilas = true;
            }
            

            if (BoletosTotal > Constantes.Numeros.Trescientos)
            {
                respuesta.IdError = 0;
                respuesta.Descripcion = Constantes.Mensajes.MaximoBoletos;
            }
            else if (Evento.FechaFin < Evento.FechaInicio)
            {
                respuesta.IdError = 0;
                respuesta.Descripcion = Constantes.Mensajes.ValidacionFecha;
            }
            else if (Even.Boleto.NoBoletosVendidos > Evento.NoBoletos)
            {
                respuesta.IdError = 0;
                respuesta.Descripcion = Constantes.Mensajes.ValidacionBoletos;
            }
            else
            {
                respuesta = DatosContext.EventoActualizar(Evento, numfilas,insertarfilas);
            }

            return respuesta;
        }

        public Respuesta EventoEliminar(int IdEvento)
        {
            Respuesta respuesta = new Respuesta();
            Evento Even = EventoObtener(IdEvento).FirstOrDefault();

            if (Even.Boleto.NoBoletosVendidos > Constantes.Numeros.Cero)
            {
                respuesta.IdError = 0;
                respuesta.Descripcion = Constantes.Mensajes.ValidacionBoletosVendidos;
            }
            else if (DateTime.Now < Even.FechaFin)
            {
                respuesta.IdError = 0;
                respuesta.Descripcion = Constantes.Mensajes.ValidacionFechaVencida;
            }
            else
            {
                respuesta = DatosContext.EventoEliminar(IdEvento);
            }

            return respuesta;
        }
        #endregion
        public Respuesta BoletoActualizar(Boleto Boleto)
        { 

            Respuesta respuesta = new Respuesta();
            Boleto Bol = BoletoObtener(Boleto.IdBoleto).FirstOrDefault();

            if (Boleto.Canjeado && Bol.Canjeado)
            {
                respuesta.IdError = 0;
                respuesta.Descripcion = Constantes.Mensajes.ValidacionBoletoCanjeado;
            }
            else if(Boleto.Vendido && Bol.Vendido)
            {
                respuesta.IdError = 0;
                respuesta.Descripcion = Constantes.Mensajes.ValidacionBoletoVendido;
            }
            else
            {
                respuesta = DatosContext.BoletoActualizar(Boleto);
            }

            return respuesta;
        }

        public List<Boleto> BoletoObtener(int? IdBoleto = null)
        {
            return DatosContext.BoletoObtener(IdBoleto);
        }
    }
}
