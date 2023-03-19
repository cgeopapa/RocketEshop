using System.ComponentModel.DataAnnotations;
using RocketEshop.Core.Enums;

namespace RocketEshop.Core.Domain
{
    public class Filters
    {
        public SortingFilter Sorting { get; set; }

        public string? QuickSearchFilter { get; set; }

        [Display(Name = "Show only available Games")]
        public bool Availability { get; set; }

        public int MinPrice { get; set; } = 0;
        public int MaxPrice { get; set; } = 500;

        public Filters()
        {
            QuickSearchFilter = null;
            Availability = false;
            Sorting = SortingFilter.NameAsc;
            MinPrice = 0;
            MaxPrice = 500;
        }

        public Filters(string quickSearchFilter)
        {
            QuickSearchFilter = quickSearchFilter;
            Availability = false;
            Sorting = SortingFilter.NameAsc;
            MinPrice = 0;
            MaxPrice = 500;
        }
    }
}
