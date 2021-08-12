using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entity.Validation
{
    public class OrderValidator : AbstractValidator<Order>
    {
        public OrderValidator()
        {
            //RuleFor(Order => Order.Id)
            //.GreaterThan(0).WithMessage("Sipariş Id boş!");

            RuleFor(Order => Order.Customer).SetValidator(new CustomerValidator());

            RuleFor(Order => Order.Address).SetValidator(new AddressValidator());

            RuleFor(Order => Order.Product).SetValidator(new ProductValidator());

            RuleFor(Order => Order.Quantity)
            .GreaterThan(0).WithMessage("Adet girilmemiş");

            RuleFor(Order => Order.Status)
            .NotEmpty().WithMessage("Sipariş durumu geçersiz");

        }
    }
}
