using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entity.Validation
{
    public class ProductValidator : AbstractValidator<Product>
    {
        public ProductValidator()
        {
            RuleFor(Product => Product.Id)
            .GreaterThan(0).WithMessage("ÜrünId'si boş!");

            RuleFor(Product => Product.ImageUrl)
            .NotEmpty().WithMessage("ImageUrl boş gönderilemez!");

            RuleFor(Product => Product.Name)
            .NotEmpty().WithMessage("Ürün ismi boş geçilemez!");
        }
    }
}
