using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Dominio.Servico;
using Dominio.Model;

namespace GestaoJogosUI.Controllers
{
    [Authorize]
    public class AmigosController : Controller
    {

        private readonly IAmigoRepositorio _context;

        public AmigosController(IAmigoRepositorio context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            var lista = await _context.PesquisarTodosAsync();
            return View(lista.Where(x=> x.ID > 1));
        }

        // GET: Amigoes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return View(new Amigo());
            }

            var amigo = await _context.PesquisarporIdAsync((int)id);
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
                 await _context.SalvarAsync(amigo);
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

            var amigo = await _context.PesquisarporIdAsync((int) id);
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
            var amigo = await _context.PesquisarporIdAsync((int)id);
            await _context.DeleteAsync(amigo);
            return RedirectToAction(nameof(Index));
        }
    }
}
