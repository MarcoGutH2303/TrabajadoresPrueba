using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TrabajadoresPrueba.Helpers;
using TrabajadoresPrueba.Models;

namespace TrabajadoresPrueba.Controllers
{
    public class BandejaController : Controller
    {
        private readonly TrabajadoresPruebaContext _context;

        public BandejaController(TrabajadoresPruebaContext context)
        {
            _context = context;
        }

        public IActionResult Bandeja()
        {
            return View();
        }

        public async Task<IActionResult> _BandejaTrabajador()
        {
            var listTrabajadorDTO = _context.TrabajadoresDTO
                .FromSqlInterpolated($"EXEC spListarTrabajadores")
                .AsAsyncEnumerable();

            var listTrabajador = await new Conversiones().ConvertirTrabajadorDTOATrabajador(listTrabajadorDTO);

            return PartialView(listTrabajador);
        }

        public async Task<IActionResult> _FiltroSexoTrabajador(string sexo)
        {
            if (sexo is null)
            {
                var listTrabajadores = await _context.Trabajadores
                            .Include(x => x.IdDepartamentoNavigation)
                            .Include(x => x.IdDistritoNavigation)
                            .Include(x => x.IdProvinciaNavigation)
                            .ToListAsync();
                return PartialView("_BandejaTrabajador", listTrabajadores);
            }
            else
            {
                var listTrabajadores = await _context.Trabajadores
                            .Include(x => x.IdDepartamentoNavigation)
                            .Include(x => x.IdDistritoNavigation)
                            .Include(x => x.IdProvinciaNavigation)
                            .ToListAsync();
                listTrabajadores = listTrabajadores.Where(x => x.Sexo == sexo).ToList();
                return PartialView("_BandejaTrabajador", listTrabajadores);
            }
        }
    }
}
