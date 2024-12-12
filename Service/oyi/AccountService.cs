using System.Security.Claims;
using AutoMapper;
using DAL.Interface;
using DAL.Storage;
using Domain.Enum;
using Domain.Helpers;
using Domain.Models;
using Domain.ModelsDb;
using Domain.Response;
using Domain.Validators;
using FluentValidation;
using MailKit.Net.Smtp;
using Microsoft.EntityFrameworkCore;
using MimeKit;
using Service.Model;


namespace Service.oyi;

public class AccountService : IAccountService
{   
    private readonly IBaseStorage<UserDb> _userStorage;
    
    private IMapper _mapper { get; set; }
    
    private UserValidator _validationRules { get; set; }

    private MapperConfiguration mapperConfiguration = new MapperConfiguration(p =>
    {
        p.AddProfile<AppMappingProfile>();
    });
    
    public AccountService(IBaseStorage<UserDb> userStorage)
    {
        _userStorage = userStorage;
        _mapper = mapperConfiguration.CreateMapper();
        _validationRules = new UserValidator();
    }

    public async Task<BaseResponse<ClaimsIdentity>> Login(User model)
    {
        try
        {
            await _validationRules.ValidateAndThrowAsync(model);

            var userDb = await _userStorage.GetAll().FirstOrDefaultAsync(x => x.Email == model.Email);

            if (userDb == null)
            {
                return new BaseResponse<ClaimsIdentity>()
                {
                    Description = "Пользователь не найден"
                };
            }

            if (userDb.Password != HashPasswordHelper.HashPassword(model.Password))
            {
                return new BaseResponse<ClaimsIdentity>()
                {
                    Description = "Неверный пароль или почта"
                };
            }

            var result = AuthenticateUserHelper.Authenticate(userDb);

            return new BaseResponse<ClaimsIdentity>()
            {
                Data = result,
                StatusCode = StatusCode.Ok
            };

        }
        catch(ValidationException ex)
        {
            var errorMesage = string.Join(";", ex.Errors.Select(e => e.ErrorMessage));
            return new BaseResponse<ClaimsIdentity>()
            {
                Description = errorMesage,
                StatusCode = StatusCode.BadRequest
            };
        }
        
        catch (Exception ex)
        {
            return new BaseResponse<ClaimsIdentity>()
            {
                Description = ex.Message,
                StatusCode = StatusCode.InternalServerError
            };
        }
        
    }

    public async Task<BaseResponse<string>> Register(User model)
    {
        try
        {
            Random random = new Random();
            string confirmationCode = $"{random.Next(10)}{random.Next(10)}{random.Next(10)}{random.Next(10)}";

            if (await _userStorage.GetAll().FirstOrDefaultAsync(x => x.Email == model.Email) != null)
            {
                return new BaseResponse<string>()
                {
                    Description = "Пользователь с такой почтой уже есть"
                };
            }

            await SendEmail(model.Email, confirmationCode);

            return new BaseResponse<string>()
            {
                Data = confirmationCode,
                Description = "Письмо отправлено",
                StatusCode = StatusCode.Ok
            };

        }
        catch (ValidationException ex)
        {
            var errorMessage = string.Join(";", ex.Errors.Select(e => e.ErrorMessage));
            return new BaseResponse<string>()
            {
                Description = errorMessage,
                StatusCode = StatusCode.BadRequest
            };
        }
        catch (Exception ex)
        {
            return new BaseResponse<string>()
            {
                Description = ex.Message,
                StatusCode = StatusCode.InternalServerError
            };
        }

    }
     public async Task SendEmail(string email, string confirmationCode)
    {
        string path = "C:\\313RN\\pass.txt";
        var emailMessage = new MimeMessage();

        emailMessage.From.Add(new MailboxAddress("Администрация сайта", "UP_kwork.ru"));
        emailMessage.To.Add(new MailboxAddress("", email));
        emailMessage.Subject = "Добро пожаловать!";
        emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html)
        {
            Text = "<html>" +
                   "<head>" +
                   "<style>" +
                   "body { font-family: Arial, sans-serif; background-color: #f2f2f2; }" +
                   "0.container { max-width: 80%; margin: 0 auto; padding: 20px; background-color: #fff; border-radius: 10px; box-shadow: 0px 0px 10px rgba(0,0,0,0.1); }" +
                   ".header { text-align: center; margin-bottom: 20px; }" +
                   ".message { font-size: 16px; line-height: 1.6; }" +
                   ".container .code { background-color: #f0f0f0; padding: 5px; border-radius: 5px; font-weight: bold; text-align: center; }" +
                   "</style>" +
                   "</head>" +
                   "<body>" +
                   "<div class='container'>" +
                   "<div class='header'><h1>Добро пожаловать на сайт UP_kwork!</h1></div>" +
                   "<div class='message'>" +
                   "<p>Пожалуйста, введите данный код на сайте, чтобы подтвердить ваш email и завершить регистрацию:</p>" +
                   "</div>" +
                   "<div class='container code'><p class='code'>" + confirmationCode + "</p></div>" +
                   "</div>" +
                   "</body>" +
                   "</html>"
        };

