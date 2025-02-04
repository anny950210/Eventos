using WebApiRest.AccesoDatos;
using WebApiRest.Models;

namespace WebApiRest.Negocio
{
    public interface IBusinessContext
    {
        List<Evento> EventoObtener(int? IdEvento = null);
        Respuesta EventoInsertar(Evento Evento);
        Respuesta EventoActualizar(Evento Evento, int? Nofilas = null, bool? Insertar = null);
        Respuesta EventoEliminar(int IdEvento);

        List<Boleto> BoletoObtener(int? IdBoleto = null);
        Respuesta BoletoActualizar(Boleto Boleto);
    }
}
