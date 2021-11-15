using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuiltCode.Domain.Entities
{
    public class Medico : BaseEntity
    {
        public string Nome { get; set; }
        public string Crm { get; set; }
        public string UfCrm { get; set; }
        public string Especialidade { get; set; }
        public ICollection<Paciente> Pacientes { get; set; }
    }
}
