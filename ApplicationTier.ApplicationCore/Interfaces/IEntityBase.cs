using System.ComponentModel.DataAnnotations;

namespace RocketEshop.Data.Base
{
    public interface IEntityBase
    {
        [Required]
        int Id { get; set; }
    }
}
