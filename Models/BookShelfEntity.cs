namespace BookLibraryApi.Models;

public class BookShelfEntity
{
    public int Id { get; set; }
    public string BookStatus { get; set; }
    public int UserId { get; set; }
    public int BookId { get; set; }
    public UserEntity User { get; set; }
    public BookEntity Book { get; set; }
    public DateTime TimeToBuild { get; set; } = DateTime.Now;

}