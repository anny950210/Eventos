using Microsoft.Extensions.Options;
using System.Data;
using System.Data.SqlClient;
using WebApiRest.BaseDatos.Core;
using WebApiRest.Models;
using static WebApiRest.BaseDatos.Core.SP;

namespace WebApiRest.BaseDatos.DAO
{
    public class EventosDAO
    {
        private readonly ConnectionStrings conexiones;

        public EventosDAO(IOptions<ConnectionStrings> options)
        {
            conexiones = options.Value;
        }
        #region Evento
        public Respuesta EventoInsertar(Evento Evento)
        {
            Respuesta Respuesta = new Respuesta();

            using (var conexion = new SqlConnection(conexiones.CadenaSQL))
            {

                conexion.Open();
                SqlCommand cmd = new SqlCommand(SP.Eventos.SpeEventoInsertar, conexion);

                cmd.Parameters.Add("@OutErrorNumber", SqlDbType.Int).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@OutErrorMessage", SqlDbType.NVarChar, -1).Direction = ParameterDirection.Output;
                cmd.Parameters.AddWithValue("@Nombre", Evento.Nombre);
                cmd.Parameters.AddWithValue("@FechaInicio", Evento.FechaInicio);
                cmd.Parameters.AddWithValue("@FechaFin", Evento.FechaFin);
                cmd.Parameters.AddWithValue("@NoBoletos", Evento.NoBoletos);



                cmd.CommandType = CommandType.StoredProcedure;

                cmd.ExecuteNonQuery();

                Respuesta.IdError = (int)cmd.Parameters["@OutErrorNumber"].Value;
                Respuesta.Descripcion = (string)cmd.Parameters["@OutErrorMessage"].Value;


                cmd.Parameters.Clear();
                conexion.Close();

            }
            return Respuesta;
        }
        #endregion

        #region Boletos

        #endregion



    }
}
