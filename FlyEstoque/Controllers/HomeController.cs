using FlyEstoque.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using FlyEstoque.Data;

namespace FlyEstoque.Controllers
{
    public class HomeController : Controller
    {
        private readonly Contexto _contexto;
 
        public HomeController(Contexto contexto)
        {
            _contexto = contexto;
        }

        public IActionResult Index()
        {   
            ViewData["QuantProd"]   = _contexto.Produto.Count();
            ViewData["QuantMov"]    = _contexto.Movimento.Count();
            ViewData["FazerPed"]    = _contexto.Produto.Where(p => p.SaldoAtual <= p.EstoqueMin).Count();
            ViewData["EmFalta"]     = _contexto.Produto.Where(p => p.SaldoAtual <= 0).Count();

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
