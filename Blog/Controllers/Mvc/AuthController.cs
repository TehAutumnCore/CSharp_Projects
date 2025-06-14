using Microsoft.AspNetCore.Mvc; //provides classes, interfaces and attributes that support core mvc functionality
using System.Net.Http.Headers; //privudes classes for working with HTTP headers, which are keyvalue pairs that carry additional context about HTTP requests and responses
using System.Text; //provides classes for working with text, encodings, string manipulation
using System.Text.Json; //provides classes for serializing objects to JSON and deseerializing object from JSON
using Blog.DTOs.Auth; //provides dto authentication and authorization functionalities

namespace Blog.Controllers.Mvc;

public class AuthController : Controller
{

    private readonly IHttpClientFactory _httpClientFactory; //best practice for http requests, prevents socket exhaustion, easily testable and injectable.

    public AuthController(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    //Renders the login form
    [HttpGet]
    public IActionResult Login() => View(); ///Auth/Login view

    //Handles login submission
    [HttpPost]
    public async Task<IActionResult> Login(LoginDTO dto) //accepts user login data(username+password) via form
    {
        if (!ModelState.IsValid) return View(dto); //make sure LoginDTO is valid

        //Send to API
        var client = _httpClientFactory.CreateClient();// Creates post request to the API login endpoint
        var json = JsonSerializer.Serialize(dto); //converts DTO into JSON
        var content = new StringContent(json, Encoding.UTF8, "application/json"); //Sets the Content-Type: application/json header
        var response = await client.PostAsync("http://localhost:5062/api/auth/login", content); //sends it as body content 
        if (!response.IsSuccessStatusCode) //if credentials are wrong, show an error and redisplay the form
        {
            ModelState.AddModelError("", "Invalid login attempt");
            return View(dto);
        }

        var result = await response.Content.ReadAsStringAsync(); //
        var token = JsonDocument.Parse(result).RootElement.GetProperty("token").GetString(); //Parses the token from the API's response
        HttpContext.Session.SetString("JwtToken", token!); //Stores the JWT in the user's session so you can later us it in headers          Session -> Tempdata

        return RedirectToAction("Index", "BlogView"); //Take the user to the blog list after successful login

    }

    //GET: /Auth/Register

    [HttpGet]
    public IActionResult Register() => View(); //shows the Register.cshtml form

    //Post: /Auth/Register
    [HttpPost]
    public async Task<IActionResult> Register(RegisterDto dto)
    {
        if (!ModelState.IsValid) return View(dto); //verifies dto model state

        var client = _httpClientFactory.CreateClient();//Creates an HTTP client for making a request to send to API
        var json = JsonSerializer.Serialize(dto); // Convert the DTO into JSON 
        var content = new StringContent(json, Encoding.UTF8, "application/json");//Sets the Content-Type: application/json header

        var response = await client.PostAsync("http://localhost:5062/api/auth/register", content); //send post request to the backend API
        if (!response.IsSuccessStatusCode) //If registration fails, show the user the error
        {
            var error = await response.Content.ReadAsStringAsync(); //gets the error message from API
            ModelState.AddModelError("", error); //attach error to Razor validation summary
            return View(dto); //redisplay form with error message
        }
        return RedirectToAction("Login", "Auth"); //Redirect to the login page if successful
    }


    public IActionResult Logout()
    {
        HttpContext.Session.Remove("JwtToken"); //delete jwt Cookie; clears teh token
        return RedirectToAction("Index", "Home"); //redirect back to homepage
    }

    /*
        public IActionResult Login() => View();
        public IActionResult Register() => View();
    */
}