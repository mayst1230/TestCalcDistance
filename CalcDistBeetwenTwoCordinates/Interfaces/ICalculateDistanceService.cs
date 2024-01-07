using CalcDistBeetwenTwoCordinates.Controllers.v1;
using CalcDistBeetwenTwoCordinates.Modules.Queries;

namespace CalcDistBeetwenTwoCordinates.Interfaces
{
    public interface ICalculateDistanceService
    {
        double CalculateDistanceByCords(GetDistanceQueryDefinition query);
    }
}
