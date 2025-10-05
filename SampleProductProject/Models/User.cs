using System.ComponentModel.DataAnnotations;

namespace SampleProductProject.Models;

public class User
{
    public int Id { get; set; }

    [Required(ErrorMessage = "UserName is required")]
    [StringLength(50)]
    public string UserName { get; set; } = null!;

    [Required(ErrorMessage = "PassWord is required")]
    [StringLength(50)]
    public string Password { get; set; } = null!;

    public ICollection<UserProduct> Products { get; set; } = new List<UserProduct>();
}
