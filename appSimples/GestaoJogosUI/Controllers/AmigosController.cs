using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GestaoJogosUI.Models;
using Microsoft.AspNetCore.Authorization;

namespace GestaoJogosUI.Controllers
{
    [Authorize]
    public class AmigosController : Controller
    {
        private readonly GestaoJogosUIContext _context;

        public AmigosController(GestaoJogosUIContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            return View(await _context.Amigo.Where(x=> x.ID > 1).ToListAsync());
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
        public async Task<IActionResult> Edit([Bind("ID,Nome")] Amigo amigo)
        {
            if (ModelState.IsValid)
            {
                    if(amigo.ID == null)
                    _context.Add(amigo);
                    else _context.Update(amigo);
                    await _context.SaveChangesAsync();
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
