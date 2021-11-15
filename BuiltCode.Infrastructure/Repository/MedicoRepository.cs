using BuiltCode.Domain.Abstractions;
using BuiltCode.Domain.Entities;
using BuiltCode.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuiltCode.Infrastructure.Repository
{
    public class MedicoRepository : Repository<Medico>, IMedicoRepository
    {
        public MedicoRepository(AppDbContext db) : base(db)
        {
        }

        public Task<bool> CrmEmUso(string Crm, string UfCrm)
        {
            return _db.Medicos.AnyAsync(x => x.Crm.ToLower() == Crm.ToLower() && x.UfCrm.ToLower() == UfCrm.ToLower());
        }

        public Task<bool> CrmEmUso(Medico medico)
        {
            return _db.Medicos.AnyAsync(x => x.Crm.ToLower() == medico.Crm.ToLower() && x.UfCrm.ToLower() == medico.UfCrm.ToLower() && x.Id != medico.Id);
        }

        public async Task<bool> MedicoComPacientes(Medico medico)
        {
            return await _db.Pacientes.AnyAsync(x => x.MedicoId == medico.Id);
        }
    }
}
