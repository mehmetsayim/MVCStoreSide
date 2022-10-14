using Microsoft.AspNetCore.Identity;

namespace MVCStoreeWeb
{
        public class CustomIdentityErrorDescriber : IdentityErrorDescriber
        {
            public override IdentityError DuplicateUserName(string userName) => new IdentityError { Code = "DuplicateUserName", Description = $"\"{userName}\" kullanıcı adı kullanılmaktadır." };
            public override IdentityError InvalidUserName(string userName) => new IdentityError { Code = "InvalidUserName", Description = "Geçersiz kullanıcı adı." };
            public override IdentityError DuplicateEmail(string email) => new IdentityError { Code = "DuplicateEmail", Description = $"\"{email}\" başka bir kullanıcı tarafından kullanılmaktadır." };
            public override IdentityError InvalidEmail(string email) => new IdentityError { Code = "InvalidEmail", Description = "Geçersiz email." };
           // public override IdentityError DublicateRoleName(string role) => new IdentityError { Code = "Zaten Rolü var  ", Description = $"Bu üye zaten '{role}' rolündedir!." };

    }
}

