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
