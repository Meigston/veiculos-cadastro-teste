namespace MServices.Domain.Mapper
{
    using AutoMapper;

    using MServices.Domain.Commands;
    using MServices.Domain.Dtos;
    using MServices.Domain.Models;

    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<VeiculoCadastroCommand, Veiculo>()
                .ForMember(a => a.Chassi, s => s.MapFrom(m => m.Veiculo.Chassi))
                .ForMember(a => a.Cor, s => s.MapFrom(m => m.Veiculo.Cor))
                .ForMember(a => a.QuantidadePassageiros, s => s.MapFrom(m => m.Veiculo.QuantidadePassageiros))
                .ForMember(a => a.Tipo, s => s.MapFrom(m => m.Veiculo.Tipo));

            CreateMap<VeiculoDto, Veiculo>()
                .ReverseMap();
        }
    }
}
