using System.ComponentModel.DataAnnotations;

namespace meurh360backend.Models
{
    public class Usuario
    {
    [Key]
    public int Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;

    }
}
