namespace MServices.Domain.Handlers
{
    using System;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    using AutoMapper;

    using MediatR;

    using MServices.Domain.Commands;
    using MServices.Domain.Dtos;
    using MServices.Domain.Menssages;
    using MServices.Domain.Models;
    using MServices.Domain.Repositorys.Interfaces;

    public class CadastroVeiculoHandler : IRequestHandler<VeiculoCadastroCommand, RetornoDto>
    {
        private readonly IRepository<Veiculo> veiculoRepository;
        private readonly IMapper mapper;

        public CadastroVeiculoHandler(IRepository<Veiculo> veiculoRepository, IMapper mapper)
        {
            this.veiculoRepository = veiculoRepository;
            this.mapper = mapper;
        }

        public async Task<RetornoDto> Handle(VeiculoCadastroCommand request, CancellationToken cancellationToken)
        {
            var resultado = new RetornoDto();
            try
            {
                var veiculoChassi = await this.veiculoRepository.ObterPorExpressao(a => a.Chassi == request.Veiculo.Chassi);

                if (veiculoChassi.Any())
                {
                    resultado.Sucesso = false;
                    resultado.Mensagem = string.Format(RetornoProcesso.Chassis_Registred, request.Veiculo.Chassi);
                    return resultado;
                }

                var veiculo = this.mapper.Map<Veiculo>(request.Veiculo);
                await this.veiculoRepository.Inserir(veiculo);

                resultado.Mensagem = RetornoProcesso.Sucess_Register_Vehicle;
                resultado.Sucesso = true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                resultado.Sucesso = false;
                resultado.Mensagem = Excecoes.Fail_Register_Vehicle;
            }

            return resultado;
        }
    }
}
