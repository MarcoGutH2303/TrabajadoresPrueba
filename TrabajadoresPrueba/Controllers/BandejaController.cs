using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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

        public async Task<IActionResult> Bandeja()
        {
            return View();
        }

        public async Task<IActionResult> _BandejaTrabajador()
        {
            var listTrabajadorDTO = _context.TrabajadoresDTO
                .FromSqlInterpolated($"EXEC spListarTrabajadores")
                .AsAsyncEnumerable();

            var listTrabajador = new List<Trabajadore>();

            await foreach (var trabajador in listTrabajadorDTO)
            {
                var oTrabajador = new Trabajadore();
                oTrabajador.Id = trabajador.Id;
                oTrabajador.Nombres = trabajador.Nombres;
                oTrabajador.TipoDocumento = trabajador.TipoDocumento;
                oTrabajador.NumeroDocumento = trabajador.NumeroDocumento;
                oTrabajador.Sexo = trabajador.Sexo;
                oTrabajador.IdDepartamentoNavigation = new Departamento();
                oTrabajador.IdDepartamentoNavigation.NombreDepartamento = trabajador.NombreDepartamento;
                oTrabajador.IdProvinciaNavigation = new Provincium();
                oTrabajador.IdProvinciaNavigation.NombreProvincia = trabajador.NombreProvincia;
                oTrabajador.IdDistritoNavigation = new Distrito();
                oTrabajador.IdDistritoNavigation.NombreDistrito = trabajador.NombreDistrito;

                listTrabajador.Add(oTrabajador);
            }

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
