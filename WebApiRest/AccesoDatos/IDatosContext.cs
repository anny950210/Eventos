using WebApiRest.Models;

namespace WebApiRest.AccesoDatos
{
    public interface IDatosContext
    {
        Respuesta EventoInsertar(Evento Evento);
    }
}
