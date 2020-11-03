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

    public class EditarVeiculoHandler : IRequestHandler<EditarVeiculoCommand, RetornoDto>
    {
        private readonly IRepository<Veiculo> veiculoRepository;

        public EditarVeiculoHandler(IRepository<Veiculo> veiculoRepository)
        {
            this.veiculoRepository = veiculoRepository;
        }

        public async Task<RetornoDto> Handle(EditarVeiculoCommand request, CancellationToken cancellationToken)
        {
            var resultado = new RetornoDto();
            try
            {
                var veiculo = (await this.veiculoRepository.ObterPorExpressao(a => a.Chassi == request.Veiculo.Chassi)).FirstOrDefault();

                if (veiculo == null)
                {
                    resultado.Sucesso = false;
                    resultado.Mensagem = string.Format(Excecoes.ID_NotFound, request.Veiculo.Chassi);
                    return resultado;
                }

                veiculo.Cor = request.Veiculo.Cor;
                await this.veiculoRepository.Atualizar(veiculo);

                resultado.Sucesso = true;
                resultado.Mensagem = RetornoProcesso.Sucess_Update_Vehicle;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                resultado.Sucesso = false;
                resultado.Mensagem = Excecoes.Fail_Update_Vehicle;
            }

            return resultado;
        }
    }
}
