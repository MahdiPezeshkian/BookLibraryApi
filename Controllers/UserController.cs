using BookLibraryApi.DataBaseContext;
using Microsoft.AspNetCore.Mvc;
using BookLibraryApi.Models;
using BookLibraryApi.Repository;
using BookLibraryApi.Dtos;
using BookLibraryApi.Hash;
using BookLibraryApi.Generatetoken;
using Microsoft.AspNetCore.Authorization;

namespace BookLibraryApi.Controllers;

[ApiController]
[Route("api/user/")]
public class UserController : ControllerBase
{
    private UserRepository _userRepository;
    private BookShelfRepository _bookShelfRepository;
    private HashAlgorithm _hashAlgorithm;
    private TokenGenerator _tokenGenerator;

    public UserController(UserRepository userRepository, BookShelfRepository bookShelfRepository ,HashAlgorithm hashAlgorithm, TokenGenerator tokenGenerator)
    {
        _userRepository = userRepository;
        _bookShelfRepository = bookShelfRepository;
        _hashAlgorithm = hashAlgorithm;
        _tokenGenerator = tokenGenerator;
    }

    [Authorize(Roles = "admin")]
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

    [HttpPost("signUp")]
    public ActionResult SignUp(UserInputDto user)
    {
        if (!ModelState.IsValid)
            return BadRequest();

        _userRepository.AddUser(user);
        return Created("api/user/", user);
    }

    [HttpPost("logIn")]
    public ActionResult LogIn(string userName , string passWord)
    {
        var user = _userRepository.GetUserEntityByUsername(userName);

        if (user == null)
            return NotFound();
        
        if (user.Password != _hashAlgorithm.Hash(passWord))
            return Conflict();
        
        if (_userRepository.CheckUserNamePassword(userName , passWord))
            return Conflict();

        return Ok(_tokenGenerator.GetToken(user));
    }

    [HttpDelete("deleteBookFromUsershelf/{shelfId}")]
    public ActionResult DeleteBookFromUsershelf(int shelfId)
    {
        return Ok(
            new JsonResult(new { Deleted =  _bookShelfRepository.DeleteBookShelfById(shelfId)})
        );
    }



}