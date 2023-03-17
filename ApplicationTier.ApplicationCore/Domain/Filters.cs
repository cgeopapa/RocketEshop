using System.ComponentModel.DataAnnotations;

namespace RocketEshop.Core.Domain
{
    public class Filters
    {
        public string? QuickSearchFilter { get; set; }

        [Display(Name = "Show only available Games")]
        public bool Availability { get; set; } = false;

        public Filters() { }

        public Filters(string? quickSearchFilter, bool availability)
        {
            this.QuickSearchFilter = quickSearchFilter;
            this.Availability = availability;
        }
    }
}
