using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Dominio.Servico;
using Dominio.Model;

namespace GestaoJogosUI.Controllers
{
    [Authorize]
    public class UsuariosController : Controller
    {

        private readonly IUsuarioRepositorio _context;

        public UsuariosController(IUsuarioRepositorio context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            return View( await _context.PesquisarTodosAsync());
        }

        // GET: Amigoes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return View(new Usuario());
            }

            var usuario = await _context.PesquisarporIdAsync((int)id);
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
                 await _context.SalvarAsync(usuario);
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

            var usuario = await _context.PesquisarporIdAsync((int) id);
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
            var usuario = await _context.PesquisarporIdAsync((int)id);
            await _context.DeleteAsync(usuario);
            return RedirectToAction(nameof(Index));
        }
    }
}
