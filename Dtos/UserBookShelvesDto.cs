using System.ComponentModel.DataAnnotations;
using BookLibraryApi.Models;

namespace BookLibraryApi.Dtos;
public class UserBookShelves
{
    public UserOutputDto user { get; set; }
    public List<BookShelfOutputDto> Shelves { get; set; }
}