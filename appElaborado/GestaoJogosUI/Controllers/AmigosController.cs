using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Dominio.Servico;
using GestaoJogosUI.Models;
using AutoMapper;
using System.Collections.Generic;
using Dominio.Model;

namespace GestaoJogosUI.Controllers
{
    [Authorize]
    public class AmigosController : Controller
    {

        private readonly IAmigoRepositorio _context;
        private readonly IMapper _mapper;

        public AmigosController(IAmigoRepositorio context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<IActionResult> Index()
        {
            var lista = _mapper.Map<List<AmigoViewModel>>(await _context.PesquisarTodosAsync());
            return View(lista.Where(x=> x.ID > 1));
        }

        // GET: Amigoes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return View(new AmigoViewModel());
            }

            var amigo = _mapper.Map<AmigoViewModel>(await _context.PesquisarporIdAsync((int)id));
            if (amigo == null)
            {
                return NotFound();
            }
            return View(amigo);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([Bind("ID,Nome")] AmigoViewModel amigo)
        {
            if (ModelState.IsValid)
            {
                 await _context.SalvarAsync(_mapper.Map<Amigo>(amigo));
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

            var amigo = _mapper.Map<AmigoViewModel>(await _context.PesquisarporIdAsync((int) id));
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
