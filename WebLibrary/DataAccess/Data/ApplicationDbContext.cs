using Microsoft.EntityFrameworkCore;
using Models.Models;
using Models.StoreProcedureModels;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Data
{
    public class ApplicationDbContext: DbContext
    {

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): base(options)
        {

        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<Publisher> Publishers { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<BookAuthor> BookAuthors { get; set; }
        public DbSet<BookGenre> BookGenres { get; set; }
        public DbSet<BookDetail> BookDetails { get; set; }

        public DbSet<BookDetailsView> BookDetailsView { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BookAuthor>().HasKey(ba => new{ ba.AuthorId, ba.BookId});
            modelBuilder.Entity<BookGenre>().HasKey(bg => new { bg.GenreId, bg.BookId });
            modelBuilder.Entity<Book>()
            .HasIndex(b => b.BookDetailId)
            .IsUnique();

            //view

            modelBuilder.Entity<BookDetailsView>().HasNoKey().ToView("GetOnlyBookDetails");
        }

        
    }
}
