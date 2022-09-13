using System.ComponentModel.DataAnnotations;
using BookLibraryApi.Models;


namespace BookLibraryApi.Dtos;

public class BookInputDto
{
    [Required]
    [MinLength(2)]
    [MaxLength(50)]
    public string BookName { get; set; } = string.Empty;

    [Required]
    [MinLength(2)]
    [MaxLength(50)]
    public string BookAuthor { get; set; } = string.Empty;

}