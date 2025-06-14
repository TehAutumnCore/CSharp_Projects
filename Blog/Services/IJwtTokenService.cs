using Blog.Models;

namespace Blog.Services;

public interface IJwtTokenService
{
    string CreateToken(AppUser user); //when given a user, generate a signed jwt string.
}