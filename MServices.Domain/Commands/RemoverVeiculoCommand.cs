namespace MServices.Domain.Commands
{
    using MediatR;

    using MServices.Domain.Dtos;

    public class RemoverVeiculoCommand : IRequest<RetornoDto>
    {
        public string Chassi { get; set; }
    }
}
