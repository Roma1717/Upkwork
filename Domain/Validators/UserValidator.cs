using Domain.Models;
using FluentValidation;

namespace Domain.Validators;

public class UserValidator : AbstractValidator<User>
{
    public UserValidator()
    {
        RuleFor(user => user.Password).NotEmpty().WithMessage("Пароль обязателен")
            .MinimumLength(6).WithMessage("Пароль должен содержать не менее 6 символов");
        RuleFor(user => user.Email).NotEmpty().WithMessage("Почта обязательна")
            .EmailAddress().WithMessage("Неверный формат почты");
    }
}