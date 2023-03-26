using System.ComponentModel.DataAnnotations;

namespace RocketEshop.Core.Interfaces
{
    public interface IEntity<T>
    {
        [Required]
        T Id { get; set; }
    }
}
