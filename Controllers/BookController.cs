using BookLibraryApi.DataBaseContext;
using Microsoft.AspNetCore.Mvc;
using BookLibraryApi.Models;
using BookLibraryApi.Repository;
using BookLibraryApi.Dtos;

namespace BookLibraryApi.Controllers;

[ApiController]
[Route("api/book/")]
public class BookController : ControllerBase
{

    private readonly BookRepository _bookRepository;

    public BookController(BookRepository bookRepository)
    {
        _bookRepository = bookRepository;
    }

    [HttpGet]
    public ActionResult<BookOutputDto> GetAllBooks()
    {
        return Ok(
            new JsonResult(_bookRepository.GetAllBooks())
        );
    }

    [HttpGet("{bookId}")]
    public ActionResult GetBookById(int bookId)
    {
        return Ok(
            new JsonResult(_bookRepository.GetBookById(bookId))
        );
    }

    [HttpPost]
    public ActionResult AddBook(BookInputDto book)
    {
        if (!ModelState.IsValid)
            return BadRequest();

        _bookRepository.AddBook(book);
        return Created("api/user/", book);
    }

    [HttpDelete("{bookId}")]
    public ActionResult DeleteBookByID(int bookId)
    {
        return Ok(
            new JsonResult(new { Deleted = _bookRepository.DeleteBookById(bookId) })
        );
    }
}