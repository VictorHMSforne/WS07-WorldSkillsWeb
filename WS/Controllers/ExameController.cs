using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WS.Context;
using WS.Models;

namespace WS.Controllers
{
    public class ExameController : Controller
    {
        private readonly AppDbContext _context;

        public ExameController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Exame
        public async Task<IActionResult> Index()
        {
              return View(await _context.Exames.ToListAsync());
        }

        // GET: Exame/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Exames == null)
            {
                return NotFound();
            }

            var exame = await _context.Exames
                .FirstOrDefaultAsync(m => m.ExameId == id);
            if (exame == null)
            {
                return NotFound();
            }

            return View(exame);
        }

        // GET: Exame/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Exame/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ExameId,NomeExame,Dias")] Exame exame)
        {
            if (ModelState.IsValid)
            {
                _context.Add(exame);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(exame);
        }

        // GET: Exame/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Exames == null)
            {
                return NotFound();
            }

            var exame = await _context.Exames.FindAsync(id);
            if (exame == null)
            {
                return NotFound();
            }
            return View(exame);
        }

        // POST: Exame/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ExameId,NomeExame,Dias")] Exame exame)
        {
            if (id != exame.ExameId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(exame);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ExameExists(exame.ExameId))
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
            return View(exame);
        }

        // GET: Exame/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Exames == null)
            {
                return NotFound();
            }

            var exame = await _context.Exames
                .FirstOrDefaultAsync(m => m.ExameId == id);
            if (exame == null)
            {
                return NotFound();
            }

            return View(exame);
        }

        // POST: Exame/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Exames == null)
            {
                return Problem("Entity set 'AppDbContext.Exames'  is null.");
            }
            var exame = await _context.Exames.FindAsync(id);
            if (exame != null)
            {
                _context.Exames.Remove(exame);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ExameExists(int id)
        {
          return _context.Exames.Any(e => e.ExameId == id);
        }
    }
}
