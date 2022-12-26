using System;

namespace ExpoApp.Service.SearchModels
{
    public class ExpoSearchModel : BaseSearchModel
    {
        public string? Title { get; set; }
        public DateTime? DateFrom { get; set; }
        public DateTime? DateTo { get; set; }
        public double? PriceFrom { get; set; }
        public double? PriceTo { get; set; }
        public int? Industry { get; set; }
    }
}
