using Domain.Filter;
using Domain.Models;
using Domain.Response;

namespace Service.oyi;

public interface IJobsService
{
    BaseResponse<List<Jobs>> GetAllJobsByIdCategories(Guid Id);
    
    BaseResponse<List<Jobs>> GetTourByFilter(Filter filter);
}