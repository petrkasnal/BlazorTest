using Microsoft.AspNetCore.Components.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace WebApplication6.Data
{
    public class DumbAuthenticationStateProvider : AuthenticationStateProvider
    {
        public static ClaimsPrincipal User
            => new ClaimsPrincipal(new ClaimsIdentity(UserClaims, "Dumb Auth Type"));

        public static Claim[] UserClaims
            => new[]{
                    new Claim(ClaimTypes.Sid, "024672e0-250a-46fc-bd35-1902974cf9e1"),
                    new Claim(ClaimTypes.Name, "Normal User"),
                    new Claim(ClaimTypes.NameIdentifier, "Normal User"),
                    new Claim(ClaimTypes.Email, "user@user,com"),
                    new Claim(ClaimTypes.Role, "User")
            };
        public static ClaimsPrincipal Visitor
            => new ClaimsPrincipal(new ClaimsIdentity(VisitorClaims, "Dumb Auth Type"));

        public static Claim[] VisitorClaims
            => new[]{
                    new Claim(ClaimTypes.Sid, "324672e0-250a-46fc-bd35-1902974cf9e1"),
                    new Claim(ClaimTypes.Name, "Visitor"),
                    new Claim(ClaimTypes.NameIdentifier, "Normal Visitor"),
                    new Claim(ClaimTypes.Email, "visitor@user,com"),
                    new Claim(ClaimTypes.Role, "Visitor")
            };

        bool _switch;

        public override Task<AuthenticationState> GetAuthenticationStateAsync()
            => Task.FromResult(_switch
                ? new AuthenticationState(User)
                : new AuthenticationState(Visitor)
                );

        public Task<AuthenticationState> ChangeIdentity()
        {
            _switch = !_switch;
            var task = this.GetAuthenticationStateAsync();
            this.NotifyAuthenticationStateChanged(task);
            return task;
        }

    }
}