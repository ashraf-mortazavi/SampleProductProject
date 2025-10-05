using System.ComponentModel.DataAnnotations;

namespace SampleProductProject.Models;

public class UserProduct
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Title is required")]
    [StringLength(100)]
    public string Title { get; set; } = null!;

    [Required(ErrorMessage = "Code is required")]
    [StringLength(100)]
    public string Code { get; set; } = null!;
    public int? Price { get; set; }

    public int UserId { get; set; }
    public User User { get; set; } = null!;
}