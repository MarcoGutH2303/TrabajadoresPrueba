using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace TrabajadoresPrueba.Models
{
    public partial class Provincium
    {
        public Provincium()
        {
            Distritos = new HashSet<Distrito>();
            Trabajadores = new HashSet<Trabajadore>();
        }

        public int Id { get; set; }
        public int? IdDepartamento { get; set; }
        [DisplayName("Provincia")]
        public string? NombreProvincia { get; set; }

        public virtual Departamento? IdDepartamentoNavigation { get; set; }
        public virtual ICollection<Distrito> Distritos { get; set; }
        public virtual ICollection<Trabajadore> Trabajadores { get; set; }
    }
}
