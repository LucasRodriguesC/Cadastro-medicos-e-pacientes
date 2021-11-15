using BuiltCode.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BuiltCode.Domain.Abstractions
{
    public interface IPacienteService
    {
        Task<Paciente> Add(Paciente paciente);
        Task<Paciente> Update(Paciente paciente);
        Task Delete(Paciente paciente);
        Task<IEnumerable<Paciente>> GetPacientes();
        Task<Paciente> GetById(Guid id);
        Task<bool> ValidarCpfEmUso(string Cpf);
        Task<bool> ValidarCpfEmUso(Paciente paciente);
        Task<bool> ValidaMedicoCadastrado(Guid id);
    }
}
