using AlertarApp.Application.DTOs.TrustedContact;
using FluentValidation;

namespace AlertarApp.Application.Validators
{
    public class CreateTrustedContactDtoValidator : AbstractValidator<CreateTrustedContactDto>
    {
        public CreateTrustedContactDtoValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Nome é obrigatório")
                .MaximumLength(100).WithMessage("Nome deve ter no máximo 100 caracteres");

            RuleFor(x => x.Phone)
                .NotEmpty().WithMessage("Telefone é obrigatório")
                .MaximumLength(20).WithMessage("Telefone deve ter no máximo 20 caracteres")
                .Matches(@"^\(\d{2}\)\s?\d{4,5}-\d{4}$").WithMessage("Telefone inválido. Use o formato (XX) XXXXX-XXXX");

            RuleFor(x => x.Email)
                .EmailAddress().WithMessage("Email inválido")
                .MaximumLength(100).WithMessage("Email deve ter no máximo 100 caracteres")
                .When(x => !string.IsNullOrEmpty(x.Email) || x.SendEmail);

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email é obrigatório quando a opção de envio por email está ativada")
                .When(x => x.SendEmail);

            RuleFor(x => x.SendSms)
                .Equal(true).WithMessage("Pelo menos um método de contato deve estar ativado")
                .When(x => !x.SendEmail);
        }
    }
}
