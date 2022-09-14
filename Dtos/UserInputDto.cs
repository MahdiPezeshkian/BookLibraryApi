using System.ComponentModel.DataAnnotations;
using BookLibraryApi.Models;


namespace BookLibraryApi.Dtos;

public class UserInputDto
{

    [Required]
    [MinLength(2)]
    [MaxLength(50)]
    public string UserName { get; set; } = string.Empty;

    [Required]
    [MinLength(2)]
    [MaxLength(50)]
    public string FirstName { get; set; } = string.Empty;

    [Required]
    [MinLength(2)]
    [MaxLength(50)]
    public string LastName { get; set; } = string.Empty;

    [Required]
    [MinLength(2)]
    [MaxLength(50)]
    public string EmailAddress { get; set; } = string.Empty;
    
    [Required]
    [MinLength(2)]
    [MaxLength(50)]
    public string PassWord { get; set; } = string.Empty;

    [Required]
    [MinLength(2)]
    [MaxLength(50)]
    public string rule { get; set; }
}