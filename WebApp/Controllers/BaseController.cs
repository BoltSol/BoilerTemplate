using AppDomain.Common;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace WebApp.Controllers
{
    public class BaseController: Controller
    {
        private static Random random = new Random();
        public static string RandomString(int length)
        {
            return new string(Enumerable.Repeat(Constants.AlphaNumeric, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }
        public bool IsAuthenticated
        {
            get
            {
                return User.Identity.IsAuthenticated;
            }
        }
        public long? LoggedinUserId
        {
            get
            {
                var nameIdentifier = string.Empty;
                if (IsAuthenticated)
                    nameIdentifier = User.FindFirstValue(ClaimTypes.NameIdentifier);
                if (!string.IsNullOrEmpty(nameIdentifier)) return long.Parse(nameIdentifier);
                return null;
            }
        }
    }
}
