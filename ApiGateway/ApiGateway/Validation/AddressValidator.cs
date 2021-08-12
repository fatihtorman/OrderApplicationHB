using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entity.Validation
{
    public class AddressValidator : AbstractValidator<Address>
    {
        public AddressValidator()
        {
            //RuleFor(Address => Address.Id)
            //.GreaterThan(0).WithMessage("Id alanı boş geçilemez");

            RuleFor(Address => Address.AddressLine)
            .NotEmpty().WithMessage("Açık adres boş geçilemez!");

            RuleFor(Address => Address.City)
            .NotEmpty().WithMessage("City alanı boş geçilemez!");

            RuleFor(Address => Address.Country)
            .NotEmpty().WithMessage("Country alanı boş geçilemez!");

            RuleFor(Address => Address.CityCode)
            .GreaterThan(0).WithMessage("İl kodu gereklidir!");
        }
    }
}
