using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GestaoJogosUI.Models;

namespace GestaoJogosUI.Controllers
{
    public class AmigosController : Controller
    {
        private readonly GestaoJogosUIContext _context;

        public AmigosController(GestaoJogosUIContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            return View(await _context.Amigo.ToListAsync());
        }

        // GET: Amigoes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return View(new Amigo());
            }

            var amigo = await _context.Amigo.SingleOrDefaultAsync(m => m.ID == id);
            if (amigo == null)
            {
                return NotFound();
            }
            return View(amigo);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, [Bind("ID,Nome")] Amigo amigo)
        {
            if (id != amigo.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if(amigo.ID == null)
                    _context.Add(amigo);
                    else _context.Update(amigo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AmigoExists(amigo.ID))
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
            return View(amigo);
        }

        // GET: Amigoes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var amigo = await _context.Amigo
                .SingleOrDefaultAsync(m => m.ID == id);
            if (amigo == null)
            {
                return NotFound();
            }

            return View(amigo);
        }

        // POST: Amigoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int? id)
        {
            var amigo = await _context.Amigo.SingleOrDefaultAsync(m => m.ID == id);
            _context.Amigo.Remove(amigo);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AmigoExists(int? id)
        {
            return _context.Amigo.Any(e => e.ID == id);
        }
    }
}
