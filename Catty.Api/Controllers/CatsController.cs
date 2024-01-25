namespace Catty.Api.Controllers
{
    [ApiController]
    [Authorize(AuthenticationSchemes = "PrivateKey")]
    public class CatsController : ControllerBase
    {
        private readonly ICatsCollectionService _catsCollectionService;
        public CatsController(ICatsCollectionService catsCollectionService)
        {
            _catsCollectionService = catsCollectionService;
        }

        [HttpGet("cats")]
        public async Task<IActionResult> GetCats([FromQuery] CatFilter filter)
        {
            var result = await _catsCollectionService.GetAllCats(filter);

            return StatusCode(Convert.ToInt32(result.Code), result);
        }

        [HttpGet("cat/{id}")]
        public async Task<IActionResult> GetCat(string id)
        {
            var result = await _catsCollectionService.GetCat(id);

            return StatusCode(Convert.ToInt32(result.Code), result);
        }

        [HttpPost("cat")]
        public async Task<IActionResult> AddCat([FromBody] AddCatRequest request)
        {
            var result = await _catsCollectionService.AddCat(request);

            return StatusCode(Convert.ToInt32(result.Code), result);
        }

        [HttpPut("cat")]
        public async Task<IActionResult> UpdateCat([FromBody] UpdateCatRequest request)
        {
            var result = await _catsCollectionService.UpdateCat(request);

            return StatusCode(Convert.ToInt32(result.Code), result);
        }

        [HttpDelete("cat/{id}")]
        public async Task<IActionResult> UpdateCat(string id)
        {
            var result = await _catsCollectionService.DeleteCat(id);

            return StatusCode(Convert.ToInt32(result.Code), result);
        }
    }
}
