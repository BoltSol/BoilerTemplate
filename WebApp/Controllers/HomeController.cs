using AppDomain.Common;
using AppService.Roles;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Models;

namespace WebApp.Controllers
{
    [Authorize(Roles = Constants.Role_Admin_User)]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IRoleAppService _roleAppService;

        public HomeController(IRoleAppService roleAppService, ILogger<HomeController> logger)
        {
            this._roleAppService = roleAppService;
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            var test = await _roleAppService.GetAllRoles();
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
