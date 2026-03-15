using CashBox.Core.DTOs.UserDTOs; // DTO'muzun yeni adresini buraya ekledik!
using FluentValidation;

namespace CashBox.Service.Validations;

public class UserCreateDtoValidator : AbstractValidator<UserCreateDto>
{
    public UserCreateDtoValidator()
    {
        RuleFor(x => x.FirstName)
            .NotEmpty().WithMessage("Ad alanı boş bırakılamaz!")
            .MaximumLength(50).WithMessage("Ad alanı en fazla 50 karakter olabilir.");

        RuleFor(x => x.LastName)
            .NotEmpty().WithMessage("Soyad alanı boş bırakılamaz!")
            .MaximumLength(50).WithMessage("Soyad alanı en fazla 50 karakter olabilir.");

        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("E-posta alanı zorunludur!")
            .EmailAddress().WithMessage("Lütfen geçerli bir e-posta formatı giriniz! (Örn: test@test.com)");

        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Şifre alanı zorunludur!")
            .MinimumLength(6).WithMessage("Şifre en az 6 karakterden oluşmalıdır.");
    }
}
