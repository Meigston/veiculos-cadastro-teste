namespace MServices.Domain.Dtos
{
    using System;

    using MServices.Domain.Enums;

    public class VeiculoDto
    {
        public string Chassi { get; set; }

        public TipoVeiculo Tipo { get; set; }

        public Passageiros QuantidadePassageiros
        {
            get
            {
                switch (this.Tipo)
                {
                    case TipoVeiculo.Onibus:
                        return Passageiros.Acentos42;

                    case TipoVeiculo.Caminhao:
                        return Passageiros.Acentos2;
                    default:
                        throw new Exception("Tipo inválido");
                }
            }
        }

        public string Cor { get; set; }
    }
}
