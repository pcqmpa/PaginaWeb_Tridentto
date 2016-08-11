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
}
