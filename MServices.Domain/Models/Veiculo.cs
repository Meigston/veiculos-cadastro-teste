namespace MServices.Domain.Models
{
    using System.ComponentModel.DataAnnotations;

    using MServices.Domain.Enums;
    using MServices.Domain.Models.Base;

    public class Veiculo : ModelBase
    {
        [MaxLength(17)]
        public string Chassi { get; set; }

        [MaxLength(2)]
        public TipoVeiculo Tipo { get; set; }

        [MaxLength(2)]
        public Passageiros QuantidadePassageiros { get; set; }

        [MaxLength(10)]
        public string Cor { get; set; }
    }
}
