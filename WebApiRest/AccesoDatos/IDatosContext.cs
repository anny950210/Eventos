using WebApiRest.Models;

namespace WebApiRest.AccesoDatos
{
    public interface IDatosContext
    {
        List<Evento> EventoObtener(int? IdEvento = null);
        Respuesta EventoInsertar(Evento Evento);
        Respuesta EventoActualizar(Evento Evento, int? Nofilas = null, bool? Insertar = null);
        Respuesta EventoEliminar(int IdEvento);

        List<Boleto> BoletoObtener(int? IdBoleto = null);
        Respuesta BoletoActualizar(Boleto Boleto);

    }
}
