namespace MServices.Domain.Commands
{
    using MediatR;

    using MServices.Domain.Dtos;

    public class EditarVeiculoCommand : IRequest<RetornoDto>
    {
        public VeiculoDto Veiculo { get; set; }
    }
}
