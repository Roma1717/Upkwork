using Domain.Models;
using Domain.Response;

namespace Service.oyi;

public interface ICategoriesService
{
    BaseResponse<List<Categories>> GetAllCountries();
}