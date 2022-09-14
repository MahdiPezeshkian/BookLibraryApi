using BookLibraryApi.DataBaseContext;
using Microsoft.AspNetCore.Mvc;
using BookLibraryApi.Models;
using BookLibraryApi.Repository;
using BookLibraryApi.Dtos;

namespace BookLibraryApi.Controllers;

[ApiController]
[Route("api/user/")]
public class UserController : ControllerBase
{
    private UserRepository _userRepository;
    private BookShelfRepository _bookShelfRepository;

    public UserController(UserRepository userRepository, BookShelfRepository bookShelfRepository)
    {
        _userRepository = userRepository;
        _bookShelfRepository = bookShelfRepository;
    }

    [HttpGet]
    public ActionResult<UserOutputDto> GetUsers()
    {
        return Ok(
            new JsonResult(_userRepository.GetAllUsers())
        );
    }

    [HttpGet("{userId}")]
    public ActionResult GetUserByID(int userId)
    {
        var result = _userRepository.GetUserById(userId);

        return Ok(
          new JsonResult(result)
        );
    }

    [HttpGet("{userId}/UserBookShelves")]
    public ActionResult<UserBookShelves> UserShelves(int userId)
    {
        return Ok(new JsonResult(
             _userRepository.GetUserShelves(userId)
        ));
    }

    [HttpPost]
    public ActionResult AddUser(UserInputDto user)
    {
        if (!ModelState.IsValid)
            return BadRequest();

        _userRepository.AddUser(user);
        return Created("api/user/", user);
    }

    [HttpPost("addBookToUsershelf")]
    public ActionResult AddBookToUsershelf(BookShelfInputDto bookShelfInputDto)
    {
        _bookShelfRepository.AddBookShelf(bookShelfInputDto);
        return Created("api/user/{userId}/addBookToUsershelf/{bookID}", bookShelfInputDto);
    }

    [HttpDelete("{userId}")]
    public ActionResult DeleteUserById(int userId)
    {
        return Ok(
            new JsonResult(new { Deleted = true })
        );
    }

    [HttpDelete("deleteBookFromUsershelf/{shelfId}")]
    public ActionResult DeleteBookFromUsershelf(int shelfId)
    {
        return Ok(
            new JsonResult(new { Deleted =  _bookShelfRepository.DeleteBookShelfById(shelfId)})
        );
    }



}