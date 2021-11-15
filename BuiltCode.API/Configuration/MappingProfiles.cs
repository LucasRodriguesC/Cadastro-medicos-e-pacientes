using AutoMapper;
using BuiltCode.API.Dto;
using BuiltCode.Domain.Entities;

namespace BuiltCode.API.Configuration
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Parceiro, ParceiroDto>();
            CreateMap<Medico, MedicoDto>();
            CreateMap<MedicoCadastroDto, Medico>();
            CreateMap<PacienteCadastroDto, Paciente>();
            CreateMap<PacienteAtualizarDto, Paciente>();
        }
    }
}
