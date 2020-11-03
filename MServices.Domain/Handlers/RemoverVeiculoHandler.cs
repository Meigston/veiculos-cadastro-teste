namespace MServices.Domain.Handlers
{
    using System;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    using MediatR;

    using MServices.Domain.Commands;
    using MServices.Domain.Dtos;
    using MServices.Domain.Menssages;
    using MServices.Domain.Models;
    using MServices.Domain.Repositorys.Interfaces;

    public class RemoverVeiculoHandler : IRequestHandler<RemoverVeiculoCommand, RetornoDto>
    {
        private readonly IRepository<Veiculo> veiculoRepository;

        public RemoverVeiculoHandler(IRepository<Veiculo> veiculoRepository)
        {
            this.veiculoRepository = veiculoRepository;
        }

        public async Task<RetornoDto> Handle(RemoverVeiculoCommand request, CancellationToken cancellationToken)
        {
            var resultado = new RetornoDto();

            try
            {
                var veiculo = (await this.veiculoRepository.ObterPorExpressao(a => a.Chassi == request.Chassi)).FirstOrDefault();

                if (veiculo == null)
                {
                    resultado.Sucesso = false;
                    resultado.Mensagem = string.Format(Excecoes.ID_NotFound, request.Chassi);
                    return resultado;
                }

                await this.veiculoRepository.Remover(veiculo.Id);

                resultado.Sucesso = true;
                resultado.Mensagem = RetornoProcesso.Sucess_Remove_Vehicle;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                resultado.Sucesso = false;
                resultado.Mensagem = Excecoes.Fail_Remove_Vehicle;
            }

            return resultado;
        }
    }
}
