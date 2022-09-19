using Microsoft.EntityFrameworkCore;
using BookLibraryApi.Models;
using Microsoft.Extensions.Configuration;

namespace BookLibraryApi.DataBaseContext;

public class Context : DbContext
{
    public Context(DbContextOptions options) : base(options)
    {

    }

    public DbSet<UserEntity> Users { get; set; }
    public DbSet<BookEntity> Books { get; set; }
    public DbSet<BookShelfEntity> BookShelves { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<UserEntity>()
            .HasKey(key => key.Id);

        modelBuilder.Entity<BookEntity>()
            .HasKey(key => key.Id);

        modelBuilder.Entity<BookShelfEntity>()
            .HasKey(key => key.Id);

        modelBuilder.Entity<UserEntity>()
            .Property(property => property.Id)
            .ValueGeneratedOnAdd()
            .IsRequired();

        modelBuilder.Entity<UserEntity>()
            .Property(property => property.UserName)
            .HasMaxLength(50)
            .IsUnicode()
            .IsRequired();

        modelBuilder.Entity<UserEntity>()
            .Property(property => property.FirstName)
            .HasMaxLength(50)
            .IsRequired();

        modelBuilder.Entity<UserEntity>()
            .Property(property => property.LastName)
            .HasMaxLength(50)
            .IsRequired();

        modelBuilder.Entity<UserEntity>()
            .Property(property => property.EmailAddress)
            .HasMaxLength(50)
            .IsRequired();

        modelBuilder.Entity<UserEntity>()
            .Property(property => property.role)
            .IsRequired()
            .HasMaxLength(50);
        
        modelBuilder.Entity<BookEntity>()
            .Property(property => property.Id)
            .ValueGeneratedOnAdd()
            .IsRequired();

        modelBuilder.Entity<BookEntity>()
            .Property(property => property.BookName)
            .HasMaxLength(50)
            .IsRequired();

        modelBuilder.Entity<BookEntity>()
            .Property(property => property.BookAuthor)
            .HasMaxLength(50)
            .IsRequired();

        modelBuilder.Entity<BookShelfEntity>()
            .Property(property => property.Id)
            .ValueGeneratedOnAdd()
            .IsRequired();

        modelBuilder.Entity<BookShelfEntity>()
            .Property(property => property.UserId)
            .IsRequired();

        modelBuilder.Entity<BookShelfEntity>()
            .Property(property => property.BookId)
            .IsRequired();
    }
}
