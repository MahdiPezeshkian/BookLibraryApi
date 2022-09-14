using System.Linq.Expressions;
using BookLibraryApi.DataBaseContext;
using Microsoft.EntityFrameworkCore;
using BookLibraryApi.Models;
using BookLibraryApi.Dtos;

namespace BookLibraryApi.Repository;

public class UserRepository
{
    private GenericRepository<UserEntity> _dbUser;
    private BookRepository _bookRepository;
    private BookShelfRepository _bookShelfRepository;
    public UserRepository(Context context , BookRepository bookRepository , BookShelfRepository bookShelfRepository)
    {
        _dbUser = new GenericRepository<UserEntity>(context);
        _bookRepository = bookRepository;
        _bookShelfRepository = bookShelfRepository;
    }

    public IEnumerable<UserOutputDto> GetAllUsers()
    {
        var userEntityList = _dbUser.Get().ToList();
        List<UserOutputDto> userDtoList = new List<UserOutputDto>();

        foreach (var user in userEntityList)
        {
            userDtoList.Add(new UserOutputDto()
            {
                Id = user.Id,
                UserName = user.UserName,
                FirstName = user.FirstName,
                LastName = user.LastName,
                rule = user.rule,
                EmailAddress = user.EmailAddress
            });
        }
        
        return userDtoList;
    }

    public UserOutputDto GetUserById(int id)
    {
        UserEntity user = _dbUser.GetById(id);
        return new UserOutputDto()
        {
            UserName = user.UserName,
            FirstName = user.FirstName,
            LastName = user.LastName,
            EmailAddress = user.EmailAddress,
            rule = user.rule
        };
    }

    public void AddUser(UserInputDto user)
    {
        UserEntity userEntity = new UserEntity()
        {
            UserName = user.UserName,
            FirstName = user.FirstName,
            LastName = user.LastName,
            EmailAddress = user.EmailAddress,
            Password = user.PassWord,
            rule = user.rule
        };

        _dbUser.InsertRow(userEntity);
    }

    public bool DeleteUserById(int id)
    {
        return _dbUser.DeleteById(id);
    }


    public UserBookShelves GetUserShelves(int userId)
    {

        List<BookShelfOutputDto> bookShelfOutputDtos = _bookShelfRepository.GetBookShelfByUserId(userId);
        
        return new UserBookShelves()
        {
            user = GetUserById(userId),
            Shelves = bookShelfOutputDtos
        };
    }
}