namespace Catty.Api.Controllers
{
    [ApiController]
    [Authorize(AuthenticationSchemes = "PrivateKey")]
    [ProducesResponseType(typeof(ApiResponse<EmptyDto>), StatusCodes.Status500InternalServerError)]
    public class CatsController : ControllerBase
    {
        private readonly ICatsCollectionService _catsCollectionService;
        public CatsController(ICatsCollectionService catsCollectionService) => _catsCollectionService = catsCollectionService;

        /// <summary>
        /// Gets all the cats
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        [HttpGet("cats")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(ApiResponse<List<CatResponse>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<EmptyDto>), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiResponse<EmptyDto>), StatusCodes.Status400BadRequest)]
        [SwaggerOperation(nameof(GetCats), OperationId = nameof(GetCats))]
        public async Task<IActionResult> GetCats([FromQuery] CatFilter filter)
        {
            var result = await _catsCollectionService.GetAllCats(filter);

            return StatusCode(Convert.ToInt32(result.Code), result);
        }

        /// <summary>
        /// Gets a cat by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("cat/{id}")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(ApiResponse<CatResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<EmptyDto>), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiResponse<EmptyDto>), StatusCodes.Status400BadRequest)]
        [SwaggerOperation(nameof(GetCat), OperationId = nameof(GetCat))]
        public async Task<IActionResult> GetCat(string id)
        {
            var result = await _catsCollectionService.GetCat(id);

            return StatusCode(Convert.ToInt32(result.Code), result);
        }

        /// <summary>
        /// Creates a new cat
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("cat")]
        [Consumes("application/json")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(ApiResponse<CatResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<EmptyDto>), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiResponse<EmptyDto>), StatusCodes.Status400BadRequest)]
        [SwaggerOperation(nameof(AddCat), OperationId = nameof(AddCat))]
        public async Task<IActionResult> AddCat([FromBody] AddCatRequest request)
        {
            var result = await _catsCollectionService.AddCat(request);

            return StatusCode(Convert.ToInt32(result.Code), result);
        }

        /// <summary>
        /// Updates a cat's speciality by id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPut("cat/{id}")]
        [Consumes("application/json")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(ApiResponse<string>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<EmptyDto>), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiResponse<EmptyDto>), StatusCodes.Status400BadRequest)]
        [SwaggerOperation(nameof(UpdateCat), OperationId = nameof(UpdateCat))]
        public async Task<IActionResult> UpdateCat(string id,[FromBody] UpdateCatRequest request)
        {
            var result = await _catsCollectionService.UpdateCat(id,request);

            return StatusCode(Convert.ToInt32(result.Code), result);
        }

        /// <summary>
        /// Deletes a cat by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("cat/{id}")]
        [Consumes("application/json")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(ApiResponse<string>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<EmptyDto>), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiResponse<EmptyDto>), StatusCodes.Status400BadRequest)]
        [SwaggerOperation(nameof(DeleteCat), OperationId = nameof(DeleteCat))]
        public async Task<IActionResult> DeleteCat(string id)
        {
            var result = await _catsCollectionService.DeleteCat(id);

            return StatusCode(Convert.ToInt32(result.Code), result);
        }
    }
}
