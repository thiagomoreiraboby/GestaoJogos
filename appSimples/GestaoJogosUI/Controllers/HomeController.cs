using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using GestaoJogosUI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;

namespace GestaoJogosUI.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly GestaoJogosUIContext _context;
        private static IHttpContextAccessor _contextAccessor;
        private static HttpContext _contexthttp { get { return _contextAccessor.HttpContext; } }

        public HomeController(GestaoJogosUIContext context, IHttpContextAccessor contextAccessor)
        {
            _context = context;
            _contextAccessor = contextAccessor;
        }

        public static string UserName
        {
            get
            {
                var userName = "";
                try
                {

                if (_contexthttp != null)
                {
                    if (_contexthttp.User != null)
                    {
                        var identity = _contexthttp.User.Identity;

                        if (identity != null && identity.IsAuthenticated)
                        {
                            userName = identity.Name;
                        }
                    }
                }

                }
                catch
                {


                }
                return userName;
            }
        }

        public async Task<IActionResult> Index()
        {
            var gestaoJogosUIContext = _context.Jogo.Include(j => j.Amigo).Where(x=> x.AmigoID > 1);
            ViewData["jogoscomvoce"] = _context.Jogo.Include(j => j.Amigo).Where(x => x.AmigoID == 1);
            return View(await gestaoJogosUIContext.ToListAsync());
            
        }

        public async Task<IActionResult> Devolver(int? id)
        {
            var jogo = await _context.Jogo.SingleOrDefaultAsync(m => m.ID == id);
            jogo.AmigoID = 1;
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
