using FlyEstoque.Data;
using FlyEstoque.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlyEstoque.Controllers
{

    public class GrupoProdutoController : Controller
    {
        private readonly Contexto _contexto;
        public GrupoProdutoController(Contexto contexto)
        {
            _contexto = contexto;
        }
        public async Task<IActionResult> Index()
        {
            return View(await _contexto.GrupoProduto.ToListAsync());
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(GrupoProduto grupoProduto)
        {
            if (ModelState.IsValid)
            {
                _contexto.Add(grupoProduto);
                await _contexto.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            else
            {
                return View(grupoProduto);
            }
        }

        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if (id != null)
            {
                GrupoProduto grupoProduto = _contexto.GrupoProduto.Find(id);
                return View(grupoProduto);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int? id, GrupoProduto grupoProduto)
        {
            if (id != null)
            {
                if (ModelState.IsValid)
                {
                    _contexto.Update(grupoProduto);
                    await _contexto.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    return View(grupoProduto);
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
                GrupoProduto grupoProduto = _contexto.GrupoProduto.Find(id);
                return View(grupoProduto);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int? id, GrupoProduto grupoProduto)
        {
            if (id != null)
            {
                _contexto.Remove(grupoProduto);
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
