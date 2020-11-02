namespace MServices.Domain.Commands
{
    using MediatR;

    using MServices.Domain.Dtos;

    public class VeiculoCadastroCommand : IRequest<RetornoDto>
    {
        public VeiculoDto Veiculo { get; set; }
    }
}
