using Covid_App.Entities;
using Covid_App.Model;

namespace Covid_App.Services.Users
{
    public interface IUserService
    {
        AuthenticationResponse? Authenticate(AuthenticationRequest request);
        
    }
}

