namespace MServices.Api.Validators
{
    using FluentValidation;

    using MServices.Domain.Dtos;

    public class VeiculoValidator : AbstractValidator<VeiculoDto>
    {
        public VeiculoValidator()
        {
            RuleFor(x => x.Chassi)
                .NotNull().WithMessage("Informe o número do Chassi.")
                .NotEmpty().WithMessage("Informe o número do Chassi.")
                .Length(17).WithMessage("Um chassi tem 17 caracteres.");

            RuleFor(x => x.Tipo)
                .NotNull().WithMessage("Informe o tipo do veículo.")
                .Custom(
                    (tipo, context) =>
                        {
                            if (tipo == 0)
                            {
                                context.AddFailure("Informe um tipo de veículo válido!");
                            }
                        });

            RuleFor(x => x.QuantidadePassageiros)
                .NotNull().WithMessage("Informe a quantidade de passageiros do veículo.")
                .Custom(
                    (tipo, context) =>
                        {
                            if (tipo == 0)
                            {
                                context.AddFailure("Informe uma quantidade de passageiros de acordo com as quantidades permitidas!");
                            }
                        });

            RuleFor(x => x.Cor)
                .NotNull().WithMessage("Informe a cor do véiculo.")
                .NotEmpty().WithMessage("Informe a cor do véiculo.")
                .Length(3, 10).WithMessage("Informe um nome de cor válido para o veículo.");
        }
    }
}
