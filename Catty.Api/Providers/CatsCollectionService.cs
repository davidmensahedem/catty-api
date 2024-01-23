using Catty.Api.Interfaces;
using Catty.Api.Models.Filters;
using Catty.Api.Models.Requests;
using Catty.Api.Models.Responses;
using Catty.Core.Models.Responses;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Cats.Api.Models;

namespace Catty.Api.Providers
{
    public class CatsCollectionService : ICatsCollectionService
    {
        private readonly ICatsCollectionRepository _catsCollectionRepository;
        public CatsCollectionService(ICatsCollectionRepository catsCollectionRepository)
        {
            _catsCollectionRepository = catsCollectionRepository;
        }

        public async Task<ApiResponse<List<CatResponse>>> GetAllCats(CatFilter filter)
        {
            var cats = await _catsCollectionRepository.GetAllCatsAsync(filter);

            return new ApiResponse<List<CatResponse>>
            {
                Code = $"{StatusCodes.Status200OK}",
                Message = "Successful",
                Data = cats
            };
        }
        public async Task<ApiResponse<CatResponse>> GetCat(string id)
        {
            var cat = await _catsCollectionRepository.GetCatAsync(id);

            if (cat == null) return new ApiResponse<CatResponse>
            {
                Code = $"{StatusCodes.Status400BadRequest}",
                Message = "Cat not found"
            };

            return new ApiResponse<CatResponse>
            {
                Code = $"{StatusCodes.Status200OK}",
                Message = "Successful",
                Data = cat
            };
        }
        public async Task<ApiResponse<CatResponse>> AddCat(AddCatRequest request)
        {
            var cat = await _catsCollectionRepository.AddCat(request);

            if (cat == null) return new ApiResponse<CatResponse>
            {
                Code = $"{StatusCodes.Status400BadRequest}",
                Message = "Could not add cat."
            };

            return new ApiResponse<CatResponse>
            {
                Code = $"{StatusCodes.Status200OK}",
                Message = "Successful",
                Data = cat
            };
        }
        public async Task<ApiResponse<string>> UpdateCat(UpdateCatRequest request)
        {
            var result = await _catsCollectionRepository.UpdateCatSpeciality(request);

            if (!result) return new ApiResponse<string>
            {
                Code = $"{StatusCodes.Status400BadRequest}",
                Message = "Update was unsuccessful"
            };

            return new ApiResponse<string>
            {
                Code = $"{StatusCodes.Status200OK}",
                Message = "Successful"
            };
        }
        public async Task<ApiResponse<string>> DeleteCat(string id)
        {
            var result = await _catsCollectionRepository.DeleteCat(id);

            if (!result) return new ApiResponse<string>
            {
                Code = $"{StatusCodes.Status400BadRequest}",
                Message = "Delete was unsuccessful"
            };

            return new ApiResponse<string>
            {
                Code = $"{StatusCodes.Status200OK}",
                Message = "Successful"
            };
        }
    }
}


