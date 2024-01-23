using MongoDB.Cats.Api.Models;
using Catty.Api.Models.Filters;
using Catty.Api.Models.Requests;
using Catty.Api.Models.Responses;

namespace Catty.Api.Interfaces
{
    public interface ICatsCollectionRepository
    {
        Task<List<CatResponse>> GetAllCatsAsync(CatFilter filter);
        Task<CatResponse> GetCatAsync(string id);
        Task<CatResponse> AddCat(AddCatRequest request);
        Task<bool> UpdateCatSpeciality(UpdateCatRequest request);
        Task<bool> DeleteCat(string id);
    }
}
