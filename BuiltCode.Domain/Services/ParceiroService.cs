using BuiltCode.Domain.Abstractions;
using BuiltCode.Domain.Entities;
using Microsoft.AspNetCore.JsonPatch;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BuiltCode.Domain.Services
{
    public class ParceiroService : IParceiroService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ParceiroService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Parceiro> Add(Parceiro parceiro)
        {
            _unitOfWork.ParceiroRepository.Add(parceiro);
            await _unitOfWork.Commit();
            return parceiro;
        }

        public async Task Delete(Parceiro parceiro)
        {
            _unitOfWork.ParceiroRepository.Delete(parceiro);
            await _unitOfWork.Commit();
        }

        public IEnumerable<Parceiro> GetParceiros()
        {
            return _unitOfWork.ParceiroRepository.Get();
        }

        public async Task<IEnumerable<Medico>> ObterMedicosParceiros(string UfCrm)
        {
            return await _unitOfWork.ParceiroRepository.ObterMedicosParceiros(UfCrm);
        }

        public async Task Update(Parceiro parceiro)
        {
            _unitOfWork.ParceiroRepository.Update(parceiro);
            await _unitOfWork.Commit();
        }

        public Task<Parceiro> UpdatePatch(Guid id)
        {
            return _unitOfWork.ParceiroRepository.UpdatePatch(id);
        }

        public Task<bool> ValidarApiKey(Guid apiKey)
        {
            return _unitOfWork.ParceiroRepository.ValidarApiKey(apiKey);
        }
    }
}
