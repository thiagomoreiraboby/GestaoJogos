using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using GestaoJogosUI.Models;
using Microsoft.AspNetCore.Authorization;
using Dominio.Servico;
using Microsoft.AspNetCore.Http;

namespace GestaoJogosUI.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly IJogoRepositorio _context;
        private static IHttpContextAccessor _contextAccessor;
        private static HttpContext _contexthttp { get { return _contextAccessor.HttpContext; } }

        public HomeController(IJogoRepositorio context, IHttpContextAccessor contextAccessor)
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
            var gestaoJogosUIContext = await _context.JogosEmprestadosAsync();
            ViewData["jogoscomvoce"] = await _context.JogosComigoAsync();
            return View(gestaoJogosUIContext.ToList());
        }

        public async Task<IActionResult> Devolver(int? id)
        {
            var jogo = await _context.PesquisarporIdAsync((int)id);
            jogo.AmigoID = 1;
            jogo.Amigo = null;
            await _context.SalvarAsync(jogo);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
