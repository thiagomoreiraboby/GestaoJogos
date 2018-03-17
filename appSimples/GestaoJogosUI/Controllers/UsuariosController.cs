using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using GestaoJogosUI.Models;
using Microsoft.EntityFrameworkCore;

namespace GestaoJogosUI.Controllers
{
    [Authorize]
    public class UsuariosController : Controller
    {

        private readonly GestaoJogosUIContext _context;

        public UsuariosController(GestaoJogosUIContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            return View(await _context.Usuario.ToListAsync());
        }

        // GET: Amigoes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return View(new Usuario());
            }

            var usuario = await _context.Usuario.SingleOrDefaultAsync(m => m.ID == id);
            if (usuario == null)
            {
                return NotFound();
            }
            return View(usuario);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([Bind("ID,Nome,Senha")] Usuario usuario)
        {
            if (ModelState.IsValid)
            {
                if(string.IsNullOrEmpty(usuario.Senha) || string.IsNullOrEmpty(usuario.Nome))
                    return View(usuario);          
                 if(usuario.ID == null)
                    _context.Add(usuario);
                    else _context.Update(usuario);
                    await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(usuario);
        }

        // GET: Amigoes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var usuario = await _context.Usuario
                .SingleOrDefaultAsync(m => m.ID == id);
            if (usuario == null)
            {
                return NotFound();
            }

            return View(usuario);
        }

        // POST: Amigoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int? id)
        {
            var usuario = await _context.Amigo.SingleOrDefaultAsync(m => m.ID == id);
                        _context.Amigo.Remove(usuario);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
