using Microsoft.Build.Framework;

namespace RocketEshop.Data.Base
{
    public interface IEntityBase
    {
        [Required]
        int GameId { get; set; }
    }
}
