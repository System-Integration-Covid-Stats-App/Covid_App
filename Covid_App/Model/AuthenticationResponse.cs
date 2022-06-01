using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Covid_App.Entities;

namespace Covid_App.Model
{
    public class AuthenticationResponse
    {
        public long Id { get; set; }
        public string Username { get; set; }
        public string Token { get; set; }
        public AuthenticationResponse(User user, string token)
        {
            Id = user.UserId;
            Username = user.Username;
            Token = token;
        }
    }
}
