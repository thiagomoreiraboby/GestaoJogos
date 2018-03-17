using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GestaoJogosUI.Models;
using Microsoft.AspNetCore.Authorization;

namespace GestaoJogosUI.Controllers
{
    [Authorize]
    public class JogosController : Controller
    {
        private readonly GestaoJogosUIContext _context;
        public JogosController(GestaoJogosUIContext context)
        {
            _context = context;
        }

        // GET: Jogos
        public async Task<IActionResult> Index()
        {
            return View(await _context.Jogo.Include(j => j.Amigo).ToListAsync());
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                ViewData["AmigoID"] = new SelectList(_context.Amigo, "ID", "Nome", 1);
                return View(new Jogo());
            }

            var jogo = await _context.Jogo.SingleOrDefaultAsync(m => m.ID == id);
            if (jogo == null)
            {
                return NotFound();
            }
            ViewData["AmigoID"] = new SelectList(_context.Amigo, "ID", "Nome", jogo.AmigoID);
            return View(jogo);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([Bind("ID,Nome,AmigoID")] Jogo jogo)
        {
            if (ModelState.IsValid)
            {
                    if(jogo.ID == null)
                    _context.Add(jogo);
                    else _context.Update(jogo);
                    await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AmigoID"] = new SelectList(_context.Amigo, "ID", "Nome", jogo.AmigoID);
            return View(jogo);
        }

        // GET: Jogos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var jogo = await _context.Jogo
                .Include(j => j.Amigo)
                .SingleOrDefaultAsync(m => m.ID == id);
            if (jogo == null)
            {
                return NotFound();
            }

            return View(jogo);
        }

        // POST: Jogos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int? id)
        {
            var jogo = await _context.Jogo.SingleOrDefaultAsync(m => m.ID == id);
            _context.Jogo.Remove(jogo);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool JogoExists(int? id)
        {
            return _context.Jogo.Any(e => e.ID == id);
        }
    }
}
