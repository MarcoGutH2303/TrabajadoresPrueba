using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TrabajadoresPrueba.Models;

namespace TrabajadoresPrueba.Controllers
{
    public class TrabajadorController : Controller
    {
        private readonly TrabajadoresPruebaContext _context;

        public TrabajadorController(TrabajadoresPruebaContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Trabajador()
        {
            return View();
        }

        public async Task<IActionResult> _ListarTrabajador()
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
                return PartialView("_ListarTrabajador", listTrabajadores);
            }
            else
            {
                var listTrabajadores = await _context.Trabajadores
                            .Include(x => x.IdDepartamentoNavigation)
                            .Include(x => x.IdDistritoNavigation)
                            .Include(x => x.IdProvinciaNavigation)
                            .ToListAsync();
                listTrabajadores = listTrabajadores.Where(x => x.Sexo == sexo).ToList();
                return PartialView("_ListarTrabajador", listTrabajadores);
            }
        }

        [HttpPost]
        public async Task<int> GuardarTrabajador(Trabajadore oTrabajadore)
        {
            int val = 0;
            if (oTrabajadore.Id == 0)
                _context.Add(oTrabajadore);
            else
                _context.Update(oTrabajadore);
            val = await _context.SaveChangesAsync();
            return val;
        }

        [HttpGet]
        public async Task<JsonResult> RecuperarTrabajador(int idTrabajador)
        {
            var oTrabajador = await _context.Trabajadores.FirstOrDefaultAsync(tr => tr.Id == idTrabajador);
            return Json(oTrabajador);
        }

        [HttpPost]
        public async Task<int> EliminarTrabajador(int idTrabajador)
        {
            var oTrabajador = await _context.Trabajadores.FirstOrDefaultAsync(tr => tr.Id == idTrabajador);
            if (oTrabajador == null)
            {
                return 0;
            }

            _context.Remove(oTrabajador);
            return await _context.SaveChangesAsync();
        }

        [HttpGet]
        public async Task<JsonResult> CargarDepartamento()
        {
            var listDepartamento = await _context.Departamentos.ToListAsync();
            return Json(listDepartamento);
        }

        [HttpGet]
        public async Task<JsonResult> CargarProvincia(int idDepartamento)
        {
            var listProvincia = await _context.Provincia.Where(x => x.IdDepartamento == idDepartamento).ToListAsync();
            return Json(listProvincia);
        }

        [HttpGet]
        public async Task<JsonResult> CargarDistrito(int idProvincia)
        {
            var listDistrito = await _context.Distritos.Where(x => x.IdProvincia == idProvincia).ToListAsync();
            return Json(listDistrito);
        }
    }
}
