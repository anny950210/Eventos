namespace WebApiRest
{
    public class Constantes
    {
        public struct Numeros
        {
            public const int Cero = 0;
            public const int Trescientos = 300;
        }

        public struct Mensajes
        {
            public const string MaximoBoletos = "El número máximo de boletos es 300";
            public const string ValidacionFecha = "La fecha fin no puede ser menor a la fecha inicio";
            public const string ValidacionBoletos = "El numero de bolestos vendidos es maor a la cantidad que deseas actualizar";

            public const string ValidacionBoletosVendidos = "Hay al menos un boleto vendido para el evento";
            public const string ValidacionFechaVencida = "La fecha fin del evento aun no ha pasado";

            public const string ValidacionBoletoVendido = "El boleto ya ha sido vendido";
            public const string ValidacionBoletoCanjeado = "El boleto ya ha sido canjeado";
        }
    }
}
