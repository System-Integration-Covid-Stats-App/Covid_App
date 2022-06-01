using Covid_App.Entities;

namespace Covid_App.Model.Response;

public class AuthenticationResponse
{
    public string Token { get; set; }
    public AuthenticationResponse(string token)
    {
        Token = token;
    }
}

