namespace MServices.Domain.Handlers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    using AutoMapper;

    using MediatR;

    using MServices.Domain.Dtos;
    using MServices.Domain.Menssages;
    using MServices.Domain.Models;
    using MServices.Domain.Querys;
    using MServices.Domain.Services.Interfaces;

    public class ConsultaVeiculoHandler : IRequestHandler<ConsultaVeiculosTodosQuery, RetornoDto<IEnumerable<VeiculoDto>>>,
                                          IRequestHandler<ConsultaVeiculoChassiQuery, RetornoDto<VeiculoDto>>
    {
        private readonly IRepository<Veiculo> veiculoRepository;
        private readonly IMapper mapper;

        public ConsultaVeiculoHandler(IRepository<Veiculo> veiculoRepository, IMapper mapper)
        {
            this.veiculoRepository = veiculoRepository;
            this.mapper = mapper;
        }

        public async Task<RetornoDto<IEnumerable<VeiculoDto>>> Handle(ConsultaVeiculosTodosQuery request, CancellationToken cancellationToken)
        {
            var resultado = new RetornoDto<IEnumerable<VeiculoDto>>();

            try
            {
                var veiculos = await this.veiculoRepository.ObterTodos();
                resultado.Objeto = this.mapper.Map<IEnumerable<VeiculoDto>>(veiculos);
                resultado.Sucesso = true;

                if (!veiculos.Any())
                {
                    resultado.Sucesso = true;
                    resultado.Mensagem = RetornoProcesso.Empty_Vehicles;
                }
            }
            catch (Exception e)
            {
                resultado.Sucesso = false;
                resultado.Mensagem = Excecoes.Fail_Search_Vehicles;
            }

            return resultado;
        }

        public async Task<RetornoDto<VeiculoDto>> Handle(ConsultaVeiculoChassiQuery request, CancellationToken cancellationToken)
        {
            var resultado = new RetornoDto<VeiculoDto>();
            try
            {
                var veiculo = (await this.veiculoRepository.ObterPorExpressao(a => a.Chassi == request.Chassi)).FirstOrDefault();

                if (veiculo == null)
                {
                    resultado.Sucesso = false;
                    resultado.Mensagem = string.Format(Excecoes.ID_NotFound, request.Chassi);
                    return resultado;
                }

                resultado.Objeto = this.mapper.Map<VeiculoDto>(veiculo);
                resultado.Sucesso = true;
            }
            catch (Exception e)
            {
                resultado.Sucesso = false;
                resultado.Mensagem = Excecoes.Fail_Search_Vehicles;
            }

            return resultado;
        }
    }
}
