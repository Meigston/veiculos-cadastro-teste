namespace MServices.Api.Controllers
{
    using System.Threading.Tasks;

    using MediatR;

    using Microsoft.AspNetCore.Mvc;

    using MServices.Api.Validators;
    using MServices.Domain.Commands;
    using MServices.Domain.Dtos;
    using MServices.Domain.Querys;

    [Microsoft.AspNetCore.Components.Route("veiculo")]
    public class VeiculoController : ControllerBase
    {
        private readonly IMediator mediator;

        public VeiculoController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        /// <summary>
        /// Cadastro de veículo
        /// </summary>
        /// <param name="command">dados do veículo</param>
        /// <returns>situação do processo</returns>
        [HttpPost("cadastrar-veiculo")]
        public async Task<IActionResult> Cadastrar([FromBody] VeiculoCadastroCommand command)
        {
            var validator = new VeiculoValidator().Validate(command.Veiculo);
            if (!validator.IsValid)
            {
                return this.BadRequest(validator.Errors);
            }

            var resultado = await this.mediator.Send(command);
            return Retorno(resultado);
        }

        /// <summary>
        /// Atualizar dados do veículo
        /// </summary>
        /// <param name="command">dados a serem atualizados</param>
        /// <returns>situação do processo</returns>
        [HttpPut("atualizar-veiculo")]
        public async Task<IActionResult> Atualizar([FromBody] EditarVeiculoCommand command)
        {
            var validator = new VeiculoValidator().Validate(command.Veiculo);
            if (!validator.IsValid)
            {
                return this.BadRequest(validator.Errors);
            }

            var resultado = await this.mediator.Send(command);
            return Retorno(resultado);
        }

        /// <summary>
        /// Consulta de veículo por Chassi
        /// </summary>
        /// <param name="chassi">Número do chassi</param>
        /// <returns>Dados do veículo</returns>
        [HttpGet("obter-veiculo")]
        public async Task<IActionResult> ObterVeiculoPorChassi([FromQuery] string chassi)
        {
            var resultado = await this.mediator.Send(new ConsultaVeiculoChassiQuery { Chassi = chassi });
            return Retorno(resultado);
        }

        /// <summary>
        /// Consulta de todos os veículos cadastrados
        /// </summary>
        /// <returns>Dados dos veículos cadastrados</returns>
        [HttpGet("obter-todos-veiculos")]
        public async Task<IActionResult> ObterTodosVeiculos()
        {
            var resultado = await this.mediator.Send(new ConsultaVeiculosTodosQuery());
            return Retorno(resultado);
        }

        /// <summary>
        /// Remove um veículo cadastrado
        /// </summary>
        /// <param name="chassi">Número do chassi</param>
        /// <returns>situação do processo</returns>
        [HttpDelete("remover-veiculo")]
        public async Task<IActionResult> RemoverVeiculo([FromQuery] string chassi)
        {
            var resultado = await this.mediator.Send(new RemoverVeiculoCommand { Chassi = chassi });
            return Retorno(resultado);
        }

        private IActionResult Retorno(RetornoDto resultado)
        {
            return resultado.Sucesso ? (IActionResult)this.Ok(resultado) : this.BadRequest(resultado);
        }

        private IActionResult Retorno<T>(RetornoDto<T> resultado)
        {
            return resultado.Sucesso ? (IActionResult)this.Ok(resultado) : this.BadRequest(resultado);
        }
    }
}
