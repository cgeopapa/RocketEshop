using System.ComponentModel.DataAnnotations;

namespace RocketEshop.Core.Interfaces
{
    public interface IEntity
    {
        [Required]
        int Id { get; set; }
    }
}
