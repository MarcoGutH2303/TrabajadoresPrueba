using TrabajadoresPrueba.Models;
using TrabajadoresPrueba.Models.DTOs;

namespace TrabajadoresPrueba.Helpers
{
    public class Conversiones
    {
        public async Task<List<Trabajadore>> ConvertirTrabajadorDTOATrabajador(IAsyncEnumerable<TrabajadorDTO> listTrabajadorDTO)
        {
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
            return listTrabajador;
        }
    }
}
