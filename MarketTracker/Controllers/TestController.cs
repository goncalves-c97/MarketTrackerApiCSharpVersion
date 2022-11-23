using MarketTracker.Entities;
using MarketTracker.Interfaces;
using MarketTracker.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MarketTracker.Controllers
{
    [Route("Api/Test/")]
    public class TestController : ControllerBase
    {
        [HttpGet]
        [Route("TestAllDistancesByFESA"), AllowAnonymous]
        public ActionResult<string> TestAllDistances([FromServices] IMercados _mercadosRepo)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            List<MERCADOS> mercados = _mercadosRepo.ListarMercados();

            string retorno = string.Empty;

            foreach(MERCADOS mercado in mercados)
            {
                retorno += $"Mercado = {mercado.NOME}{Environment.NewLine}";
                retorno += $"Distância = {CoordinatesHelper.GetDistanceBetweenPoints(-23.737517515019437, -46.583824240136025, Convert.ToDouble(mercado.LAT), Convert.ToDouble(mercado.LONG))}{Environment.NewLine}{Environment.NewLine}";
            }

            return Ok(retorno);
        }
    }
}
