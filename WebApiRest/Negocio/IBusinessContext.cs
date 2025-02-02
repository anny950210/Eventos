using WebApiRest.AccesoDatos;
using WebApiRest.Models;

namespace WebApiRest.Negocio
{
    public interface IBusinessContext
    {
        Respuesta EventoInsertar(Evento Evento);

    }
}
