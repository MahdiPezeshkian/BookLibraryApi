using System.ComponentModel.DataAnnotations;
using BookLibraryApi.Models;


namespace BookLibraryApi.Dtos;

public class BookShelfInputDto
{
    [Required]
    public int BookId { get; set; }
    [Required]
    public int UserId { get; set; }
    public string Status { get; set; }
}