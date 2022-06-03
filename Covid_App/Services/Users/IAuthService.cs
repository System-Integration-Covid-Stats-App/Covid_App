using Covid_App.Model.Request;
using Covid_App.Model.Response;

namespace Covid_App.Services.Users;

public interface IAuthService
{
    AuthenticationResponse? Authenticate(AuthenticationRequest request);
}