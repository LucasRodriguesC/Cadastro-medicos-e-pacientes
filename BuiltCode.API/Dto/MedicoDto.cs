using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BuiltCode.API.Dto
{
    public class MedicoDto
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public string Crm { get; set; }
        public string UfCrm { get; set; }
        public string Especialidade { get; set; }
    }
}
