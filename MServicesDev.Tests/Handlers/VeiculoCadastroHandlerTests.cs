namespace MServicesDev.Tests.Handlers
{
    using System.Threading;
    using System.Threading.Tasks;

    using AutoMapper;

    using Moq;

    using MServices.Domain.Commands;
    using MServices.Domain.Handlers;
    using MServices.Domain.Menssages;
    using MServices.Domain.Repositorys;

    using MServicesDev.Tests.Utils;

    using Xunit;

    public class VeiculoCadastroHandlerTests
    {
        private IMapper mapper;

        public VeiculoCadastroHandlerTests()
        {
            this.mapper = Configuracao.ObterInstanciaMapper();
        }

        [Fact(DisplayName = "Cadastro Veiculo - Sucesso")]
        public async Task CadastroVeiculoTest()
        {
            var command = new VeiculoCadastroCommand();
            var handler = new CadastroVeiculoHandler(new Mock<VeiculoRepository>().Object, this.mapper);
            var resultado = await handler.Handle(command, CancellationToken.None);

            Assert.True(resultado.Sucesso);
            Assert.Equal(resultado.Mensagem, RetornoProcesso.Sucess_Register_Vehicle);
        }

        [Fact(DisplayName = "Cadastro Veiculo - Erro")]
        public async Task CadastroVeiculoTest2()
        {
            var command = new VeiculoCadastroCommand();
            var handler = new CadastroVeiculoHandler(null, this.mapper);
            var resultado = await handler.Handle(command, CancellationToken.None);

            Assert.False(resultado.Sucesso);
            Assert.Equal(resultado.Mensagem, Excecoes.Fail_Register_Vehicle);
        }

        [Fact(DisplayName = "Cadastro Veiculo - Chassi ja cadastrado")]
        public async Task CadastroVeiculoTest3()
        {
            var command = new VeiculoCadastroCommand();
            var handler = new CadastroVeiculoHandler(new Mock<VeiculoRepository>().Object, this.mapper);
            var resultado = await handler.Handle(command, CancellationToken.None);

            Assert.False(resultado.Sucesso);
            Assert.Equal(resultado.Mensagem, RetornoProcesso.Chassis_Registred);
        }
    }
}