        using (StreamReader reader = new StreamReader(path))
        {
            string password = await reader.ReadToEndAsync();

            using (var client = new SmtpClient())
            {
                await client.ConnectAsync("smtp.gmail.com", 465, true);
                await client.AuthenticateAsync("rzavtonev@gmail.com", password);
                await client.SendAsync(emailMessage);

                await client.DisconnectAsync(true);
            }
        }
    }
    public async Task<BaseResponse<ClaimsIdentity>> ConfirmEmail(User model, string code, string confirmCode)
    {
        try
        {
            if (code != confirmCode)
            {
                throw new Exception("Неверный код! Регистрация не выполнена.");
            }

            model.CreatedAt = DateTime.Now;
            model.Password = HashPasswordHelper.HashPassword(model.Password);

            await _validationRules.ValidateAndThrowAsync(model);
            var userDb = _mapper.Map<UserDb>(model);
            await _userStorage.Add(userDb);
            var result = AuthenticateUserHelper.Authenticate(userDb);

            return new BaseResponse<ClaimsIdentity>()
            {
                Data = result,
                Description = "Объект добавился",
                StatusCode = StatusCode.Ok
            };

        }
        catch (Exception ex)
        {
            return new BaseResponse<ClaimsIdentity>()
            {
                Description = ex.Message,
                StatusCode = StatusCode.InternalServerError
            };
        }
    }

    public async Task<BaseResponse<ClaimsIdentity>> IsCreatedAccount(User model)
    {
        try
        {
            var userDb = new UserDb();
            if (await _userStorage.GetAll().FirstOrDefaultAsync(x => x.Email == model.Email) == null)
            {
                model.Password = "google";
                model.CreatedAt = DateTime.Now;

                userDb = _mapper.Map<UserDb>(model);

                await _userStorage.Add(userDb);

                var resultRegister = AuthenticateUserHelper.Authenticate(userDb);
                return new BaseResponse<ClaimsIdentity>()
                {
                    Data = resultRegister,
                    Description = "Объект добавлен",
                    StatusCode = StatusCode.Ok
                };
            }
            userDb = _mapper.Map<UserDb>(model);

            var resultLogin = AuthenticateUserHelper.Authenticate(userDb);
            return new BaseResponse<ClaimsIdentity>()
            {
                Data = resultLogin,
                Description = "Объект уже был создан",
                StatusCode = StatusCode.Ok
            };
        }
        catch (Exception ex)
        {
            return new BaseResponse<ClaimsIdentity>()
            {
                Description = ex.Message,
                StatusCode = StatusCode.InternalServerError,
            };
        }
    }
    
}