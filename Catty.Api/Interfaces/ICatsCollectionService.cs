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
