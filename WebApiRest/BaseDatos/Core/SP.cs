namespace WebApiRest.BaseDatos.Core
{
    public class SP
    {
        public struct Eventos
        {
            public const string SpeEventoInsertar = "SpeEventoInsertar";
            public const string Usp_Productos_Ins = "Usp_Productos_Ins";
            public const string Usp_Productos_Upd = "Usp_Productos_Upd";
            public const string Usp_Productos_Del = "Usp_Productos_Del";
        }

        public struct Proveedores
        {
            public const string Usp_Proveedores_Obt = "Usp_Proveedores_Obt";
        }

        public struct TipoProducto
        {
            public const string Usp_TipoProducto_Obt = "Usp_TipoProducto_Obt";
        }
    }
}
