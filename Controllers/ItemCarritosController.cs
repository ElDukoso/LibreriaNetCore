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
    public class ItemCarritosController : Controller
    {
        private readonly LibreriaContext _context;

        public ItemCarritosController(LibreriaContext context)
        {
            _context = context;
        }

        // GET: ItemCarritos
        public async Task<IActionResult> Index()
        {
            var libreriaContext = _context.ItemCarritos.Include(i => i.Carrito).Include(i => i.Libro);
            return View(await libreriaContext.ToListAsync());
        }

        // GET: ItemCarritos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var itemCarrito = await _context.ItemCarritos
                .Include(i => i.Carrito)
                .Include(i => i.Libro)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (itemCarrito == null)
            {
                return NotFound();
            }

            return View(itemCarrito);
        }

        // GET: ItemCarritos/Create
        public IActionResult Create()
        {
            ViewData["CarritoId"] = new SelectList(_context.CarritoDeCompras, "Id", "Id");
            ViewData["LibroId"] = new SelectList(_context.Libros, "Id", "Id");
            return View();
        }

        // POST: ItemCarritos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,CarritoId,LibroId,Cantidad,Precio")] ItemCarrito itemCarrito)
        {
            if (ModelState.IsValid)
            {
                _context.Add(itemCarrito);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CarritoId"] = new SelectList(_context.CarritoDeCompras, "Id", "Id", itemCarrito.CarritoId);
            ViewData["LibroId"] = new SelectList(_context.Libros, "Id", "Id", itemCarrito.LibroId);
            return View(itemCarrito);
        }

        // GET: ItemCarritos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var itemCarrito = await _context.ItemCarritos.FindAsync(id);
            if (itemCarrito == null)
            {
                return NotFound();
            }
            ViewData["CarritoId"] = new SelectList(_context.CarritoDeCompras, "Id", "Id", itemCarrito.CarritoId);
            ViewData["LibroId"] = new SelectList(_context.Libros, "Id", "Id", itemCarrito.LibroId);
            return View(itemCarrito);
        }

        // POST: ItemCarritos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,CarritoId,LibroId,Cantidad,Precio")] ItemCarrito itemCarrito)
        {
            if (id != itemCarrito.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(itemCarrito);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ItemCarritoExists(itemCarrito.Id))
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
            ViewData["CarritoId"] = new SelectList(_context.CarritoDeCompras, "Id", "Id", itemCarrito.CarritoId);
            ViewData["LibroId"] = new SelectList(_context.Libros, "Id", "Id", itemCarrito.LibroId);
            return View(itemCarrito);
        }

        // GET: ItemCarritos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var itemCarrito = await _context.ItemCarritos
                .Include(i => i.Carrito)
                .Include(i => i.Libro)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (itemCarrito == null)
            {
                return NotFound();
            }

            return View(itemCarrito);
        }

        // POST: ItemCarritos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var itemCarrito = await _context.ItemCarritos.FindAsync(id);
            if (itemCarrito != null)
            {
                _context.ItemCarritos.Remove(itemCarrito);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ItemCarritoExists(int id)
        {
            return _context.ItemCarritos.Any(e => e.Id == id);
        }
    }
}
