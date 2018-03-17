using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Authorization;
using Dominio.Servico;
using Dominio.Model;

namespace GestaoJogosUI.Controllers
{
    [Authorize]
    public class JogosController : Controller
    {
        private readonly IJogoRepositorio _context;
        private readonly IAmigoRepositorio _contextAmigo;
        public JogosController(IJogoRepositorio context, IAmigoRepositorio contextAmigo)
        {
            _context = context;
            _contextAmigo = contextAmigo;
        }

        // GET: Jogos
        public async Task<IActionResult> Index()
        {
            var gestaoJogosUIContext = await _context.PesquisarTodoscomIncludAsync();
            return View(gestaoJogosUIContext.ToList());
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                ViewData["AmigoID"] = new SelectList(await _contextAmigo.PesquisarTodosAsync(), "ID", "Nome", 1);
                return View(new Jogo());
            }

            var jogo = await _context.PesquisarporIdAsync((int)id);
            if (jogo == null)
            {
                return NotFound();
            }
            ViewData["AmigoID"] = new SelectList(await _contextAmigo.PesquisarTodosAsync(), "ID", "Nome", jogo.AmigoID);
            return View(jogo);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([Bind("ID,Nome,AmigoID")] Jogo jogo)
        {
            if (ModelState.IsValid)
            {
                    await _context.SalvarAsync(jogo);
                return RedirectToAction(nameof(Index));
            }
            ViewData["AmigoID"] = new SelectList(await _contextAmigo.PesquisarTodosAsync(), "ID", "Nome", jogo.AmigoID);
            return View(jogo);
        }

        // GET: Jogos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var jogo = await _context.PesquisarporIdAsync((int)id);
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
            var jogo = await _context.PesquisarporIdAsync((int)id);
            await _context.DeleteAsync(jogo);
            return RedirectToAction(nameof(Index));
        }
    }
}
