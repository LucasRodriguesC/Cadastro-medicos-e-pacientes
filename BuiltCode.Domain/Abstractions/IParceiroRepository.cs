using BuiltCode.Domain.Entities;
using Microsoft.AspNetCore.JsonPatch;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuiltCode.Domain.Abstractions
{
    public interface IParceiroRepository : IRepository<Parceiro>
    {
        Task<Parceiro> UpdatePatch(Guid id);
        Task<IEnumerable<Medico>> ObterMedicosParceiros(string ufCrm);
        Task<bool> ValidarApiKey(Guid apiKey);
    }
}
