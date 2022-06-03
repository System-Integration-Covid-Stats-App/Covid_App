using Covid_App.Entities;
using Covid_App.Model;
using Covid_App.Model.Request;
using Covid_App.Model.Response;

namespace Covid_App.Services.Users
{
    public interface IUserService
    {
        CreateUserResponse RegisterUser(UserRequest request);

        User UpdateUser(int userId,UserRequest request);
    }
}

