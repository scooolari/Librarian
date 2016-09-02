using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Books
{
    /// <summary>
    /// Model for a single book
    /// </summary>
    public class BookViewModel
    {
        public int BookId { get; set; }
        [Required]
        public string Author { get; set; }
        [Required]
        public string Title { get; set; }
        public DateTime? ReleaseDate { get; set; }
        [Required]
        public string ISBN { get; set; }
        [Required]
        public int BookGenreId { get; set; }
        public string BookGenre { get; set; }
        [Required]
        public int Count { get; set; }
        public DateTime AddDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public List<DictBookGenreViewModel> DictBookGenreList { get; set; }
    }
}
