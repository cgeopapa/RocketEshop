using System.ComponentModel.DataAnnotations;

namespace RocketEshop.Core.Enums
{
    public enum Rating
    {
        Poor,
        Bad,
        Average,
        Good,
        Excellent,
    }

    public enum SortingFilter
    {
        [Display(Name = "A - Z")]
        NameAsc,
        [Display(Name = "Z - A")]
        NameDesc,
        [Display(Name = "By Price - Most cheap first")]
        PriceAsc,
        [Display(Name = "By Price - Most expensive first")]
        PriceDsc,
        [Display(Name = "By Release Date - Latest release first")]
        ReleaseDateAsc,
        [Display(Name = "By Release Date - Oldest release first")]
        ReleaseDateDsc,
    }
}
