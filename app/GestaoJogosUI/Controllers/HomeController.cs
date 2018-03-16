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

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
