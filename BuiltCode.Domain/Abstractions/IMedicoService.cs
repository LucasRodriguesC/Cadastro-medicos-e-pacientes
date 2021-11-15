using BuiltCode.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BuiltCode.Domain.Abstractions
{
    public interface IMedicoService
    {
        Task<Medico> Add(Medico medico);
        Task Update(Medico medico);
        Task Delete(Medico medico);
        IEnumerable<Medico> GetMedicos();
        Task<bool> CrmEmUso(string Crm, string UfCrm);
        Task<Medico> GetById(Guid id);
        Task<bool> MedicoComPacientes(Medico medico);
        Task<bool> CrmEmUsoUpdate(Medico medico);

    }
}
