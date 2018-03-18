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
    public class UsuariosController : Controller
    {

        private readonly IUsuarioRepositorio _context;
        public readonly IMapper _mapper;

        public UsuariosController(IUsuarioRepositorio context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<IActionResult> Index()
        {
            return View(_mapper.Map<List<UsuarioViewModel>>(await _context.PesquisarTodosAsync()));
        }

        // GET: Amigoes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return View(new UsuarioViewModel());
            }

            var usuario = _mapper.Map<UsuarioViewModel>(await _context.PesquisarporIdAsync((int)id));
            if (usuario == null)
            {
                return NotFound();
            }
            return View(usuario);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([Bind("ID,Nome,Senha")] UsuarioViewModel usuario)
        {
            if (ModelState.IsValid)
            {
                await _context.SalvarAsync(_mapper.Map<Usuario>(usuario));
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

            var usuario = _mapper.Map<UsuarioViewModel>(await _context.PesquisarporIdAsync((int) id));
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
