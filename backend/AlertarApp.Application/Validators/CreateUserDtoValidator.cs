using AlertarApp.Application.DTOs.User;
using FluentValidation;

namespace AlertarApp.Application.Validators
{
    public class CreateUserDtoValidator : AbstractValidator<CreateUserDto>
    {
        public CreateUserDtoValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Nome é obrigatório")
                .MaximumLength(100).WithMessage("Nome deve ter no máximo 100 caracteres");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email é obrigatório")
                .MaximumLength(100).WithMessage("Email deve ter no máximo 100 caracteres")
                .EmailAddress().WithMessage("Email inválido");

            RuleFor(x => x.Phone)
                .NotEmpty().WithMessage("Telefone é obrigatório")
                .MaximumLength(20).WithMessage("Telefone deve ter no máximo 20 caracteres")
                .Matches(@"^\(\d{2}\)\s?\d{4,5}-\d{4}$").WithMessage("Telefone inválido. Use o formato (XX) XXXXX-XXXX");

            RuleFor(x => x.DocumentId)
                .NotEmpty().WithMessage("CPF é obrigatório")
                .MaximumLength(14).WithMessage("CPF deve ter no máximo 14 caracteres")
                .Must(BeValidCpf).WithMessage("CPF inválido");

            RuleFor(x => x.Pin)
                .NotEmpty().WithMessage("PIN é obrigatório")
                .Length(4, 6).WithMessage("PIN deve ter entre 4 e 6 caracteres")
                .Matches(@"^\d+$").WithMessage("PIN deve conter apenas números");
        }

        private bool BeValidCpf(string cpf)
        {
            // Implementação básica de validação de CPF
            // Em produção, usar uma validação mais completa
            cpf = cpf.Replace(".", "").Replace("-", "");
            
            if (cpf.Length != 11)
                return false;
            
            // Verifica se todos os dígitos são iguais, o que é inválido
            bool allDigitsEqual = true;
            for (int i = 1; i < cpf.Length; i++)
            {
                if (cpf[i] != cpf[0])
                {
                    allDigitsEqual = false;
                    break;
                }
            }
            
            if (allDigitsEqual)
                return false;
                
            // Validação simplificada para exemplo
            return true;
        }
    }
}
