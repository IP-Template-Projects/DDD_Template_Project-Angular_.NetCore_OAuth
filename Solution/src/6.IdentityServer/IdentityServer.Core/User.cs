using System;
using Microsoft.AspNetCore.Identity;

namespace IdentityServer.Core
{
    public class User : IdentityUser<Guid>
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        //public IEnumerable<Order> Orders { get; set; }
    }
}