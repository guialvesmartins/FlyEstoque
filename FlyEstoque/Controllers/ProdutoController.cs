using FlyEstoque.Data;
using FlyEstoque.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlyEstoque.Controllers
{
    public class ProdutoController : Controller
    {
        private readonly Contexto _contexto;
        public ProdutoController (Contexto contexto){            
            _contexto = contexto;
        }

        public async Task<IActionResult> Index()
        {
            var Contexto = _contexto.Produto.Include(c => c.GrupoProduto);
            return View(await Contexto.ToListAsync());
        }
        [HttpGet]
        public IActionResult Create()
        {
            //ListaGrupoProduto();
            ViewData["GrupoProdutoID"] = new SelectList(_contexto.GrupoProduto, "Id", "Descricao");
            ViewData["CodigoProduto"] = new SelectList(_contexto.Produto, "Id", "Codigo");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Produto produto)
        {
            var GrpProd = _contexto.GrupoProduto.Find(produto.GrupoProdutoId);
            var UltProd = _contexto.Produto.Where(p => p.GrupoProdutoId == GrpProd.Id).ToList();            

            if (UltProd.Count() != 0)
            {
                var Numeric = UltProd.Last().Codigo;
                var CodAtual = Int32.Parse(Numeric.Substring(2, 4));
                var ProxCod = CodAtual + 1;
                produto.Codigo = GrpProd.Codigo + ProxCod.ToString().PadLeft(4,'0');
            }
            else
            {
                produto.Codigo = GrpProd.Codigo + "0001";
            }

            if (ModelState.IsValid)
            {                
                _contexto.Add(produto);
                await _contexto.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            else
            {
                return View(produto);
            }

        }
        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if (id != null)
            {
                ViewData["GrupoProdutoID"] = new SelectList(_contexto.GrupoProduto, "Id", "Descricao");
                Produto produto = _contexto.Produto.Find(id);
                return View(produto);
            }
            else
            {
                return NotFound();
            }
        }
        [HttpPost]
        public async Task<IActionResult> Edit(int? id, Produto produto)
        {

            if (id != null)
            {
                if (ModelState.IsValid)
                {
                    _contexto.Update(produto);
                    await _contexto.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    return View(produto);
                }
            }
            else
            {
                return NotFound();
            }

        }
        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id != null)
            {
                ViewData["GrupoProdutoID"] = new SelectList(_contexto.GrupoProduto, "Id", "Descricao");
                Produto produto =  _contexto.Produto.Find(id);
                return View(produto);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int? id, Produto produto)
        {
            if (id != null)
            {
                
                _contexto.Remove(produto);
                await _contexto.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            else
            {
                return NotFound();
            }

        }        
    }
}
