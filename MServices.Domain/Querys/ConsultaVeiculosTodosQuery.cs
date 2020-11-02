namespace MServices.Domain.Querys
{
    using System.Collections.Generic;

    using MediatR;

    using MServices.Domain.Dtos;

    public class ConsultaVeiculosTodosQuery : IRequest<RetornoDto<IEnumerable<VeiculoDto>>>
    {
    }
}
