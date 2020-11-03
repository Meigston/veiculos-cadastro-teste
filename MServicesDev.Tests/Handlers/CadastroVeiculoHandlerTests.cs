namespace MServicesDev.Tests.Handlers
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    using AutoMapper;

    using Microsoft.Extensions.Logging;

    using Moq;

    using MServices.Domain.Commands;
    using MServices.Domain.Handlers;
    using MServices.Domain.Menssages;
    using MServices.Domain.Models;
    using MServices.Domain.Repositorys.Interfaces;

    using MServicesDev.Tests.Utils;

    using Xunit;

    public class CadastroVeiculoHandlerTests : IDisposable
    {
        private IMapper mapper;
        private IRepository<Veiculo> veiculoRepository;

        public CadastroVeiculoHandlerTests()
        {
            this.mapper = Configuracao.ObterInstanciaMapper();
            this.veiculoRepository = new InMemoryRepository<Veiculo>();
        }

        [Fact(DisplayName = "Cadastro Veiculo - Sucesso")]
        public async Task CadastroVeiculoTest()
        {
            var command = new VeiculoCadastroCommand
            {
                Veiculo = VeiculosUtils.CriarVeiculoDto("ABCD123456789VBLO")
            };

            var handler = new CadastroVeiculoHandler(this.veiculoRepository, this.mapper);
            var resultado = await handler.Handle(command, CancellationToken.None);

            Assert.True(resultado.Sucesso);
            Assert.Equal(resultado.Mensagem, RetornoProcesso.Sucess_Register_Vehicle);
        }

        [Fact(DisplayName = "Cadastro Veiculo - Erro")]
        public async Task CadastroVeiculoTest2()
        {
            var command = new VeiculoCadastroCommand
            {
                Veiculo = VeiculosUtils.CriarVeiculoDto()
            };
            var handler = new CadastroVeiculoHandler(null, this.mapper);
            var resultado = await handler.Handle(command, CancellationToken.None);

            Assert.False(resultado.Sucesso);
            Assert.Equal(resultado.Mensagem, Excecoes.Fail_Register_Vehicle);
        }

        [Fact(DisplayName = "Cadastro Veiculo - Chassi ja cadastrado")]
        public async Task CadastroVeiculoTest3()
        {
            await this.CadastroVeiculoTest();
            var command = new VeiculoCadastroCommand
            {
                Veiculo = VeiculosUtils.CriarVeiculoDto("ABCD123456789VBLO")
            };
            var handler = new CadastroVeiculoHandler(this.veiculoRepository, this.mapper);
            var resultado = await handler.Handle(command, CancellationToken.None);

            Assert.False(resultado.Sucesso);
            Assert.Equal(resultado.Mensagem, string.Format(RetornoProcesso.Chassis_Registred, command.Veiculo.Chassi));
        }

        public void Dispose()
        {
            this.mapper = null;
            this.veiculoRepository = null;
        }
    }
}
