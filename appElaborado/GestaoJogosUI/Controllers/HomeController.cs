﻿using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using GestaoJogosUI.Models;
using Microsoft.AspNetCore.Authorization;
using Dominio.Servico;
using Microsoft.AspNetCore.Http;
using AutoMapper;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;
using Dominio.Model;

namespace GestaoJogosUI.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IJogoRepositorio _context;
        private static IHttpContextAccessor _contextAccessor;
        public readonly IAmigoRepositorio _contextAmigo;
        private static HttpContext _contexthttp { get { return _contextAccessor.HttpContext; } }

        public HomeController(IJogoRepositorio context, IAmigoRepositorio contextAmigo, IHttpContextAccessor contextAccessor, IMapper mapper)
        {
            _context = context;
            _contextAmigo = contextAmigo;
            _contextAccessor = contextAccessor;
            _mapper = mapper;
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
            var gestaoJogosUIContext = _mapper.Map<List<JogoViewModel>>(await _context.JogosEmprestadosAsync());
            ViewData["jogoscomvoce"] = _mapper.Map<List<JogoViewModel>>(await _context.JogosComigoAsync());
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


        public async Task<IActionResult> Emprestar(int? id)
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
            ViewData["AmigoID"] = new SelectList(_mapper.Map<List<AmigoViewModel>>(await _contextAmigo.PesquisarTodosAsync()), "ID", "Nome", jogo.AmigoID);
            return View(jogo);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Emprestar([Bind("ID,Nome,AmigoID")] JogoViewModel jogo)
        {
            if (ModelState.IsValid)
            {
                await _context.SalvarAsync(_mapper.Map<Jogo>(jogo));
                return RedirectToAction(nameof(Index));
            }
            ViewData["AmigoID"] = new SelectList(_mapper.Map<List<AmigoViewModel>>(await _contextAmigo.PesquisarTodosAsync()), "ID", "Nome", jogo.AmigoID);
            return View(jogo);
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
