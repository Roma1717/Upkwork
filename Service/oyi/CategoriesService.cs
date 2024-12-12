using AutoMapper;
using DAL.Interface;
using Domain.Enum;
using Domain.Models;
using Domain.ModelsDb;
using Domain.Response;
using Service.Model;

namespace Service.oyi;

public class CategoriesService : ICategoriesService
{
    
    private readonly IBaseStorage<CategoriesDb> _categoriesStorage;
    private IMapper _mapper { get; set; }
   
    MapperConfiguration mapperConfiguration = new MapperConfiguration(p =>
    {
        p.AddProfile<AppMappingProfile>();
    });


    public CategoriesService(IBaseStorage<CategoriesDb> categoriesStorage)
    {
        _categoriesStorage = categoriesStorage;
        _mapper = mapperConfiguration.CreateMapper();
    }
    public BaseResponse<List<Categories>> GetAllCountries()
    {
        try
        {
            var categoriesDb = _categoriesStorage.GetAll().OrderBy(p => p.CreatedAt).ToList();
            var result = _mapper.Map<List<Categories>>(categoriesDb);

            if (result.Count == 0)
            {
                return new BaseResponse<List<Categories>>()
                {
                    Description = "Найдено 0 элементов",
                    StatusCode = StatusCode.Ok
                };
            }

            return new BaseResponse<List<Categories>>()
            {
                Data = result,
                StatusCode = StatusCode.Ok
            };
        }
        catch (Exception ex)
        {
            return new BaseResponse<List<Categories>>()
            {
                Description = ex.Message,
                StatusCode = StatusCode.InternalServerError
            };
        }
    }
}