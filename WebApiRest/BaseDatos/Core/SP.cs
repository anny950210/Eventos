namespace WebApiRest.BaseDatos.Core
{
    public class SP
    {
        public struct Eventos
        {
            public const string SpeEventoInsertar = "SpeEventoInsertar";
            public const string SpeEventoObtener = "SpeEventoObtener";
            public const string SpeEventoActualizar = "SpeEventoActualizar";
            public const string SpeEventoEliminar = "SpeEventoEliminar";
        }

        public struct Boletos
        {
            public const string SpeBoletoObtener = "SpeBoletoObtener";
            public const string SpeBoletoActualizar = "SpeBoletoActualizar";
        }
    }
}
