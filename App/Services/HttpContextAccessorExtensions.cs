using Microsoft.AspNetCore.Http;
using System;
using System.Security.Claims;

namespace App.Services
{
    public interface IHttpContextAccessorExtensions
    {
        bool IsRoleAdmin();
        int GetId();
        string GetAccess();
        string GetUserFullName();
        string GetUserPhone();
        string GetUserIp();
    }

    public class HttpContextAccessorExtensions : IHttpContextAccessorExtensions
    {
        private readonly IHttpContextAccessor accessor;

        public HttpContextAccessorExtensions(IHttpContextAccessor accessor)
        {
            this.accessor = accessor;
        }

        public bool IsRoleAdmin()
        {
            var role = accessor.HttpContext.User.FindFirst(ClaimTypes.Role);
            return role.Value.Contains("1;");
        }

        public int GetId()
        {
            var r = accessor.HttpContext.User?.FindFirst(ClaimTypes.Sid);
            return r == null ? 0 : Convert.ToInt32(r.Value);
        }

        public string GetAccess()
        {
            var role = accessor.HttpContext.User.FindFirst(ClaimTypes.Role);
            return role.Value;
        }

        public string GetUserFullName()
        {
            var role = accessor.HttpContext.User.FindFirst(ClaimTypes.Name);
            return role.Value;
        }

        public string GetUserPhone()
        {
            var role = accessor.HttpContext.User.FindFirst(ClaimTypes.MobilePhone);
            return role.Value;
        }

        public string GetUserIp()
        {
            return accessor.HttpContext.Connection.RemoteIpAddress.ToString();
        }
    }
}
