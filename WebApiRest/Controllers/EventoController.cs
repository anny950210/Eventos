using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using WebApiRest.Models;
using WebApiRest.Negocio;

namespace WebApiRest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventoController : Controller
    {
        private readonly IBusinessContext _businessContext;

        public EventoController(IBusinessContext businessContext)
        {
            _businessContext = businessContext;
        }
        #region Evento

        [HttpGet("ObtenerEvento")]
        public List<Evento> ObtenerEvento(int? IdEvento = null)
        {
            return _businessContext.EventoObtener(IdEvento);
        }

        [HttpPost("InsertarEvento")]
        public Respuesta CrearEvento([FromBody] Evento objeto)
        {
            return _businessContext.EventoInsertar(objeto);
        }

        [HttpPost("ActualizarEvento")]
        public Respuesta ActualizarEvento([FromBody] Evento objeto)
        {
            return _businessContext.EventoActualizar(objeto);
        }

        [HttpDelete("EliminarEvento")]
        public Respuesta EliminarEvento(int IdEvento)
        {
            return _businessContext.EventoEliminar(IdEvento);
        }

        #endregion

        [HttpPost("ActualizarBoleto")]
        public Respuesta ActualizarBoleto([FromBody] Boleto objeto)
        {
            return _businessContext.BoletoActualizar(objeto);
        }

        [HttpGet("ObtenerBoleto")]
        public List<Boleto> ObtenerBoleto(int? IdBoleto = null)
        {
            return _businessContext.BoletoObtener(IdBoleto);
        }
    }
}
