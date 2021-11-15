using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuiltCode.Domain.Entities
{
    public class Paciente : BaseEntity
    {
        public string Nome { get; set; }
        public string Cpf { get; set; }
        public string Telefone { get; set; }
        public Guid MedicoId { get; set; }
        public Medico Medico { get; set; }
    }
}
