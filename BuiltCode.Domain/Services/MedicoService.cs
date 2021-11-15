using BuiltCode.Domain.Abstractions;
using BuiltCode.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuiltCode.Domain.Services
{
    public class MedicoService : IMedicoService
    {
        private readonly IUnitOfWork _unitOfWork;

        public MedicoService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Medico> Add(Medico medico)
        {
            _unitOfWork.MedicoRepository.Add(medico);
            await _unitOfWork.Commit();
            return medico;
        }

        public async Task Delete(Medico medico)
        {
            _unitOfWork.MedicoRepository.Delete(medico);
            await _unitOfWork.Commit();
        }

        public IEnumerable<Medico> GetMedicos()
        {
            return _unitOfWork.MedicoRepository.Get();
        }

        public async Task Update(Medico medico)
        {
            _unitOfWork.MedicoRepository.Update(medico);
            await _unitOfWork.Commit();
        }

        public async Task<Medico> GetById(Guid id)
        {
            return await _unitOfWork.MedicoRepository.GetById(id);
        }

        public async Task<bool> CrmEmUso(string Crm, string UfCrm)
        {
            return await _unitOfWork.MedicoRepository.CrmEmUso(Crm, UfCrm);
        }

        public async Task<bool> CrmEmUsoUpdate(Medico medico)
        {
            return await _unitOfWork.MedicoRepository.CrmEmUso(medico);
        }

        public async Task<bool> MedicoComPacientes(Medico medico)
        {
            return await _unitOfWork.MedicoRepository.MedicoComPacientes(medico);
        }
    }
}
