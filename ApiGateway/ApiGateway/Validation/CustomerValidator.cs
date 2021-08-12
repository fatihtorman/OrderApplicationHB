using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entity.Validation
{
    public class CustomerValidator : AbstractValidator<Customer>
    {
        public CustomerValidator()
        {
            //RuleFor(customer => customer.Id)
            //.GreaterThan(0).WithMessage("Id alanı 0'dan büyük olmalıdır!");

            RuleFor(customer => customer.Name)
            .NotEmpty().WithMessage("İsim alanı boş geçilemez!");

            RuleFor(customer => customer.Email)
            .EmailAddress()
            .WithMessage("Geçerli bir e-posta değeri giriniz!").When(i => !string.IsNullOrEmpty(i.Email));

            RuleFor(customer => customer.Address).SetValidator(new AddressValidator());
        }

    }
}
