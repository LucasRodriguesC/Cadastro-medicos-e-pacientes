using BuiltCode.Domain.Abstractions;
using BuiltCode.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuiltCode.Domain.Services
{
    public class PacienteService : IPacienteService
    {
        private readonly IUnitOfWork _unitOfWork;

        public PacienteService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Paciente> Add(Paciente paciente)
        {
            _unitOfWork.PacienteRepository.Add(paciente);
            await _unitOfWork.Commit();
            return paciente;
        }

        public async Task Delete(Paciente paciente)
        {
            _unitOfWork.PacienteRepository.Delete(paciente);
            await _unitOfWork.Commit();
        }

        public async Task<Paciente> GetById(Guid id)
        {
            return await _unitOfWork.PacienteRepository.GetById(id);
        }

        public async Task<IEnumerable<Paciente>> GetPacientes()
        {
            return await _unitOfWork.PacienteRepository.ObterPacienteMedico();
        }

        public async Task<Paciente> Update(Paciente paciente)
        {
            _unitOfWork.PacienteRepository.Update(paciente);
            await _unitOfWork.Commit();
            return paciente;
        }

        public async Task<bool> ValidaMedicoCadastrado(Guid id)
        {
            return await _unitOfWork.PacienteRepository.ValidaMedicoCadastrado(id);
        }

        public async Task<bool> ValidarCpfEmUso(string Cpf)
        {
            return await _unitOfWork.PacienteRepository.ValidarCpfEmUso(Cpf);
        }

        public async Task<bool> ValidarCpfEmUso(Paciente paciente)
        {
            return await _unitOfWork.PacienteRepository.ValidaCpfEmUso(paciente);
        }

    }
}
