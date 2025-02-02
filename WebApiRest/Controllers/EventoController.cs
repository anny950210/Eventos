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
       
        [HttpPost]
        public Respuesta Crear([FromBody] Evento objeto)
        {
            return _businessContext.EventoInsertar(objeto);
        }

        #endregion

        
    }
}
