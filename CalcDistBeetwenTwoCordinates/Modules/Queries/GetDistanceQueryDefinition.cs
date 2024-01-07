using System.ComponentModel.DataAnnotations;

namespace CalcDistBeetwenTwoCordinates.Modules.Queries
{
    /// <summary>
    /// Данные запроса для получения дистанции между 2 точками (мили).
    /// </summary>
    public class GetDistanceQueryDefinition
    {
        public GeoCord FirstMarker { get; set; }

        public GeoCord SecondMarker { get; set; }
    }

    /// <summary>
    /// Географическая координата.
    /// </summary>
    public class GeoCord
    {
        /// <summary>
        /// Долгота.
        /// </summary>
        [Required]
        public double Lon { get; set; }

        /// <summary>
        /// Широта.
        /// </summary>
        [Required]
        public double Lat { get; set; }
    }
}
