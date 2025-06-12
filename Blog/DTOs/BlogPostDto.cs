using System.ComponentModel.DataAnnotations;
namespace Blog.DTOs;
/* Definiton -> DTO: Data Transfer Object
An object used to encapsulate data for transfer between different parts of a software system.

DTOs are a copy of the model thats tailored for the user so you arent sending sensitive info everytime. ; A trimmed down safe version of your model shaped for the user or API client- only what they need to see or send. Only thing to interact with Models directly is the DB

DTOs are used for sending and receiving data
Only the database interacts with the full model

//receiving dto from client and convert it into a real BlogPost to store into the db
Post:  DTO -> Model  from dto.Title -> post.Title

//Retrieving a BlostPost model from the database and converting it into a BlogPostDto to return to the client
Get:   Model-> Dto   from post.Title -> dto.Title

*/
public class BlogPostDto
{
    [Required]
    public string Title { get; set; } = string.Empty;
    [Required]
    public string Description { get; set; } = string.Empty;
    public string Author { get; set; } = "Anonymous User";
    public DateTime Date { get; set; } = DateTime.Now;

}