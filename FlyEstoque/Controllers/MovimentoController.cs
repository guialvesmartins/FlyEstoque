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
    public class MovimentoController : Controller
    {
        private readonly Contexto _contexto;
        public MovimentoController(Contexto contexto)
        {
            _contexto = contexto;
        }
        public async Task<IActionResult> Index()
        {
            var Contexto = _contexto.Movimento.Include(c => c.Produto);
            return View(await Contexto.ToListAsync());
        }
        [HttpGet]
        public IActionResult Create()
        {
           var produtos = _contexto.Produto.Select(p => new
            {
                Text = p.Codigo + " ( Cod.Barra: " + p.CodBarras + " ) " + p.Descricao,
                Value = p.Id
            }).ToList();

            Movimento model = new()
            {
                DtMovimento = DateTime.Now
            };

            ViewData["ProdutoID"] = new SelectList(produtos.OrderBy(x=> x.Text), "Value", "Text");
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Create(Movimento movimento)
        {
            if (ModelState.IsValid)
            {   
                _contexto.Add(movimento);
                var produto = _contexto.Produto.Find(movimento.ProdutoId);
                if (movimento.TipoMovimento.ToString().Equals("E"))
                {
                    produto.SaldoAtual = produto.SaldoAtual + movimento.Quantidade;
                }else if (movimento.TipoMovimento.ToString().Equals("S"))
                {
                    produto.SaldoAtual = produto.SaldoAtual - movimento.Quantidade;
                }
                await _contexto.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            else
            {
                return View(movimento);
            }
        }
        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if (id != null)
            {
                Movimento movimento = _contexto.Movimento.Find(id);
                Produto produto = _contexto.Produto.Find(movimento.ProdutoId);
                ViewData["ProdutoCodigo"] = produto.Codigo;
                ViewData["ProdutoDesc"] = produto.Descricao;
                return View(movimento);
            }
            else
            {
                return NotFound();
            }
        }
        [HttpPost]
        public async Task<IActionResult> Edit(int? id, Movimento movimento)
        {
            if (id != null)
            {
                if (ModelState.IsValid)
                {
                    _contexto.Update(movimento);
                    await _contexto.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    return View(movimento);
                }
            }
            else
            {
                return NotFound();
            }
        }
        [HttpGet]
        public IActionResult Delete(int? id)
        {
            if (id != null)
            {
                Movimento movimento = _contexto.Movimento.Find(id);
                Produto produto = _contexto.Produto.Find(movimento.ProdutoId);
                ViewData["ProdutoCodigo"] = produto.Codigo;
                ViewData["ProdutoDesc"] = produto.Descricao;
                return View(movimento);
            }
            else
            {
                return NotFound();
            }
        }
        [HttpPost]
        public async Task<IActionResult> Delete(int? id, Movimento movimento)
        {
            if (id != null)
            {
                var mov = _contexto.Movimento.Find(id);
                var produto = _contexto.Produto.Find(mov.ProdutoId);

                if (mov.TipoMovimento.ToString().Equals("E"))
                {
                    produto.SaldoAtual = produto.SaldoAtual - mov.Quantidade;
                }
                else if (mov.TipoMovimento.ToString().Equals("S"))
                {
                    produto.SaldoAtual = produto.SaldoAtual + mov.Quantidade;
                }
                _contexto.Remove(mov);
                await _contexto.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            else
            {
                return NotFound();
            }

        }
        [HttpGet]
        public IActionResult Inventario()
        {
            var produtos = _contexto.Produto.Select(p => new
            {
                Text = p.Codigo + " ( Cod.Barra: " + p.CodBarras + " ) " + p.Descricao,
                Value = p.Id
            }).ToList();

            Movimento model = new()
            {
                DtMovimento = DateTime.Now
            };

            ViewData["ProdutoID"] = new SelectList(produtos.OrderBy(x => x.Text), "Value", "Text");
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Inventario(Movimento movimento)
        {
            if (ModelState.IsValid)
            {
                _contexto.Add(movimento);
                var produto = _contexto.Produto.Find(movimento.ProdutoId);
                if (movimento.TipoMovimento.ToString().Equals("I"))
                {
                    produto.SaldoAtual = movimento.Quantidade;
                }

                await _contexto.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            else
            {
                return View(movimento);
            }
        }
    }
}
