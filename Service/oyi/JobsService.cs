using AutoMapper;
using DAL.Interface;
using Domain.Enum;
using Domain.Filter;
using Domain.Models;
using Domain.ModelsDb;
using Domain.Response;
using Service.Model;

namespace Service.oyi;

public class JobsService : IJobsService
{
    private readonly IBaseStorage<JobsDb> _jobsStorage;
    
    private IMapper _mapper { get; set; }
    MapperConfiguration mapperConfiguration = new MapperConfiguration(p =>
    {
        p.AddProfile<AppMappingProfile>();
    });

    public JobsService(IBaseStorage<JobsDb> jobsStorage)
    {
        _jobsStorage = jobsStorage;
        _mapper = mapperConfiguration.CreateMapper();
    }

    public BaseResponse<List<Jobs>> GetAllJobsByIdCategories(Guid Id)
    {
        try
        {
            var jobsDb = _jobsStorage.GetAll().Where(x => Id == x.Category_id).ToList();

            var result = _mapper.Map<List<Jobs>>(jobsDb);
            if (result.Count == 0)
            {
                return new BaseResponse<List<Jobs>>()
                {
                    Description = "Найдено 0 элементов",
                    StatusCode = StatusCode.Ok
                };
            }

            return new BaseResponse<List<Jobs>>()
            {
                Data = result,
                StatusCode = StatusCode.Ok
            };
        }
        catch (Exception ex)
        {
            return new BaseResponse<List<Jobs>>()
            {
                Description = ex.Message,
                StatusCode = StatusCode.InternalServerError
            };
        }
    }
    public BaseResponse<List<Jobs>> GetTourByFilter(Filter filter)
    {
        try
        {
            var master_classesFilter = GetAllJobsByIdCategories(filter.CategoryId).Data;

            if (filter != null && master_classesFilter != null)
            {
                if (filter.PriceAdultMax != 000 || filter.PriceAdultMin != 0)
                {
                    master_classesFilter = master_classesFilter.Where(f => f.salary <= filter.PriceAdultMax && f.salary >= filter.PriceAdultMin).ToList();
                }
            }

            return new BaseResponse<List<Jobs>>
            {
                Data = master_classesFilter,
                Description = "Отфильтрованные данные",
                StatusCode = StatusCode.Ok
            };
        }
        catch (Exception ex)
        {
            return new BaseResponse<List<Jobs>>
            {
                Description = ex.Message,
                StatusCode = StatusCode.InternalServerError
            };
        }
    }
}