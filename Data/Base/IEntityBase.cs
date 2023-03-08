using Microsoft.Build.Framework;

namespace RocketEshop.Data.Base
{
    public interface IEntityBase
    {
        [Required]
        int Id { get; set; }
    }
}
