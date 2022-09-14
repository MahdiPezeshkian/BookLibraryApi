using System.Linq.Expressions;
using BookLibraryApi.DataBaseContext;
using Microsoft.EntityFrameworkCore;
using BookLibraryApi.Models;
using BookLibraryApi.Dtos;

namespace BookLibraryApi.Repository;

public class BookRepository
{
    private GenericRepository<BookEntity> _dbBook;
    private BookShelfRepository _bookShelfRepository;
    public BookRepository(Context context , BookShelfRepository bookShelfRepository)
    {
        _dbBook = new GenericRepository<BookEntity>(context);
        _bookShelfRepository = bookShelfRepository;
    }

    public IEnumerable<BookOutputDto> GetAllBooks()
    {
        IEnumerable<BookEntity> bookEntities = _dbBook.Get();
        List<BookOutputDto> bookDtos = new List<BookOutputDto>();

        foreach (var book in bookEntities)
        {
            bookDtos.Add(new BookOutputDto()
            {
                Id = book.Id,
                BookName = book.BookName,
                BookAuthor = book.BookAuthor
            });
        }


        return bookDtos;
    }

    public BookOutputDto GetBookById(int id)
    {
        BookEntity bookEntity = _dbBook.GetById(id);
        return new BookOutputDto()
        {
            Id = bookEntity.Id,
            BookName = bookEntity.BookName,
            BookAuthor = bookEntity.BookAuthor
        };
    }

    public List<BookWithCountPropDto> GetMostPopularBooks()
    {
        var bookGroupByResult = _bookShelfRepository.Get()
            .GroupBy(g => g.BookId)
            .Select(s => new { count = s.Count(), bookId = s.Key })
            .ToList();

        var bookResult = GetAllBooks();

        return bookGroupByResult
        .Join(bookResult,
                    GroupByresult => GroupByresult.bookId,
                    book => book.Id,
                    (GroupByresult, book) => new BookWithCountPropDto()
                    {
                        book = book,
                        count = GroupByresult.count
                    }).OrderByDescending(o => o.count).ToList();
    }
    public void AddBook(BookInputDto book)
    {
        _dbBook.InsertRow(new BookEntity
        {
            BookName = book.BookName,
            BookAuthor = book.BookAuthor
        });
    }

    public void DeleteBookById(int id)
    {
        _dbBook.DeleteById(id);
    }


}