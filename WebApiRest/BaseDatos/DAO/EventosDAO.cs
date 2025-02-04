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
        public List<Evento> EventoObtener(int? IdEvento = null)
        {
            List<Evento> lista = new List<Evento>();

            using (var conexion = new SqlConnection(conexiones.CadenaSQL))
            {

                conexion.Open();
                SqlCommand cmd = new SqlCommand(SP.Eventos.SpeEventoObtener, conexion);

                cmd.Parameters.AddWithValue("@IdEvento", IdEvento);



                cmd.CommandType = CommandType.StoredProcedure;

                cmd.ExecuteNonQuery();

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        lista.Add(new Evento
                        {
                            IdEvento = Convert.ToInt32(reader["IdEvento"].ToString()),
                            Nombre = reader["Nombre"].ToString(),
                            FechaInicio = Convert.ToDateTime(reader["FechaInicio"].ToString()),
                            FechaFin = Convert.ToDateTime(reader["FechaFin"].ToString()),
                            NoBoletos = Convert.ToInt32(reader["NoBoletos"].ToString()),
                            Boleto = new Boleto
                            {
                                NoBoletosDisponibles = Convert.ToInt32(reader["NoBoletosDisponibles"].ToString()),
                                NoBoletosVendidos = Convert.ToInt32(reader["NoBoletosVendidos"].ToString()),
                                NoBoletosCanjeados = Convert.ToInt32(reader["NoBoletosCanjeados"].ToString())
                            }
                        });
                    }
                }

                cmd.Parameters.Clear();
                conexion.Close();

            }
            return lista;
        }


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

        public Respuesta EventoActualizar(Evento Evento, int? Nofilas = null, bool? Insertar = null)
        {
            Respuesta Respuesta = new Respuesta();

            using (var conexion = new SqlConnection(conexiones.CadenaSQL))
            {

                conexion.Open();
                SqlCommand cmd = new SqlCommand(SP.Eventos.SpeEventoActualizar, conexion);

                cmd.Parameters.Add("@OutErrorNumber", SqlDbType.Int).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@OutErrorMessage", SqlDbType.NVarChar, -1).Direction = ParameterDirection.Output;
                cmd.Parameters.AddWithValue("@IdEvento", Evento.IdEvento);
                cmd.Parameters.AddWithValue("@Nombre", Evento.Nombre);
                cmd.Parameters.AddWithValue("@FechaInicio", Evento.FechaInicio);
                cmd.Parameters.AddWithValue("@FechaFin", Evento.FechaFin);
                cmd.Parameters.AddWithValue("@NoBoletos", Evento.NoBoletos);

                cmd.Parameters.AddWithValue("@Nofilas", Nofilas);
                cmd.Parameters.AddWithValue("@Insertar", Insertar);



                cmd.CommandType = CommandType.StoredProcedure;

                cmd.ExecuteNonQuery();

                Respuesta.IdError = (int)cmd.Parameters["@OutErrorNumber"].Value;
                Respuesta.Descripcion = (string)cmd.Parameters["@OutErrorMessage"].Value;


                cmd.Parameters.Clear();
                conexion.Close();

            }
            return Respuesta;
        }

        public Respuesta EventoEliminar(int IdEvento)
        {
            Respuesta Respuesta = new Respuesta();

            using (var conexion = new SqlConnection(conexiones.CadenaSQL))
            {

                conexion.Open();
                SqlCommand cmd = new SqlCommand(SP.Eventos.SpeEventoEliminar, conexion);

                cmd.Parameters.Add("@OutErrorNumber", SqlDbType.Int).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@OutErrorMessage", SqlDbType.NVarChar, -1).Direction = ParameterDirection.Output;
                cmd.Parameters.AddWithValue("@IdEvento", IdEvento);



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
        public Respuesta BoletoActualizar(Boleto Boleto)
        {
            Respuesta Respuesta = new Respuesta();

            using (var conexion = new SqlConnection(conexiones.CadenaSQL))
            {

                conexion.Open();
                SqlCommand cmd = new SqlCommand(SP.Boletos.SpeBoletoActualizar, conexion);

                cmd.Parameters.Add("@OutErrorNumber", SqlDbType.Int).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@OutErrorMessage", SqlDbType.NVarChar, -1).Direction = ParameterDirection.Output;
                cmd.Parameters.AddWithValue("@IdBoleto", Boleto.IdBoleto);
                cmd.Parameters.AddWithValue("@NombreComprador", Boleto.NombreComprador);
                cmd.Parameters.AddWithValue("@Vendido", Boleto.Vendido);
                cmd.Parameters.AddWithValue("@Canjeado", Boleto.Canjeado);



                cmd.CommandType = CommandType.StoredProcedure;

                cmd.ExecuteNonQuery();

                Respuesta.IdError = (int)cmd.Parameters["@OutErrorNumber"].Value;
                Respuesta.Descripcion = (string)cmd.Parameters["@OutErrorMessage"].Value;


                cmd.Parameters.Clear();
                conexion.Close();

            }
            return Respuesta;
        }

        public List<Boleto> BoletoObtener(int? IdBoleto = null)
        {
            List<Boleto> lista = new List<Boleto>();

            using (var conexion = new SqlConnection(conexiones.CadenaSQL))
            {

                conexion.Open();
                SqlCommand cmd = new SqlCommand(SP.Boletos.SpeBoletoObtener, conexion);

                cmd.Parameters.AddWithValue("@IdBoleto", IdBoleto);



                cmd.CommandType = CommandType.StoredProcedure;

                cmd.ExecuteNonQuery();

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        lista.Add(new Boleto
                        {
                            IdBoleto = Convert.ToInt32(reader["IdBoleto"].ToString()),
                            NombreComprador = reader["NombreComprador"].ToString(),
                            FechaCompra = !reader.IsDBNull(reader.GetOrdinal("FechaCompra")) ? Convert.ToDateTime(reader["FechaCompra"]) : null,
                            Vendido = Convert.ToBoolean(reader["Vendido"].ToString()),
                            Canjeado = Convert.ToBoolean(reader["Canjeado"].ToString())
                        });
                    }
                }

                cmd.Parameters.Clear();
                conexion.Close();

            }
            return lista;
        }
        #endregion



    }
}
