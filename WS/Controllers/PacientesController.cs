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
    public class PacientesController : Controller
    {
        private readonly AppDbContext _context;

        public PacientesController(AppDbContext context)
        {
            _context = context;
        }
        
        // GET: Pacientes
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.Paciente.Include(p => p.Exame)
                                                .OrderByDescending(p => p.Data);
            return View(await appDbContext.ToListAsync());
        }

        // GET: Pacientes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Paciente == null)
            {
                return NotFound();
            }

            var paciente = await _context.Paciente
                .Include(p => p.Exame)
                .FirstOrDefaultAsync(m => m.PacienteId == id);
            if (paciente == null)
            {
                return NotFound();
            }

            return View(paciente);
        }

        // GET: Pacientes/Create
        public IActionResult Create()
        {
            ViewData["ExameId"] = new SelectList(_context.Exames, "ExameId", "NomeExame");
            return View();
        }

        // POST: Pacientes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PacienteId,Nome,ExameId,Data,DataPronto")] Paciente paciente)
        {
            //if (ModelState.IsValid)
            //{
            //    // Recupera o exame selecionado
            //    var exame = await _context.Exames.SingleOrDefaultAsync(e => e.Id == paciente.ExameId);
            //    if (exame == null)
            //    {
            //        ModelState.AddModelError("", "Exame não encontrado.");
            //        ViewData["ExameId"] = new SelectList(_context.Exames, "Id", "Nome", paciente.ExameId);
            //        return View(paciente);
            //    }

            //    // Calcula a data de pronto
            //    var dataPronto = paciente.Data.AddDays(exame.DuracaoDias);
            //    paciente.DataPronto = dataPronto;

            //    _context.Add(paciente);
            //    await _context.SaveChangesAsync();
            //    return RedirectToAction(nameof(Index));
            //}

            //ViewData["ExameId"] = new SelectList(_context.Exames, "Id", "Nome", paciente.ExameId);
            //return View(paciente);
            if (ModelState.IsValid)
            {
                var exame = await _context.Exames.SingleOrDefaultAsync(e => e.ExameId == paciente.ExameId);
                if (exame == null)
                {
                    ModelState.AddModelError("", "Exame não encontrado!");
                    ViewData["ExameId"] = new SelectList(_context.Exames, "ExameId", "NomeExame", paciente.ExameId);
                    return View(paciente);
                }
                paciente.DataPronto = paciente.Data.AddDays(exame.Dias);
                _context.Add(paciente);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ExameId"] = new SelectList(_context.Exames, "ExameId", "NomeExame", paciente.ExameId);
            return View(paciente);
        }

        // GET: Pacientes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Paciente == null)
            {
                return NotFound();
            }

            var paciente = await _context.Paciente.FindAsync(id);
            if (paciente == null)
            {
                return NotFound();
            }
            ViewData["ExameId"] = new SelectList(_context.Exames, "ExameId", "NomeExame", paciente.ExameId);
            return View(paciente);
        }

        // POST: Pacientes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PacienteId,Nome,ExameId,Data,DataPronto")] Paciente paciente)
        {
            if (id != paciente.PacienteId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(paciente);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PacienteExists(paciente.PacienteId))
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
            ViewData["ExameId"] = new SelectList(_context.Exames, "ExameId", "NomeExame", paciente.ExameId);
            return View(paciente);
        }

        // GET: Pacientes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Paciente == null)
            {
                return NotFound();
            }

            var paciente = await _context.Paciente
                .Include(p => p.Exame)
                .FirstOrDefaultAsync(m => m.PacienteId == id);
            if (paciente == null)
            {
                return NotFound();
            }

            return View(paciente);
        }

        // POST: Pacientes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Paciente == null)
            {
                return Problem("Entity set 'AppDbContext.Paciente'  is null.");
            }
            var paciente = await _context.Paciente.FindAsync(id);
            if (paciente != null)
            {
                _context.Paciente.Remove(paciente);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PacienteExists(int id)
        {
          return _context.Paciente.Any(e => e.PacienteId == id);
        }
    }
}

