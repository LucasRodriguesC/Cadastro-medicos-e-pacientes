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
    public class ParceirosController : ControllerBase
    {
        private readonly IParceiroService _parceiroService;
        private readonly IMapper _mapper;

        public ParceirosController(IParceiroService parceiroService, IMapper mapper)
        {
            _parceiroService = parceiroService;
            _mapper = mapper;
        }

        /// <summary>
        /// Endpoint para listagem de parceiros.
        /// </summary>
        /// <returns></returns>
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IEnumerable<ParceiroDto> ObterParceiros()
        {
            return _mapper.Map<IEnumerable<ParceiroDto>>(_parceiroService.GetParceiros().ToList());
        }

        /// <summary>
        /// Endpoint para listagem de médicos por parceiros
        /// </summary>
        /// <param name="apiKey"></param>
        /// <param name="ufCrm"></param>
        /// <returns></returns>
        [HttpGet("listagem-medicos-parceiros")]
        public async Task<ActionResult<ParceiroDto>> ObterMedicosParceiros([FromHeader]Guid apiKey, [FromQuery]string? ufCrm)
        {

            if (!await _parceiroService.ValidarApiKey(apiKey))
            {
                return Unauthorized(new { Message = "ApiKey inválida" });
            }

            return Ok(_mapper.Map<IEnumerable<MedicoDto>>(await _parceiroService.ObterMedicosParceiros(ufCrm)));
        }

        /// <summary>
        /// Endpoint para cadastro de parceiros
        /// </summary>
        /// <param name="parceiroDto"></param>
        /// <returns></returns>
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<Parceiro> CadastrarParceiro(ParceiroCadastroDto parceiroDto)
        {
            var parceiro = new Parceiro()
            {
                Nome = parceiroDto.Nome
            };

            return await _parceiroService.Add(parceiro);
        }

        /// <summary>
        /// Endpoint para atualização de ApiKeys de parceiros
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Authorize(Roles = "Admin")]
        [HttpPatch("{id}")]
        public async Task<ActionResult<Parceiro>> AtualizarApiKey(Guid id)
        {
             var parceiro = await _parceiroService.UpdatePatch(id);

            if(parceiro == null) return NotFound(new { message = "Id inválido" });

            return parceiro;
        }



    }
}
