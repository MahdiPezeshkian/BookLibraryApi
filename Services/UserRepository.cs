using System.Linq.Expressions;
using BookLibraryApi.DataBaseContext;
using Microsoft.EntityFrameworkCore;
using BookLibraryApi.Models;
using BookLibraryApi.Dtos;
using BookLibraryApi.Hash;

namespace BookLibraryApi.Repository;

public class UserRepository
{
    private GenericRepository<UserEntity> _dbUser;
    private BookRepository _bookRepository;
    private BookShelfRepository _bookShelfRepository;
    private HashAlgorithm _hashAlgorithm;
    public UserRepository(Context context, BookRepository bookRepository, BookShelfRepository bookShelfRepository, HashAlgorithm hashAlgorithm)
    {
        _dbUser = new GenericRepository<UserEntity>(context);
        _bookRepository = bookRepository;
        _bookShelfRepository = bookShelfRepository;
        _hashAlgorithm = hashAlgorithm;
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
                role = user.role,
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
            role = user.role, 
            Id = user.Id
        };
    }

    public UserEntity GetUserEntityById(int id)
    {
        return _dbUser.GetById(id); 
    }

    public UserEntity GetUserEntityByUsername(string userName)
    {
        return _dbUser.Get(user => user.UserName == userName).FirstOrDefault();
    }

    public UserOutputDto GetUserByUsername(string userName)
    {
        UserEntity user = _dbUser.Get(user => user.UserName == userName).FirstOrDefault();
        return new UserOutputDto()
        {
            UserName = user.UserName,
            FirstName = user.FirstName,
            LastName = user.LastName,
            EmailAddress = user.EmailAddress,
            role = user.role
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
            Password = _hashAlgorithm.Hash(user.PassWord),
            role = user.role
        };

        _dbUser.InsertRow(userEntity);
    }

    public void DeleteUserById(int id)
    {
        _dbUser.DeleteById(id);
    }

    public bool CheckUserNamePassword(string username , string passWord)
    {
        UserEntity user = _dbUser.Get(user => user.UserName == username).FirstOrDefault();

        if (passWord == user.Password)
            return true;

        return false;
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