using Catty.Api.Extensions;
using Catty.Api.Interfaces;
using Catty.Api.Models.Filters;
using Catty.Api.Models.Requests;
using Catty.Api.Models.Responses;
using Mapster;
using MongoDB.Bson;
using MongoDB.Cats.Api.Models;
using MongoDB.Driver;

namespace Catty.Api.Providers
{
    public class CatsCollectionRepository : ICatsCollectionRepository
    {
        private readonly IMongoCollection<Cat> _catsCollection;
        private readonly ILogger<CatsCollectionRepository> _logger;

        public CatsCollectionRepository(
            IMongoCollection<Cat> catsCollection,
            ILogger<CatsCollectionRepository> logger
            )
        {
            _catsCollection = catsCollection;
            _logger = logger;
        }

        public async Task<List<CatResponse>> GetAllCatsAsync(CatFilter filter)
        {
            try
            {
                _logger.LogDebug("Inside service: {Service} - method: {Method}, about to get all cats with filter: {Filter} from the database.",
                    nameof(CatsCollectionRepository), nameof(GetAllCatsAsync), filter.Serialize());

                var cats = await _catsCollection.FindAsync(_ => true).Result.ToListAsync();

                if (!string.IsNullOrWhiteSpace(filter.Type)) cats.Where(c => c.Type!.Equals(filter.Type));

                if (!string.IsNullOrWhiteSpace(filter.Name)) cats.Where(c => c.Name!.Equals(filter.Name));

                if (!cats.Any())
                {
                    _logger.LogDebug("Inside service: {Service} - method: {Method}, cat not available with the provided details: {Filter}.",
                    nameof(CatsCollectionRepository), nameof(GetAllCatsAsync), filter.Serialize());

                    return new List<CatResponse>();
                }

                return cats.Select(c => c.Adapt<CatResponse>()).ToList();
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Inside service: {Service} - method: {Method}, an error occured while getting cat(s) with the provided details: {Filter}.",
                    nameof(CatsCollectionRepository), nameof(GetAllCatsAsync), filter.Serialize());

                return new List<CatResponse>();
            }
        }

        public async Task<CatResponse> GetCatAsync(string id)
        {
            try
            {
                _logger.LogDebug("Inside service: {Service} - method: {Method}, about to get the cat with id: {Id} from the database.",
                    nameof(CatsCollectionRepository), nameof(GetCatAsync), id);

                var cat = await _catsCollection.FindAsync(cat => cat.Id.Equals(new ObjectId(id))).Result.FirstOrDefaultAsync();

                if (cat == null)
                {
                    _logger.LogDebug("Inside service: {Service} - method: {Method}, cat not available with the provided id: {Id}.",
                    nameof(CatsCollectionRepository), nameof(GetCatAsync), id);

                    return null;
                }

                return cat.Adapt<CatResponse>();
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Inside service: {Service} - method: {Method}, an error occured while getting cat with the provided id: {Id}.",
                    nameof(CatsCollectionRepository), nameof(GetCatAsync), id);

                return null;
            }
        }

        public async Task<CatResponse> AddCat(AddCatRequest request)
        {
            try
            {
                _logger.LogDebug("Inside service: {Service} - method: {Method}, about to add the cat with details: {Cat} to the database.",
                    nameof(CatsCollectionRepository), nameof(AddCat), request.Serialize());

                var cat = request.Adapt<Cat>();

                await _catsCollection.InsertOneAsync(cat);

                _logger.LogDebug("Inside service: {Service} - method: {Method}, added cat with details: {Cat} to the database.",
                   nameof(CatsCollectionRepository), nameof(AddCat), cat.Serialize());

                return cat.Adapt<CatResponse>();
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Inside service: {Service} - method: {Method}, an error occured while adding the cat with the provided details: {Request}.",
                    nameof(CatsCollectionRepository), nameof(AddCat), request.Serialize());

                return null;
            }
        }

        public async Task<bool> UpdateCatSpeciality(UpdateCatRequest request)
        {
            try
            {
                _logger.LogDebug("Inside service: {Service} - method: {Method}, about to update the cat with details: {Request} inside the database.",
                    nameof(CatsCollectionRepository), nameof(UpdateCatSpeciality), request.Serialize());

                var updateDefinition = Builders<Cat>.Update.AddToSetEach(cat => cat.Specialities, request.Specialities);

                var updateResult = await _catsCollection.UpdateOneAsync(cat => cat.Id == new ObjectId(request.Id), updateDefinition);

                if (!updateResult.IsAcknowledged)
                {
                    _logger.LogDebug("Inside service: {Service} - method: {Method}, could not update the cat with details: {Request} inside the database.",
                    nameof(CatsCollectionRepository), nameof(UpdateCatSpeciality), request.Serialize());

                    return false;

                }

                _logger.LogDebug("Inside service: {Service} - method: {Method}, successfully updated the cat with details: {Request} inside the database. \nRecord(s) modified: {Modified} and match count: {MatchCount}",
                   nameof(CatsCollectionRepository), nameof(UpdateCatSpeciality), request.Serialize(), updateResult.ModifiedCount, updateResult.MatchedCount);

                return true;
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Inside service: {Service} - method: {Method}, an error occured while updating the cat with the provided details: {Request}.",
                    nameof(CatsCollectionRepository), nameof(AddCat), request.Serialize());

                return false;
            }
        }

        public async Task<bool> DeleteCat(string id)
        {
            try
            {
                _logger.LogDebug("Inside service: {Service} - method: {Method}, about to delete the cat with id: {Id} from the database.",
                    nameof(CatsCollectionRepository), nameof(DeleteCat), id);

                var deleteResult = await _catsCollection.DeleteOneAsync(cat => cat.Id == new ObjectId(id));

                if (!deleteResult.IsAcknowledged)
                {
                    _logger.LogDebug("Inside service: {Service} - method: {Method}, could not delete the cat with id: {Id} from the database.",
                    nameof(CatsCollectionRepository), nameof(DeleteCat), id);

                    return false;
                }

                _logger.LogDebug("Inside service: {Service} - method: {Method}, successfully deleted the cat with id: {Id} inside the database.\nDelete count: {DeleteCount}",
                   nameof(CatsCollectionRepository), nameof(DeleteCat), id, deleteResult.DeletedCount);

                return true;
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Inside service: {Service} - method: {Method}, an error occured while deleting the cat with the provided id: {Id}.",
                    nameof(CatsCollectionRepository), nameof(DeleteCat), id);

                return false;
            }
        }
    }
}
