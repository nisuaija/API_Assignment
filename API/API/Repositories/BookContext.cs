using Microsoft.EntityFrameworkCore;
using API.Models;

namespace API.Repositories
{
    public class BookContext : DbContext
    {
        public BookContext(DbContextOptions<BookContext> options) : base(options) { }

        public DbSet<Book> Books { get; set; }
        public DbSet<Reservation> Reservations { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Relationship between Book and Reservation
            modelBuilder.Entity<Book>().ToTable("Books")
                 .HasOne(b => b.Reservation);
        }
    }
}
