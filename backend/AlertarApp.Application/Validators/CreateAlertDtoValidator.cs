using AlertarApp.Application.DTOs.Alert;
using FluentValidation;

namespace AlertarApp.Application.Validators
{
    public class CreateAlertDtoValidator : AbstractValidator<CreateAlertDto>
    {
        public CreateAlertDtoValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("Título é obrigatório")
                .MaximumLength(100).WithMessage("Título deve ter no máximo 100 caracteres");

            RuleFor(x => x.Description)
                .MaximumLength(500).WithMessage("Descrição deve ter no máximo 500 caracteres");

            RuleFor(x => x.Latitude)
                .NotEmpty().WithMessage("Latitude é obrigatória")
                .InclusiveBetween(-90, 90).WithMessage("Latitude deve estar entre -90 e 90");

            RuleFor(x => x.Longitude)
                .NotEmpty().WithMessage("Longitude é obrigatória")
                .InclusiveBetween(-180, 180).WithMessage("Longitude deve estar entre -180 e 180");
        }
    }
}
