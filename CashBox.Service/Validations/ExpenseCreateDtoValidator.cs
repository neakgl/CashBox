using CashBox.Core.DTOs.ExpenseDTOs;
using FluentValidation;

namespace CashBox.Service.Validations;

public class ExpenseCreateDtoValidator : AbstractValidator<ExpenseCreateDto>
{
    public ExpenseCreateDtoValidator()
    {
        RuleFor(x => x.Description)
            .NotEmpty().WithMessage("Açıklama alanı boş geçilemez.")
            .NotNull().WithMessage("Açıklama alanı zorunludur.")
            .MaximumLength(200).WithMessage("Açıklama en fazla 200 karakter olabilir.");

        RuleFor(x => x.Amount)
            .GreaterThan(0).WithMessage("Harcama tutarı 0'dan büyük olmalıdır.");

        RuleFor(x => x.CategoryId)
            .GreaterThan(0).WithMessage("Geçerli bir kategori seçilmelidir.");
    }
}
