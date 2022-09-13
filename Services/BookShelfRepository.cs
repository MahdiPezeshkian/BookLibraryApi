using System.Linq.Expressions;
using BookLibraryApi.DataBaseContext;
using Microsoft.EntityFrameworkCore;
using BookLibraryApi.Models;
using BookLibraryApi.Dtos;


namespace BookLibraryApi.Repository;

public class BookShelfRepository
{
    private GenericRepository<BookShelfEntity> _dbBookShelf;
    public BookShelfRepository(Context context)
    {
        _dbBookShelf = new GenericRepository<BookShelfEntity>(context);
    }

    public BookShelfOutputDto GetBookShelfById(int id)
    {
        var entityReult = _dbBookShelf.GetById(id);
        return new BookShelfOutputDto()
        {
            Id = entityReult.Id,
            BookId = entityReult.BookId,
            UserId = entityReult.UserId
        };
    }

    public List<BookShelfOutputDto> GetBookShelfByUserId(int UserId)
    {
        var entityReult = _dbBookShelf.Get().Where(shelf => shelf.UserId == UserId).ToList();
        List<BookShelfOutputDto> BookShelfOutputDtoList = new List<BookShelfOutputDto>();

        foreach (var shelf in entityReult)
        {
            BookShelfOutputDtoList.Add(
                new BookShelfOutputDto()
                {
                    Id = shelf.Id,
                    BookId = shelf.BookId,
                    Status = shelf.BookStatus,
                    UserId = shelf.UserId
                }
            );
        }

        return BookShelfOutputDtoList;
    }


    public List<BookShelfOutputDto> Get(Expression<Func<BookShelfEntity, bool>> where = null)
    {
        var entityReult = _dbBookShelf.Get(where);
        List<BookShelfOutputDto> dtoResult = new List<BookShelfOutputDto>();

        foreach ( var shelf in entityReult)
        {
            dtoResult.Add(
                new BookShelfOutputDto
                {
                    Id = shelf.Id,
                    BookId = shelf.BookId,
                    UserId = shelf.UserId
                }
            );
        }

        return dtoResult;
    }

    public bool DeleteBookShelfById(int id)
    {
        if(GetBookShelfById(id) == null)
            return false;
        
        _dbBookShelf.DeleteById(id);
        return true;
    }


    public void AddBookShelf(BookShelfInputDto bookShelf)
    {
        BookShelfEntity bookShelfEntity = new BookShelfEntity()
        {
            BookId = bookShelf.BookId,
            UserId = bookShelf.UserId,
            BookStatus = bookShelf.Status
        };

        _dbBookShelf.InsertRow(bookShelfEntity);
    }


}