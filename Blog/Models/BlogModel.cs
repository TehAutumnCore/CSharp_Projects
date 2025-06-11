using System.ComponentModel.DataAnnotations;

namespace Blog.Models
{
    public class BlogPost
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; } = "No Title Provided";
        [Required]
        public string Description { get; set; } = "No Description Provided";
        public DateTime Date { get; set; } = DateTime.Now;
        public string Author { get; set; } = "Anonymous User";


    }
}