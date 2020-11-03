namespace MServicesDev.Tests.Handlers
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    using AutoMapper;

    using MServices.Domain.Commands;
    using MServices.Domain.Handlers;
    using MServices.Domain.Menssages;
    using MServices.Domain.Models;
    using MServices.Domain.Repositorys.Interfaces;

    using MServicesDev.Tests.Utils;

    using Xunit;

    public class RemoverVeiculoHandlerTests : IDisposable
    {
        private IRepository<Veiculo> veiculoRepository;
        private IMapper mapper;

        public RemoverVeiculoHandlerTests()
        {
            this.mapper = Configuracao.ObterInstanciaMapper();
            this.veiculoRepository = new InMemoryRepository<Veiculo>();
        }

        [Fact(DisplayName = "Remover Veiculo")]
        public async Task RemoverVeiculoTest()
        {
            var veiculo = this.mapper.Map<Veiculo>(VeiculosUtils.CriarVeiculoDto());
            await this.veiculoRepository.Inserir(veiculo);

            var veiculoSalvo = await this.veiculoRepository.ObterPorId(veiculo.Id);

            Assert.NotNull(veiculoSalvo);

            var command = new RemoverVeiculoCommand { Chassi = veiculo.Chassi };
            var handler = new RemoverVeiculoHandler(this.veiculoRepository);
            var response = await handler.Handle(command, CancellationToken.None);

            Assert.True(response.Sucesso);
            Assert.Equal(RetornoProcesso.Sucess_Remove_Vehicle, response.Mensagem);

            veiculoSalvo = await this.veiculoRepository.ObterPorId(veiculo.Id);

            Assert.Null(veiculoSalvo);
        }

        public void Dispose()
        {
            this.mapper = null;
            this.veiculoRepository = null;
        }
    }
}
