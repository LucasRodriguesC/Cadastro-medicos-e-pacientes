using AutoMapper;
using BuiltCode.API.Dto;
using BuiltCode.Domain.Abstractions;
using BuiltCode.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BuiltCode.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MedicosController : ControllerBase
    {
        private readonly IMedicoService _medicoService;
        private readonly IMapper _mapper;

        public MedicosController(IMedicoService medicoService, IMapper mapper)
        {
            _medicoService = medicoService;
            _mapper = mapper;
        }

        /// <summary>
        /// Retorna lista de todos os médicos cadastrados.
        /// </summary>
        /// <returns></returns>
        [Authorize(Roles = "Admin, Atendente")]
        [HttpGet]
        public IEnumerable<MedicoDto> ObterMedicos()
        {
            return _mapper.Map<IEnumerable<MedicoDto>>(_medicoService.GetMedicos().ToList());
        }

        /// <summary>
        /// Endpoint para cadastro de novos médicos
        /// </summary>
        /// <param name="medicoDto"></param>
        /// <returns></returns>
        [Authorize(Roles = "Admin, Atendente")]
        [HttpPost]
        public async Task<ActionResult<Medico>> CadastrarMedico(MedicoCadastroDto medicoDto)
        {
            if (await CrmRegistrado(medicoDto)) return BadRequest(new { message = "Crm e UfCrm já cadastrados" });
            return await _medicoService.Add(_mapper.Map<Medico>(medicoDto));
        }

        /// <summary>
        /// Endpoint para atualizar dados de médicos
        /// </summary>
        /// <param name="medicoDto"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        [Authorize(Roles = "Admin, Atendente")]
        [HttpPut("{id}")]
        public async Task<ActionResult<Medico>> AtualizarMedico(MedicoCadastroDto medicoDto, Guid id)
        {
            var medico = await _medicoService.GetById(id);

            if(medico == null) return NotFound(new { message = "Id ínválido" });

            medico.Nome = medicoDto.Nome;
            medico.Crm = medicoDto.Crm;
            medico.UfCrm = medicoDto.UfCrm;
            medico.Especialidade = medicoDto.Especialidade;

            if (await CrmRegistradoUpdate(medico)) return BadRequest(new { message = "Crm e UfCrm já cadastrados" });

            await _medicoService.Update(medico);

            return medico;
        }

        /// <summary>
        /// Endpoint para exclusão de médicos.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Authorize(Roles = "Admin, Atendente")]
        [HttpDelete("{id}")]
        public async Task<ActionResult<Medico>> DeletarMedico(Guid id)
        {
            var medico = await _medicoService.GetById(id);

            if (medico == null) return NotFound(new { message = "Id inválido" });

            if (await _medicoService.MedicoComPacientes(medico)) return BadRequest(new { message = "Médico não pode ser excluído, possui pacientes cadastrados" });

            await _medicoService.Delete(medico);

            return Ok(new { message = "Medico excluído com sucesso" });
        }

        private async Task<bool> CrmRegistrado(MedicoCadastroDto medicoDto)
        {
            return await _medicoService.CrmEmUso(medicoDto.Crm, medicoDto.UfCrm);
        }

        private async Task<bool> CrmRegistradoUpdate(Medico medico)
        {
            return await _medicoService.CrmEmUsoUpdate(medico);
        }
    }
}
