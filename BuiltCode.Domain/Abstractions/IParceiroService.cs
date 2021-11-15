using BuiltCode.Domain.Entities;
using Microsoft.AspNetCore.JsonPatch;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuiltCode.Domain.Abstractions
{
    public interface IParceiroService
    {
        Task<Parceiro> Add(Parceiro parceiro);
        Task Update(Parceiro parceiro);
        Task Delete(Parceiro parceiro);
        IEnumerable<Parceiro> GetParceiros();
        Task<Parceiro> UpdatePatch(Guid id);
        Task<IEnumerable<Medico>> ObterMedicosParceiros(string UfCrm);
        Task<bool> ValidarApiKey(Guid apiKey);
    }
}
