using CashBox.Core.DTOs.IncomeDTOs;
using FluentValidation;

namespace CashBox.Service.Validators;

public class IncomeCreateDtoValidator : AbstractValidator<IncomeCreateDto>
{
    public IncomeCreateDtoValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty().WithMessage("Başlık alanı boş geçilemez.")
            .NotNull().WithMessage("Başlık alanı zorunludur.")
            .MaximumLength(150).WithMessage("Başlık en fazla 150 karakter olabilir.");

        RuleFor(x => x.Amount)
            .GreaterThan(0).WithMessage("Gelir tutarı 0'dan büyük olmalıdır.");
    }
}
