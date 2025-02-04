using System.ComponentModel;
using Newtonsoft.Json;

namespace WebApiRest.Models
{
    public class Evento
    {
        public int IdEvento { get; set; }
        public string Nombre { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public int NoBoletos { get; set; }

        [JsonConverter(typeof(Boleto))]
        public Boleto? Boleto { get; set; }
    }
}
