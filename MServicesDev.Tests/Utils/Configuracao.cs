namespace MServicesDev.Tests.Utils
{
    using AutoMapper;

    using MServices.Domain.Mapper;

    public class Configuracao
    {
        public static IMapper ObterInstanciaMapper()
        {
            return new MapperConfiguration(a =>
                {
                    a.AddProfile<MapperProfile>();
                }).CreateMapper();
        }
    }
}
