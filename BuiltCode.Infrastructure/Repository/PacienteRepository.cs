using BuiltCode.Domain.Abstractions;
using BuiltCode.Domain.Entities;
using BuiltCode.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BuiltCode.Infrastructure.Repository
{
    public class PacienteRepository : Repository<Paciente>, IPacienteRepository
    {
        public PacienteRepository(AppDbContext db) : base(db)
        {

        }

        public async Task<IEnumerable<Paciente>> ObterPacienteMedico()
        {
            return await _db.Pacientes.Include(x => x.Medico).AsNoTracking().ToListAsync();
        }

        public Task<bool> ValidaMedicoCadastrado(Guid id)
        {
            return _db.Medicos.AnyAsync(x => x.Id == id);
        }

        public async Task<bool> ValidarCpfEmUso(string Cpf)
        {
            return await _db.Pacientes.AnyAsync(x => x.Cpf == Cpf);
        }

        public async Task<bool> ValidaCpfEmUso(Paciente paciente)
        {
            return await _db.Pacientes.AnyAsync(x => x.Cpf == paciente.Cpf && x.Id != paciente.Id);
        }


    }
}
