using System;
using Microsoft.AspNetCore.Identity;

namespace IdentityServer.Core.UserManagement
{
    public class User : IdentityUser<Guid>
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

    }
}