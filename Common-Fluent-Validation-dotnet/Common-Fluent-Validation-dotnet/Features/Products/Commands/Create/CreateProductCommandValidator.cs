﻿using FluentValidation;

namespace Common_Fluent_Validation_dotnet.Features.Products.Commands.Create
{
    public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
    {
        public CreateProductCommandValidator()
        {
            RuleFor(p => p.Name).NotEmpty().MinimumLength(4);
            RuleFor(p => p.Price).GreaterThan(0);
        }
    }
}
