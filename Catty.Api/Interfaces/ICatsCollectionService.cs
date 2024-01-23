using MongoDB.Cats.Api.Models;
using Catty.Api.Models.Filters;
using Catty.Api.Models.Requests;
using Catty.Api.Models.Responses;
using Catty.Core.Models.Responses;

namespace Catty.Api.Interfaces
{
    public interface ICatsCollectionService
    {
        Task<ApiResponse<List<CatResponse>>> GetAllCats(CatFilter filter);
        Task<ApiResponse<CatResponse>> GetCat(string id);
        Task<ApiResponse<CatResponse>> AddCat(AddCatRequest request);
        Task<ApiResponse<string>> UpdateCat(UpdateCatRequest request);
        Task<ApiResponse<string>> DeleteCat(string id);
    }
}
