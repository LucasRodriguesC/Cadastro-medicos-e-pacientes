using BuiltCode.Domain.Abstractions;
using BuiltCode.Infrastructure.Context;
using System.Threading.Tasks;

namespace BuiltCode.Infrastructure.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private ParceiroRepository _parceiroRepo;
        private MedicoRepository _medicoRepo;
        private PacienteRepository _pacienteRepo;

        public AppDbContext _db;

        public UnitOfWork(AppDbContext db) { _db = db; }

        public IParceiroRepository ParceiroRepository
        { get { return _parceiroRepo = _parceiroRepo ?? new ParceiroRepository(_db); } }

        public IMedicoRepository MedicoRepository
        { get { return _medicoRepo = _medicoRepo ?? new MedicoRepository(_db); } }

        public IPacienteRepository PacienteRepository
        { get { return _pacienteRepo = _pacienteRepo ?? new PacienteRepository(_db); } }

        public async Task Commit()
        {
            await _db.SaveChangesAsync();
        }

        public void Dispose()
        {
            _db.Dispose();
        }
    }
}
