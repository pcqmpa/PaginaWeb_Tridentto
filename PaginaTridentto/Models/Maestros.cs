using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaginaTridentto.Models
{
    public class Maestros
    {
    }

    public class Paises
    {
        public int Id { get; set; }
        public string StrNombre { get; set; }
    }
    public class Departamentos
    {
        public int Id { get; set; }
        public string  StrNombre { get; set; }
    }

    public class Ciudades
    {
        public int Id { get; set; }
        public int IdDepartamento { get; set; }
        public string StrNombre { get; set; }

    }

    public class DatosComplemenatriosUsuario
    {
        public Int64 IdUsuario { get; set; }
        public int IdPais { get; set; }

        public int IdDepartamento { get; set; }
        public int IdCiudad { get; set; }
        public string StrTelefono { get; set; }
        public string StrMobil { get; set; }

    }
}
