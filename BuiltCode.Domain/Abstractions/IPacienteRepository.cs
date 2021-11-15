using BuiltCode.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuiltCode.Domain.Abstractions
{
    public interface IPacienteRepository : IRepository<Paciente>
    {
        Task<bool> ValidarCpfEmUso(string Cpf);
        Task<bool> ValidaMedicoCadastrado(Guid id);
        Task<IEnumerable<Paciente>> ObterPacienteMedico();
        Task<bool> ValidaCpfEmUso(Paciente paciente);
    }
}
