using Application.UserApp;
using Domain.Dtos;
using Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace StoreApp.Areas.Admin.Controllers
{
    [Area("admin")]
    [Authorize(Roles = "admin")]
    public class UserController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly IUserApp _userApp;

        public UserController(UserManager<User> userManager, IUserApp userApp)
        {
            _userManager = userManager;
            _userApp = userApp;
        }

        [Route("/user")]
        [Route("/admin/user")]
        public IActionResult Index()
        {
            var users = _userApp.GetUsers().ToList();
            return View(users);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(UserDto userDto)
        {
            if (ModelState.IsValid)
            {
                var user = new User()
                {
                    UserName = userDto.Email,
                    Email = userDto.Email,
                    EmailConfirmed = true,
                    Street = userDto.Street,
                    City = userDto.City,
                    PostalCode = userDto.PostalCode
                };
                var result = _userManager.CreateAsync(user, userDto.Password);
                await _userManager.AddToRoleAsync(user, "user");
                return RedirectToAction(nameof(Index));
            }
            return View(userDto);
        }
    }
}
