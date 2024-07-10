using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Libreria.Data;
using Libreria.Models;

namespace Libreria.Controllers
{
    public class CarritoDeComprasController : Controller
    {
        private readonly LibreriaContext _context;

        public CarritoDeComprasController(LibreriaContext context)
        {
            _context = context;
        }

        // GET: CarritoDeCompras
        public async Task<IActionResult> Index()
        {
            var libreriaContext = _context.CarritoDeCompras.Include(c => c.Usuario);
            return View(await libreriaContext.ToListAsync());
        }

        // GET: CarritoDeCompras/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var carritoDeCompra = await _context.CarritoDeCompras
                .Include(c => c.Usuario)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (carritoDeCompra == null)
            {
                return NotFound();
            }

            return View(carritoDeCompra);
        }

        // GET: CarritoDeCompras/Create
        public IActionResult Create()
        {
            ViewData["UsuarioId"] = new SelectList(_context.Usuarios, "Id", "Id");
            return View();
        }

        // POST: CarritoDeCompras/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,UsuarioId,Total")] CarritoDeCompra carritoDeCompra)
        {
            if (ModelState.IsValid)
            {
                _context.Add(carritoDeCompra);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["UsuarioId"] = new SelectList(_context.Usuarios, "Id", "Id", carritoDeCompra.UsuarioId);
            return View(carritoDeCompra);
        }

        // GET: CarritoDeCompras/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var carritoDeCompra = await _context.CarritoDeCompras.FindAsync(id);
            if (carritoDeCompra == null)
            {
                return NotFound();
            }
            ViewData["UsuarioId"] = new SelectList(_context.Usuarios, "Id", "Id", carritoDeCompra.UsuarioId);
            return View(carritoDeCompra);
        }

        // POST: CarritoDeCompras/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,UsuarioId,Total")] CarritoDeCompra carritoDeCompra)
        {
            if (id != carritoDeCompra.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(carritoDeCompra);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CarritoDeCompraExists(carritoDeCompra.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["UsuarioId"] = new SelectList(_context.Usuarios, "Id", "Id", carritoDeCompra.UsuarioId);
            return View(carritoDeCompra);
        }

        // GET: CarritoDeCompras/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var carritoDeCompra = await _context.CarritoDeCompras
                .Include(c => c.Usuario)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (carritoDeCompra == null)
            {
                return NotFound();
            }

            return View(carritoDeCompra);
        }

        // POST: CarritoDeCompras/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var carritoDeCompra = await _context.CarritoDeCompras.FindAsync(id);
            if (carritoDeCompra != null)
            {
                _context.CarritoDeCompras.Remove(carritoDeCompra);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CarritoDeCompraExists(int id)
        {
            return _context.CarritoDeCompras.Any(e => e.Id == id);
        }
    }
}
