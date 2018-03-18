using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Authorization;
using Dominio.Servico;
using GestaoJogosUI.Models;
using AutoMapper;
using System.Collections.Generic;
using Dominio.Model;

namespace GestaoJogosUI.Controllers
{
    [Authorize]
    public class JogosController : Controller
    {
        private readonly IJogoRepositorio _context;
        private readonly IAmigoRepositorio _contextAmigo;
        public readonly IMapper _mapper;

        public JogosController(IJogoRepositorio context, IAmigoRepositorio contextAmigo, IMapper mapper)
        {
            _context = context;
            _contextAmigo = contextAmigo;
            _mapper = mapper;
        }

        // GET: Jogos
        public async Task<IActionResult> Index()
        {
            var gestaoJogosUIContext = _mapper.Map<List<JogoViewModel>>(await _context.PesquisarTodoscomIncludAsync());
            return View(gestaoJogosUIContext.ToList());
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                ViewData["AmigoID"] = new SelectList(_mapper.Map<List<AmigoViewModel>>(await _contextAmigo.PesquisarTodosAsync()), "ID", "Nome", 1);
                return View(new JogoViewModel());
            }

            var jogo = _mapper.Map<JogoViewModel>(await _context.PesquisarporIdAsync((int)id));
            if (jogo == null)
            {
                return NotFound();
            }
            ViewData["AmigoID"] = new SelectList(_mapper.Map<List<AmigoViewModel>>(await _contextAmigo.PesquisarTodosAsync()), "ID", "Nome", jogo.AmigoID);
            return View(jogo);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([Bind("ID,Nome,AmigoID")] JogoViewModel jogo)
        {
            if (ModelState.IsValid)
            {
                    await _context.SalvarAsync(_mapper.Map<Jogo>(jogo));
                return RedirectToAction(nameof(Index));
            }
            ViewData["AmigoID"] = new SelectList(_mapper.Map<List<AmigoViewModel>>(await _contextAmigo.PesquisarTodosAsync()), "ID", "Nome", jogo.AmigoID);
            return View(jogo);
        }

        // GET: Jogos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var jogo = _mapper.Map<JogoViewModel>(await _context.PesquisarporIdAsync((int)id));
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
