namespace MServices.Domain.Querys
{
    using MediatR;

    using MServices.Domain.Dtos;

    public class ConsultaVeiculoChassiQuery : IRequest<RetornoDto<VeiculoDto>>
    {
        public string Chassi { get; set; }
    }
}
