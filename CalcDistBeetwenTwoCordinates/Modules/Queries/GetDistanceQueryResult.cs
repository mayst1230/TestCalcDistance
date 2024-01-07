using System.ComponentModel.DataAnnotations;

namespace CalcDistBeetwenTwoCordinates.Modules.Queries
{
    /// <summary>
    /// Ответ на запрос дистанции между двумя точками (мили).
    /// </summary>
    public class GetDistanceQueryResult
    {
        /// <summary>
        /// Дистанция между двумя точками (мили).
        /// </summary>
        [Required]
        public double Dist { get; set; }
    }
}
