using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Finanzas_TF.Data;
using Finanzas_TF.Models;
using Microsoft.AspNetCore.Authorization;

namespace Finanzas_TF.Controllers
{
    [Authorize]
    public class ReciboHonorariosController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ReciboHonorariosController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: ReciboHonorarios
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.ReciboHonorarios.Include(r => r.Cliente);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: ReciboHonorarios/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reciboHonorarios = await _context.ReciboHonorarios
                .Include(r => r.Cliente)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (reciboHonorarios == null)
            {
                return NotFound();
            }

            return View(reciboHonorarios);
        }

        // GET: ReciboHonorarios/Create
        public IActionResult Create()
        {
            ViewData["IdCliente"] = new SelectList(_context.Clientes, "Id", "Email");
            return View();
        }

        // POST: ReciboHonorarios/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,MontoInicial,Descripcion,IdCliente,FechaEmision,FechaPago,Moneda")] ReciboHonorarios reciboHonorarios)
        {
            if (ModelState.IsValid)
            {
                reciboHonorarios.Id = Guid.NewGuid();
                if ( (reciboHonorarios.MontoInicial > 1500 && reciboHonorarios.Moneda == 0) || (reciboHonorarios.MontoInicial * 4 > 1500 && reciboHonorarios.Moneda == 1) )
                {
                    reciboHonorarios.Retenido = reciboHonorarios.MontoInicial * (decimal)0.08;
                    reciboHonorarios.Monto = reciboHonorarios.MontoInicial * (decimal)0.92;
                }
                else
                {
                    reciboHonorarios.Retenido = 0;
                    reciboHonorarios.Monto = reciboHonorarios.MontoInicial;
                }
                _context.Add(reciboHonorarios);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdCliente"] = new SelectList(_context.Clientes, "Id", "Email", reciboHonorarios.IdCliente);
            return View(reciboHonorarios);
        }

        // GET: ReciboHonorarios/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reciboHonorarios = await _context.ReciboHonorarios.FindAsync(id);
            if (reciboHonorarios == null)
            {
                return NotFound();
            }
            ViewData["IdCliente"] = new SelectList(_context.Clientes, "Id", "Email", reciboHonorarios.IdCliente);
            return View(reciboHonorarios);
        }

        // POST: ReciboHonorarios/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,MontoInicial,Descripcion,IdCliente,FechaEmision,FechaPago")] ReciboHonorarios reciboHonorarios)
        {
            if (id != reciboHonorarios.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if ( (reciboHonorarios.MontoInicial > 1500 && reciboHonorarios.Moneda == 0) || (reciboHonorarios.MontoInicial * 4 > 1500 && reciboHonorarios.Moneda == 1) )
                    {
                        reciboHonorarios.Retenido = reciboHonorarios.MontoInicial * (decimal)0.08;
                        reciboHonorarios.Monto = reciboHonorarios.MontoInicial * (decimal)0.92;
                    }
                    else
                    {
                        reciboHonorarios.Retenido = 0;
                        reciboHonorarios.Monto = reciboHonorarios.MontoInicial;
                    }
                    _context.Update(reciboHonorarios);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ReciboHonorariosExists(reciboHonorarios.Id))
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
            ViewData["IdCliente"] = new SelectList(_context.Clientes, "Id", "Email", reciboHonorarios.IdCliente);
            return View(reciboHonorarios);
        }

        // GET: ReciboHonorarios/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reciboHonorarios = await _context.ReciboHonorarios
                .Include(r => r.Cliente)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (reciboHonorarios == null)
            {
                return NotFound();
            }

            return View(reciboHonorarios);
        }

        // POST: ReciboHonorarios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var reciboHonorarios = await _context.ReciboHonorarios.FindAsync(id);
            _context.ReciboHonorarios.Remove(reciboHonorarios);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ReciboHonorariosExists(Guid id)
        {
            return _context.ReciboHonorarios.Any(e => e.Id == id);
        }
    }
}
