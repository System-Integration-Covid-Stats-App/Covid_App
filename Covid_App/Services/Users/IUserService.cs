using Covid_App.Entities;
using Covid_App.Model;
using Covid_App.Model.Request;
using Covid_App.Model.Response;

namespace Covid_App.Services.Users
{
    public interface IUserService
    {
        AuthenticationResponse? Authenticate(AuthenticationRequest request);

        CreateUserResponse RegisterUser(CreateUserRequest request);
        
    }
}

