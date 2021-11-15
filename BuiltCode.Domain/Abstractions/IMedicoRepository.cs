using BuiltCode.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuiltCode.Domain.Abstractions
{
    public interface IMedicoRepository  : IRepository<Medico>
    {
        Task<bool> CrmEmUso(string Crm, string UfCrm);

        Task<bool> MedicoComPacientes(Medico medico);
        Task<bool> CrmEmUso(Medico medico);
    }
}
