using System.Diagnostics;
using System.Security.Claims;
using AutoMapper;
using Domain.Filter;
using Domain.Models;
using Domain.ViewModel;
using Domain.ViewModel.LoginAndRegistration;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Mvc;
using Portfol.Models;
using Service.Model;
using Service.oyi;

namespace Portfol.Controllers;

    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        
        private readonly ICategoriesService _categoriesService;

        private readonly IJobsService _jobsService;
        
        private readonly IAccountService _accountService;

        public IActionResult Index()
        {
            return View();
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        private IMapper _mapper { get; set; }

        private MapperConfiguration mapperConfiguration = new MapperConfiguration(p =>
        {
            p.AddProfile<AppMappingProfile>();
        });
        //

        private readonly IWebHostEnvironment _appEnvironment;


        public HomeController(ILogger<HomeController> logger, IAccountService accountService,
            IWebHostEnvironment appEnvironment, ICategoriesService categoriesService,IJobsService jobsService )
        {
            _accountService = accountService;
            _categoriesService = categoriesService;
            _jobsService = jobsService;
            _logger = logger;
            _mapper = mapperConfiguration.CreateMapper();
            _appEnvironment = appEnvironment;
        }

        
        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = _mapper.Map<User>(model);

                var response = await _accountService.Login(user);

                if (response.StatusCode == Domain.Enum.StatusCode.Ok)
                {
                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                        new ClaimsPrincipal(response.Data));

                    return Ok(model);
                }

                ModelState.AddModelError("", response.Description);
            }

            var errors = ModelState.Values.SelectMany(v => v.Errors)
                .Select(e => e.ErrorMessage)
                .ToList();
            return BadRequest(errors);
        }

        [HttpPost]
        public async Task<IActionResult> Register([FromBody] RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = _mapper.Map<User>(model);

                var confirm = _mapper.Map<ConfirmEmailViewModel>(model);

                var code = await _accountService.Register(user);

                confirm.GeneratedCode = code.Data;

                return Ok(confirm);
            }

            var errors = ModelState.Values.SelectMany(v => v.Errors)
                .Select(e => e.ErrorMessage)
                .ToList();

            return BadRequest(errors);
        }

        [HttpPost]
        public async Task<IActionResult> ConfirmEmail([FromBody] ConfirmEmailViewModel confirmEmailViewModel)
        {
            var user = _mapper.Map<User>(confirmEmailViewModel);

            var response = await _accountService.ConfirmEmail(user, confirmEmailViewModel.GeneratedCode,
                confirmEmailViewModel.CodeConfirm);

            if (response.StatusCode == Domain.Enum.StatusCode.Ok)
            {
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(response.Data));
                return Ok(confirmEmailViewModel);
            }

            ModelState.AddModelError("", response.Description);

            var errors = ModelState.Values.SelectMany(v => v.Errors)
                .Select(e => e.ErrorMessage)
                .ToList();

            return BadRequest(errors);
        }

        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
        }

        //
        public async Task AuthenticationGoogle(string returnUrl = "/") // По умолчанию возвращаемся на главную
        {
            await HttpContext.ChallengeAsync(GoogleDefaults.AuthenticationScheme,
                new AuthenticationProperties
                {
                    RedirectUri = Url.Action("GoogleResponse", new { returnUrl }), // Передаем returnUrl
                    Parameters = { { "prompt", "select_account" } }
                });
        }

        public async Task<IActionResult> GoogleResponse(string returnUrl = "/")
        {
            try
            {
                var result = await HttpContext.AuthenticateAsync(CookieAuthenticationDefaults.AuthenticationScheme);

                if (result?.Succeeded == true)
                {
                    User model = new User
                    {
                        Login = result.Principal.FindFirst(ClaimTypes.Name)?.Value,
                        Email = result.Principal.FindFirst(ClaimTypes.Email)?.Value,

                    };

                    var response = await _accountService.IsCreatedAccount(model);

                    if (response.StatusCode == Domain.Enum.StatusCode.Ok)
                    {
                        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                            new ClaimsPrincipal(response.Data));
                        return Redirect(returnUrl);
                    }
                }

                return BadRequest("Аутентификация не удалась");
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }
        public IActionResult ListOfCountries()
        {
            var result = _categoriesService.GetAllCountries();
            var listOfCountry = _mapper.Map<List<CategoriesViewModel>>(result.Data);
        
            return View(listOfCountry);
        }

        public IActionResult ListOfJobs(Guid Id)
        {
            var result = _jobsService.GetAllJobsByIdCategories(Id);
            var listJobs = new ListOfJobsViewModel
            {
                Jobs = _mapper.Map<List<JobsForListOfJobsViewModel>>(result.Data),
                Category_id = Id
            };

            return View(listJobs);
        }
        [HttpPost]
        public async Task<IActionResult> ListOfJobs([FromBody] Filter filter)
        {
            try
            {
                var result = _jobsService.GetTourByFilter(filter);
                var filteredJobs = _mapper.Map<List<ListOfJobsViewModel>>(result.Data);
                return Json(filteredJobs);  // Возвращаем данные в формате JSON
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });  // Возвращаем ошибку в формате JSON
            }
        }

    }
