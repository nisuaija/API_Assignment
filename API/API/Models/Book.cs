using System.ComponentModel.DataAnnotations;

namespace API.Models
{
    //Models for Book entity and reservation
    public class Book
    {
        [Key]
        public string Id { get; set; } = string.Empty;
        [Required]
        public string Title { get; set; } = string.Empty;
        public string? Author { get; set; }
        public Reservation? Reservation { get; set; }
    }

    public class Reservation
    {
        [Key]
        public string Id { get; set; } = string.Empty;
        [Required]
        public string Comment { get; set; } = string.Empty;
    }

    //For PUT operation lets only update Title and Author for easier Swagger usage.
    public class BookUpdate
    {
        public string Title { get; set; } = string.Empty;
        public string Author { get; set; } = string.Empty;

    }
}
