using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuiltCode.Domain.Abstractions
{
    public interface IUnitOfWork
    {
        IParceiroRepository ParceiroRepository { get; }
        IMedicoRepository MedicoRepository { get; }
        IPacienteRepository PacienteRepository { get; }
        Task Commit();
    }
}
