namespace MServicesDev.Tests.Utils
{
    using System;

    using FizzWare.NBuilder;

    using MServices.Domain.Dtos;
    using MServices.Domain.Enums;

    public class VeiculosUtils
    {
        public static VeiculoDto CriarVeiculoDto(string chassi = null)
        {
            return Builder<VeiculoDto>.CreateNew()
                .With(a => a.Chassi = chassi ?? Guid.NewGuid().ToString())
                .With(a => a.Cor = "Azul")
                .With(a => a.Tipo = TipoVeiculo.Onibus)
                .Build();
        }
    }
}
