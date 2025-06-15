using Microsoft.AspNetCore.Mvc; //imports mvc framework
using Blog.Models; //imports models
using System.Net; //provides an interface for many protocols used on networks
using System.Net.Mail; //contains classes used to send electronic mail to a Simple Mail Transfer Protocol(SMTP) server for delivery

namespace Blog.Controllers.Mvc;
//flow:  User submits form → Validated → Email built → Email sent → Redirect to success page

public class ContactController : Controller
{
    private readonly IConfiguration _config;//gives access to user-secrets for Email credentials, smtp server info, and destination email(myself)

    public ContactController(IConfiguration config) //Constructor injection
    {
        _config = config;
    }

    [HttpGet]
    public IActionResult Index()
    {
        return View(); //Load the form(e.g name,email,message) and return Views/Contact/Index.cshtml
    }

    //PRG - Post/Redirect/Get. Used to prevent the resubmission of a form caused by reloading the same web page
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Index(ContactFormModel model) //bind submitted form data to ContactFormModel
    {
        if (!ModelState.IsValid) return View(model);  //validate mode fields that are using annotations such as [required] or are within the string length, 
                                                      // // redisplay form with validation messages if invalid
                                                      //Load secure config from user-secrets
        var smtpUser = _config["EmailSettings:SenderEmail"]; //sending address
        var senderName = _config["EmailSettings:SenderName"];  //Gary Rojas Portfolio
        var smtpHost = _config["EmailSettings:SmtpServer"]; //smtp.email.com
        var smtpPort = int.Parse(_config["EmailSettings:SmtpPort"]!); //Port 587 - SMTP
        var smtpPass = _config["EmailSettings:SmtpPassword"]; //outlook app pass
        var recipientEmail = _config["EmailSettings:RecipientEmail"]; //my address the form/sender will send to

        var message = new MailMessage // constructs the mail message
        {
            From = new MailAddress(smtpUser!, senderName), //Set the form address and display name
            Subject = $"Portfolio - Contact form From {model.Name}", //subject with senders name
            Body = $"From: {model.Name} <{model.Email}>\n\n{model.Message}" //message body
        };
        message.To.Add(recipientEmail!); //Sends to myself from the form

        using var smtp = new SmtpClient(smtpHost, smtpPort) //Set up SMTP client to send the email
        {
            Credentials = new NetworkCredential(smtpUser, smtpPass), //Auth using the sender user email and pass
            EnableSsl = true //required for secure connection - Secure Socket Layer
        };
        smtp.Send(message); //send the message

        TempData["Success"] = "Thanks for reaching out! I'll get back to you soon."; //temp data stores the 1 time sucess message
        return RedirectToAction("Index"); //redirect to clear form and follow PRG pattern
    }
      
}