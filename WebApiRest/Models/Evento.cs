namespace WebApiRest.Models
{
    public class Evento
    {
        public int IdEvento { get; set; }
        public string Nombre { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public int NoBoletos { get; set; }
    }
}
