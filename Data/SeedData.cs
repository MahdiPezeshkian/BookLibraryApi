using BookLibraryApi.DataBaseContext;
using BookLibraryApi.Models;
using BookLibraryApi.Hash;

namespace BookLibraryApi.Data;
public static class SeedData
{
    private static HashAlgorithm _hashAlgorithm = new HashAlgorithm();
    public static void PopulateDb(IApplicationBuilder app)
    {
        using var serviceScope = app.ApplicationServices.CreateScope();
        AddInitialData(serviceScope.ServiceProvider.GetService<Context>()!);
    }

    private static void AddInitialData(Context context)
    {
        if (!context.Users.Any())
        {
            UserEntity admin = new UserEntity()
            {
                UserName = "stalker",
                FirstName = "mahdi",
                LastName = "pezeshkian",
                Password = _hashAlgorithm.Hash("1234"),
                EmailAddress = "admin@admin.com",
                role = "admin"
            };

            context.Users.Add(admin);
            context.SaveChanges();
        }

        if (!context.Books.Any())
        {
            List<BookEntity> books = new List<BookEntity>()
                    {
                    new BookEntity()
                    {
                        BookName = "riaziat",
                        BookAuthor = "javad javadi"
                    }
                    ,
                    new BookEntity()
                    {
                        BookName = "jabr",
                        BookAuthor = "mohammad mohhamadi"
                    }
                    ,
                    new BookEntity()
                    {
                        BookName = "amar o ehtemal",
                        BookAuthor = "rasul rasuli"
                    }
                    ,
                    new BookEntity()
                    {
                        BookName = "madar manteghi",
                        BookAuthor = "kobra kobraie"
                    }
                    ,
                    new BookEntity()
                    {
                        BookName = "tanzim khanevadeh",
                        BookAuthor = "amin jafarain"
                    }
                    ,
                    new BookEntity()
                    {
                        BookName = "zaban arabi",
                        BookAuthor = "moammad mohebi"
                    }
                };

            context.Books.AddRange(books);
            context.SaveChanges();
        }
    }
}