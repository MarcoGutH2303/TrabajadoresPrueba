using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace TrabajadoresPrueba.Models
{
    public partial class Trabajadore
    {
        public int Id { get; set; }
        [DisplayName("Tipo Documento")]
        public string? TipoDocumento { get; set; }
        [DisplayName("N° Documento")]
        public string? NumeroDocumento { get; set; }
        [DisplayName("Nombres")]
        public string? Nombres { get; set; }
        [DisplayName("Sexo")]
        public string? Sexo { get; set; }
        public int? IdDepartamento { get; set; }
        public int? IdProvincia { get; set; }
        public int? IdDistrito { get; set; }
        
        public virtual Departamento? IdDepartamentoNavigation { get; set; }
        public virtual Distrito? IdDistritoNavigation { get; set; }
        public virtual Provincium? IdProvinciaNavigation { get; set; }
    }
}
