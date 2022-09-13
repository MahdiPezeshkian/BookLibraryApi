using System.ComponentModel.DataAnnotations;
using BookLibraryApi.Models;


namespace BookLibraryApi.Dtos;

public class BookShelfOutputDto
{
    public int Id { get; set; }
    [Required]
    public int BookId { get; set; }
    [Required]
    public int UserId { get; set; }
    public string Status { get; set; }
}