using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using programfin.Models;

namespace programfin.Controllers
{
    public class NoticiumsController : Controller
    {
        private readonly PeriodicoContext _context;

        public NoticiumsController(PeriodicoContext context)
        {
            _context = context;
        }

        // GET: Noticiums
        public async Task<IActionResult> Index()
        {
            var periodicoContext = _context.Noticia.Include(n => n.IdCategoriaNavigation).Include(n => n.IdUsuarioNavigation);
            return View(await periodicoContext.ToListAsync());
        }

        // GET: Noticiums/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Noticia == null)
            {
                return NotFound();
            }

            var noticium = await _context.Noticia
                .Include(n => n.IdCategoriaNavigation)
                .Include(n => n.IdUsuarioNavigation)
                .FirstOrDefaultAsync(m => m.IdNoticia == id);
            if (noticium == null)
            {
                return NotFound();
            }

            return View(noticium);
        }

        // GET: Noticiums/Create
        public IActionResult Create()
        {
            ViewData["IdCategoria"] = new SelectList(_context.Categoria, "IdCategoria", "IdCategoria");
            ViewData["IdUsuario"] = new SelectList(_context.Usuarios, "IdUsuario", "IdUsuario");
            return View();
        }

        // POST: Noticiums/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdNoticia,Nombre,Descripcion,FotoPortada,Cuerpo,FechaRegistro,IdUsuario,IdCategoria")] Noticium noticium)
        {
            if (ModelState.IsValid)
            {
                _context.Add(noticium);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdCategoria"] = new SelectList(_context.Categoria, "IdCategoria", "IdCategoria", noticium.IdCategoria);
            ViewData["IdUsuario"] = new SelectList(_context.Usuarios, "IdUsuario", "IdUsuario", noticium.IdUsuario);
            return View(noticium);
        }

        // GET: Noticiums/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Noticia == null)
            {
                return NotFound();
            }

            var noticium = await _context.Noticia.FindAsync(id);
            if (noticium == null)
            {
                return NotFound();
            }
            ViewData["IdCategoria"] = new SelectList(_context.Categoria, "IdCategoria", "IdCategoria", noticium.IdCategoria);
            ViewData["IdUsuario"] = new SelectList(_context.Usuarios, "IdUsuario", "IdUsuario", noticium.IdUsuario);
            return View(noticium);
        }

        // POST: Noticiums/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdNoticia,Nombre,Descripcion,FotoPortada,Cuerpo,FechaRegistro,IdUsuario,IdCategoria")] Noticium noticium)
        {
            if (id != noticium.IdNoticia)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(noticium);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!NoticiumExists(noticium.IdNoticia))
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
            ViewData["IdCategoria"] = new SelectList(_context.Categoria, "IdCategoria", "IdCategoria", noticium.IdCategoria);
            ViewData["IdUsuario"] = new SelectList(_context.Usuarios, "IdUsuario", "IdUsuario", noticium.IdUsuario);
            return View(noticium);
        }

        // GET: Noticiums/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Noticia == null)
            {
                return NotFound();
            }

            var noticium = await _context.Noticia
                .Include(n => n.IdCategoriaNavigation)
                .Include(n => n.IdUsuarioNavigation)
                .FirstOrDefaultAsync(m => m.IdNoticia == id);
            if (noticium == null)
            {
                return NotFound();
            }

            return View(noticium);
        }

        // POST: Noticiums/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Noticia == null)
            {
                return Problem("Entity set 'PeriodicoContext.Noticia'  is null.");
            }
            var noticium = await _context.Noticia.FindAsync(id);
            if (noticium != null)
            {
                _context.Noticia.Remove(noticium);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool NoticiumExists(int id)
        {
          return (_context.Noticia?.Any(e => e.IdNoticia == id)).GetValueOrDefault();
        }
    }
}
