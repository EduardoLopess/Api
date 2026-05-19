using Application.UseCase.ProductUseCase.CreateUseCase.Request;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.UseCase.ProductUseCase.CreateUseCase.Validation
{
    public class RegisterProductBaseValidation : AbstractValidator<RegisterProductBaseRequestDTO>
    {
        public RegisterProductBaseValidation() 
        {
            
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("O nome do produto é obrigatório.")
                .MaximumLength(80).WithMessage("O nome deve ter no máximo 80 caracteres.");

           
            RuleFor(x => x.Price)
                .NotEmpty().WithMessage("Preço do produto é obrigatório.")
                .GreaterThan(0).WithMessage("Preço deve ser maior que zero.")
                .PrecisionScale(7, 2, false).WithMessage("Preço inválido. Use o formato correto com até 2 casas decimais (ex: 1500.00).");


            RuleFor(x => x.CategoryId)
                .NotEmpty().WithMessage("A categoria é obrigatória.");


            // Validação do Tipo (Garante que é um GUID válido)
            RuleFor(x => x.TypeId)
                .NotEmpty().WithMessage("O tipo é obrigatório.");


            // Validação do Subtipo (Garante que é um GUID válido)
            RuleFor(x => x.SubTypeId)
                .NotEmpty().WithMessage("O subtipo é obrigatório.");
                

            // Validação da Quantidade em Estoque
            RuleFor(x => x.QuantityStock)
                .GreaterThanOrEqualTo(0).WithMessage("A quantidade em estoque não pode ser negativa.");

            // Validação do ENUM de Disponibilidade
            RuleFor(x => x.Availability)
                .IsInEnum().WithMessage("Disponibilidade inválida.");
        }

        
    
    
    }
}
