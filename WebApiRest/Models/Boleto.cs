using System.ComponentModel;
using Newtonsoft.Json;

namespace WebApiRest.Models
{
    public class Boleto
    {
        public int IdBoleto { get; set; }
        public string NombreComprador { get; set; }
        public DateTime? FechaCompra { get; set; }
        [JsonConverter(typeof(BooleanConverter))]
        public bool Vendido {  get; set; }
        [JsonConverter(typeof(BooleanConverter))]
        public bool Canjeado { get; set; }
        public int? NoBoletosDisponibles { get; set; }
        public int? NoBoletosVendidos { get; set; }
        public int? NoBoletosCanjeados { get; set; }

        [JsonConverter(typeof(Evento))]
        public Evento? Evento { get; set; }

    }
}
