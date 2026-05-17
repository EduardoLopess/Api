using Application.DTOs.EmployeeDTO.Request;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Validation
{
    public class RegisterEmployeeValidator : AbstractValidator<RegisterEmployeeRequestDTO>
    {
        public RegisterEmployeeValidator() 
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("O nome é obrigatório.")
                .Matches(@"^[a-zA-ZÀ-ÿ\s]+$").WithMessage("Apenas letras são permitidas.");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Senha é obrigatória.")
                .MinimumLength(8);

            RuleFor(x => x.ConfirmPassword)
                .Equal(x => x.Password)
                .WithMessage("As senhas não conferem.");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email é obrigatório.");
        
        }
    }
}
