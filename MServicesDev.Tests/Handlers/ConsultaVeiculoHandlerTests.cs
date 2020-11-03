namespace MServicesDev.Tests.Handlers
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    using AutoMapper;

    using MServices.Domain.Handlers;
    using MServices.Domain.Models;
    using MServices.Domain.Querys;
    using MServices.Domain.Repositorys.Interfaces;

    using MServicesDev.Tests.Utils;

    using Xunit;

    public class ConsultaVeiculoHandlerTests : IDisposable
    {
        private IRepository<Veiculo> veiculoRepository;
        private IMapper mapper;

        public ConsultaVeiculoHandlerTests()
        {
            this.mapper = Configuracao.ObterInstanciaMapper();
            this.veiculoRepository = new InMemoryRepository<Veiculo>();
        }

        [Fact(DisplayName = "Consulta veículo por Chassi")]
        public async Task ConsultaVeiculoTeste()
        {
            var veiculo = this.mapper.Map<Veiculo>(VeiculosUtils.CriarVeiculoDto());
            var veiculo2 = this.mapper.Map<Veiculo>(VeiculosUtils.CriarVeiculoDto());

            await this.veiculoRepository.Inserir(veiculo);
            await this.veiculoRepository.Inserir(veiculo2);

            var query = new ConsultaVeiculoChassiQuery { Chassi = veiculo.Chassi };
            var handler = new ConsultaVeiculoHandler(this.veiculoRepository, this.mapper);
            var response = await handler.Handle(query, CancellationToken.None);

            Assert.True(response.Sucesso);
            Assert.NotNull(response.Objeto);
            Assert.Equal(response.Objeto.Chassi, query.Chassi);
        }

        public void Dispose()
        {
            this.mapper = null;
            this.veiculoRepository = null;
        }
    }
}
