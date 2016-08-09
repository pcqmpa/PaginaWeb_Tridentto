using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaginaTridentto.Models
{
    public class Seguridad
    {
    }

    public class Usuario
    {
        public Int64 Id { get; set; }
        public string StrUsuario { get; set; }
        public string StrPassword { get; set; }
        public string StrNombre { get; set; }

        public int IdGrupo { get; set; }

        public string StrNombreGrupo { get; set; }

        public string StrEmail { get; set; }

        public bool LogActivo { get; set; }

        public DateTime DtmFechaCreacion { get; set; }

    }
}
