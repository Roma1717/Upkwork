using DAL.Interface;
using DAL.Storage;
using Domain.ModelsDb;
using Service.oyi;


namespace In;

public static class Initializer
{
    public static void InitializeRepositories(this IServiceCollection services)
    {
        services.AddScoped<IBaseStorage<UserDb>, UserStorage>();
        services.AddScoped<IBaseStorage<CategoriesDb>, CategoriesStorage>();
        services.AddScoped<IBaseStorage<JobsDb>, JobsStorage>();

    }

    public static void InitializeServices(this IServiceCollection services)
    {
        services.AddScoped<IAccountService, AccountService>();
        services.AddScoped<ICategoriesService, CategoriesService>();
        services.AddScoped<IJobsService, JobsService>();

        services.AddControllersWithViews()
            .AddDataAnnotationsLocalization()
            .AddViewLocalization();
    }
}