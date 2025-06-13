using System.ComponentModel.DataAnnotations;

namespace Blog.Models
{
    public class AppUser
    {
        public int Id { get; set; }

        [Required]
        public string Username { get; set; } = string.Empty; //defaults empty string

        [Required]
        public byte[] PasswordHash { get; set; } = Array.Empty<byte>(); //Converts passwords into unreadable hashes, protection against potential breaches. defaults empty byte array

        [Required]
        public byte[] PasswordSalt { get; set; } = Array.Empty<byte>();// Adds a random string to each password before hashing, making it much harder to crack using pre-computed tables.
        public string Role { get; set; } = "Guest"; //there will be a guest role and admin role. Guest will be by default, while Admin will be exclusive for myself
    }
}

/* JWT Setup flow
creating a User Model
creating a table in BlogDbContext
creating a DTOs/Auth Folder
    RegisterDTO.cs -> for signup
    LoginDTO.cs -> for Login
creating a JWT Helper Service
    Accepts a user
    Generates a JWT Token
    Sign with secret key
    add claims(username + role)
creating AuthController api
    api/auth/register
    api/auth/login
        each will:
            Validate DTO
            Hash/Salt passwords
            Store/Retrieve users
            Return a JWT Token on login
*/