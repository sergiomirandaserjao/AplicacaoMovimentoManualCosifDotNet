using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using BLL.Services;
using DAL.Implementations;
using Domain.Entities;

namespace UI.AspNetCoreMvc.Controllers
{
    public class MovimentoController : Controller
    {
        private readonly MovimentoService _service;
        public MovimentoController()
        {
            _service = new MovimentoService(new MovimentoRepositoryAdo());
        }

        public async Task<IActionResult> Index(int? ano, int? mes)
        {
            var lista = await _service.Listar(ano, mes);
            return View(lista);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] MovimentoManual model)
        {
            await _service.InserirMovimentoAsync(model);
            return RedirectToAction("Index");
        }
    }
}
