using BuiltCode.Domain.Abstractions;
using BuiltCode.Domain.Entities;
using BuiltCode.Infrastructure.Context;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuiltCode.Infrastructure.Repository
{
    public class ParceiroRepository : Repository<Parceiro>, IParceiroRepository
    {
        public ParceiroRepository(AppDbContext db) : base(db) { }

        public async Task<IEnumerable<Medico>> ObterMedicosParceiros(string ufCrm)
        {
            var medicos = _db.Medicos.AsQueryable();
            if(!string.IsNullOrEmpty(ufCrm))
            {
                medicos = medicos.Where(x => x.UfCrm.ToLower() == ufCrm.ToLower());
            }

            return await medicos.ToListAsync();
        }

        public async Task<Parceiro> UpdatePatch(Guid id)
        {
            var response = await  _db.Parceiros.SingleOrDefaultAsync(x => x.Id == id);
            
            if(response != null)
            {
                response.ApiKey = Guid.NewGuid();
                await _db.SaveChangesAsync();
            }

            return response;
        }

        public async Task<bool> ValidarApiKey(Guid apiKey)
        {
            return await _db.Parceiros.AnyAsync(x => x.ApiKey == apiKey);
        }
    }
}
