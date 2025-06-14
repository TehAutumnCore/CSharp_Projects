using Microsoft.AspNetCore.Mvc;
using Blog.DTOs.Auth;
using Blog.Models;
using Blog.Services;
using Blog.Data;
using System.Security.Cryptography;
using System.Text;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.VisualBasic;

namespace Blog.Controllers.Api;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly PortfolioDbContext _context; //access to the users table like Db.Users.Add(user)
    private readonly IJwtTokenService _tokenService; //calls the CreateToken(user) method from IJwtTokenServices.cs

    public AuthController(PortfolioDbContext context, IJwtTokenService tokenService) //constructor injection
    {
        _context = context; //updates db with AuthController(context,)
        _tokenService = tokenService; //updates with user information based on when IJwtTokenServices is called through program.cs
    }

    //Post: api/auth/register
    [HttpPost("register")]
    public IActionResult Register(RegisterDto dto)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState); //checks if required fields in dto model are validated 

        if (_context.Users.Any(u => u.Username == dto.Username)) //prevents duplicate user registration by checking if the model already exist in db.Users
            return BadRequest("Username already exists"); //if username exists, give BadRequest 400

        using var hmac = new HMACSHA512(); //cryptographic hash function
        var user = new AppUser //creates user based on the validated ModelState
        {
            Username = dto.Username, //assigns client created username to db
            Role = "Guest", //defaults all users to "Guest" role
            PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(dto.Password)), //creates random key (hmac.Key) used as salt
            PasswordSalt = hmac.Key //hashes the password with that salt and stores the result into password Salt.
        };

        _context.Users.Add(user); //store the user in the db
        _context.SaveChanges(); //saves db changes

        return Ok("User registered successfully"); //return status Http status OK, user registered successfully.

        /* JSON FORMAT Register
            Header: Content-Type
            Value: application/json

            //When protected routes are set up:
            Header: Authorization	
            Value: Bearer YOUR_TOKEN_HERE


            Post: http://127.0.0.1:[Port]/api/auth/register
            { 
              "username": "gary",
              "password": "secure123!"
            }
        */
    }

    //Post: api/auth/login
    [HttpPost("login")]
    public IActionResult Login(LoginDTO dto)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState); //validates whether model met the [Required] field

        var user = _context.Users.FirstOrDefault(u => u.Username == dto.Username); //returns first element of a sequence, checks if the new user matches any of the users in the db.Users table
        if (user == null) return Unauthorized("Invalid username or password");//if user doesn't exist in the table return Invalid username or password

        using var hmac = new HMACSHA512(user.PasswordSalt); //Recreate the exact same HMACSHA512 hash generating function using the stored salt(hmac.key)
        var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(dto.Password)); //take the password they typed, convert it into bytes, and run it through computerhash to see if password was correct

        if (!computedHash.SequenceEqual(user.PasswordHash)) //Check each byte in both arrays, and if any byte is different the password is wrong, if they match its correct.
            return Unauthorized("Invalid username or password"); //returns Invalid username or password if password is wrong.

        var token = _tokenService.CreateToken(user); //takes a validated user, extracts details like username/password, created JWT, signs using secret key and returns JWT as a string

        return Ok(new { token }); //Wraps the token in a JSON object and sends an HTTP 200 OK response.
    }
}