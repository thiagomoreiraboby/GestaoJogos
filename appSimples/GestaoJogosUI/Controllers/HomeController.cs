using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using GestaoJogosUI.Models;
using Microsoft.EntityFrameworkCore;

namespace GestaoJogosUI.Controllers
{
    public class HomeController : Controller
    {
        private readonly GestaoJogosUIContext _context;

        public HomeController(GestaoJogosUIContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var gestaoJogosUIContext = _context.Jogo.Include(j => j.Amigo).Where(x=> x.AmigoID > 0);
            return View(await gestaoJogosUIContext.ToListAsync());
        }


        [HttpPost, ActionName("Devolver")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DevolverConfirmed(int? id)
        {
            var jogo = await _context.Jogo.SingleOrDefaultAsync(m => m.ID == id);
            jogo.AmigoID = null;
            jogo.Amigo = null;
            _context.Update(jogo);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
