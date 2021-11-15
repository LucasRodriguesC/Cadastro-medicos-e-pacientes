using AutoMapper;
using BuiltCode.API.Dto;
using BuiltCode.Domain.Abstractions;
using BuiltCode.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BuiltCode.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PacienteController : ControllerBase
    {
        private readonly IPacienteService _pacienteService;
        private readonly IMapper _mapper;

        public PacienteController(IPacienteService pacienteService, IMapper mapper)
        {
            _pacienteService = pacienteService;
            _mapper = mapper;
        }

		/// <summary>
		/// Endpoint para obter todos os pacientes cadastrados
		/// </summary>
		/// <returns></returns>
        [Authorize(Roles = "Admin, Atendente")]
        [HttpGet]
        public async Task<IEnumerable<Paciente>> ObterPacientes()
        {
            return await _pacienteService.GetPacientes();
        }

		/// <summary>
		/// Endpoint para cadatro de novos pacientes
		/// </summary>
		/// <param name="pacienteDto"></param>
		/// <returns></returns>
		[Authorize(Roles = "Admin, Atendente")]
		[HttpPost]
        public async Task<ActionResult<Paciente>> AdicionarPaciente(PacienteCadastroDto pacienteDto)
        {
            var paciente = _mapper.Map<Paciente>(pacienteDto);

            if (await ValidarCpfEmUso(paciente.Cpf)) return BadRequest(new { message = "CPF já cadastrado!" });

			if (!ValidaCPF.IsCpf(paciente.Cpf)) return BadRequest(new { message = "CPF inválido" });

			if (!await ValidaMedicoCadastrado(paciente.MedicoId)) return BadRequest(new { message = "MedicoId inválido" });

			return await _pacienteService.Add(paciente);
        }

		/// <summary>
		/// Endpoint para atualizadar dados de pacientes.
		/// </summary>
		/// <param name="pacienteDto"></param>
		/// <param name="id"></param>
		/// <returns></returns>
		[Authorize(Roles = "Admin, Atendente")]
		[HttpPut("{id}")]
		public async Task<ActionResult<Paciente>> AtualizarPaciente(PacienteCadastroDto pacienteDto, Guid id)
		{
			var paciente = await _pacienteService.GetById(id);

			if (paciente == null) return NotFound(new { message = "Id não encontrado" });

			paciente.Nome = pacienteDto.Nome;
			paciente.Cpf = pacienteDto.Cpf;
			paciente.Telefone = pacienteDto.Telefone;
			paciente.MedicoId = pacienteDto.MedicoId;

			if(await ValidarCpfEmUso(paciente)) return BadRequest(new { message = "CPF já cadastrado." });

			if (!ValidaCPF.IsCpf(paciente.Cpf)) return BadRequest(new { message = "CPF inválido." });

			if (!await ValidaMedicoCadastrado(paciente.MedicoId)) return BadRequest(new { message = "MedicoId inválido" });

			return await _pacienteService.Update(paciente);
		}

		/// <summary>
		/// Endpoint para deletar pacientes
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		[Authorize(Roles = "Admin, Atendente")]
		[HttpDelete("{id}")]
		public async Task<ActionResult<Medico>> DeletarMedico(Guid id)
		{
			var paciente = await _pacienteService.GetById(id);

			if (paciente == null) return NotFound(new { message = "Id inválido" });

			await _pacienteService.Delete(paciente);

			return Ok(new { message = "Paciente excluído com sucesso" });
		}

		private async Task<bool> ValidaMedicoCadastrado(Guid id)
        {
			return await _pacienteService.ValidaMedicoCadastrado(id);
        }

		private async Task<bool> ValidarCpfEmUso(Paciente paciente)
        {
			return await _pacienteService.ValidarCpfEmUso(paciente);
		}

        private async Task<bool> ValidarCpfEmUso(string Cpf)
        {
            return await _pacienteService.ValidarCpfEmUso(Cpf);
        }

		public static class ValidaCPF
		{
			public static bool IsCpf(string cpf)
			{
				int[] multiplicador1 = new int[9] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
				int[] multiplicador2 = new int[10] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };
				string tempCpf;
				string digito;
				int soma;
				int resto;
				cpf = cpf.Trim();
				cpf = cpf.Replace(".", "").Replace("-", "");
				if (cpf.Length != 11)
					return false;
				tempCpf = cpf.Substring(0, 9);
				soma = 0;

				for (int i = 0; i < 9; i++)
					soma += int.Parse(tempCpf[i].ToString()) * multiplicador1[i];
				resto = soma % 11;
				if (resto < 2)
					resto = 0;
				else
					resto = 11 - resto;
				digito = resto.ToString();
				tempCpf = tempCpf + digito;
				soma = 0;
				for (int i = 0; i < 10; i++)
					soma += int.Parse(tempCpf[i].ToString()) * multiplicador2[i];
				resto = soma % 11;
				if (resto < 2)
					resto = 0;
				else
					resto = 11 - resto;
				digito = digito + resto.ToString();
				return cpf.EndsWith(digito);
			}
		}
	}
}
