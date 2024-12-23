using System.ComponentModel.DataAnnotations;

namespace Domain.ViewModel.LoginAndRegistration;

public class RegisterViewModel
{
    [Required(ErrorMessage = "Укажите логин")]
    [MaxLength(20, ErrorMessage = "Логин должен быть не более 20 символов")]
    [MinLength(3, ErrorMessage = "Логин должен быть не менее 3 символов")]
    public string Login { get; set; }
    
    
    [Required(ErrorMessage = "Укажите имя 3-20 символов")]
    [MaxLength(40, ErrorMessage = "Почта должно иметь не более 40 символов")]
    [MinLength(3, ErrorMessage = "Почта должно иметь более 3 символов")]
    [RegularExpression(
        @"^[a-zA-Z0-9._-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,4}$",
        ErrorMessage = "Ввод некорректными символами")]

    public string Email { get; set; }

    [DataType(DataType.Password)]
    [Required(ErrorMessage = "Укажите пароль")]
    [MinLength(6, ErrorMessage = "Пароль должен содержать не менее 6 символов")]

    public string Password { get; set; }

    [DataType(DataType.Password)]
    [Required(ErrorMessage = "Подтвердите пароль")]
    [Compare("Password", ErrorMessage = "Пароли не совпадают")]

    public string PasswordConfirm { get; set; }
}