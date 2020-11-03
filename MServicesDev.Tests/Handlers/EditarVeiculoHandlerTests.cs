namespace MServicesDev.Tests.Handlers
{
    using System;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    using AutoMapper;

    using MServices.Domain.Commands;
    using MServices.Domain.Dtos;
    using MServices.Domain.Handlers;
    using MServices.Domain.Menssages;
    using MServices.Domain.Models;
    using MServices.Domain.Repositorys.Interfaces;

    using MServicesDev.Tests.Utils;

    using Xunit;

    public class EditarVeiculoHandlerTests : IDisposable
    {
        private IRepository<Veiculo> veiculoRepository;
        private IMapper mapper;

        public EditarVeiculoHandlerTests()
        {
            this.veiculoRepository = new InMemoryRepository<Veiculo>();
            this.mapper = Configuracao.ObterInstanciaMapper();
        }

        [Fact(DisplayName = "Editar veículo")]
        public async Task EditarVeiculoTest()
        {
            var veiculo = this.mapper.Map<Veiculo>(VeiculosUtils.CriarVeiculoDto());

            await this.veiculoRepository.Inserir(veiculo);
            var command = new EditarVeiculoCommand
            {
                Veiculo = new VeiculoDto()
                {
                    Chassi = veiculo.Chassi,
                    Cor = "Branco"
                }
            };

            var handler = new EditarVeiculoHandler(this.veiculoRepository);
            var response = await handler.Handle(command, CancellationToken.None);

            Assert.True(response.Sucesso);
            Assert.Equal(RetornoProcesso.Sucess_Update_Vehicle, response.Mensagem);

            var veiculoSalvo = (await this.veiculoRepository.ObterPorExpressao(a => a.Chassi == command.Veiculo.Chassi)).First();

            Assert.Equal(command.Veiculo.Cor, veiculoSalvo.Cor);

        }

        public void Dispose()
        {
            this.mapper = null;
            this.veiculoRepository = null;
        }
    }
}
