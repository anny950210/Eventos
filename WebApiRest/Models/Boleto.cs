namespace WebApiRest.Models
{
    public class Boleto
    {
        public int IdBoleto { get; set; }
        public string NombreCompraor { get; set; }
        public string FechaCompra { get; set; }
        public bool Vendio {  get; set; }
        public bool Canjeado { get; set; }
        public Evento Evento { get; set; }

    }
}
