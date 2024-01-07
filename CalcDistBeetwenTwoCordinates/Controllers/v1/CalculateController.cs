using CalcDistBeetwenTwoCordinates.Interfaces;
using CalcDistBeetwenTwoCordinates.Modules.Queries;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace CalcDistBeetwenTwoCordinates.Controllers.v1
{
    [Route("v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    public class CalculateController : ControllerBase
    {
        private readonly ICalculateDistanceService _calculateDistanceService;

        public CalculateController(ICalculateDistanceService calculateDistanceService)
        {
            _calculateDistanceService = calculateDistanceService;
        }

        /// <summary>
        /// Получение дистанции между двумя точками.
        /// </summary>
        [HttpPost("get-distance")]
        public async Task<GetDistanceQueryResult> GetDistance([FromBody][Required] GetDistanceQueryDefinition query)
        {
            return await Task.FromResult(new GetDistanceQueryResult()
            {
                Dist = _calculateDistanceService.CalculateDistanceByCords(query)
            });
        }
    }
}

