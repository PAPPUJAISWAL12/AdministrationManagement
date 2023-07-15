using FeemanagementSystem.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace FeemanagementSystem.Controllers
{
	public class HomeController : Controller
	{
		private readonly FeeManagementSystemContext _context;
		public HomeController(FeeManagementSystemContext context)
		{
			_context = context;
		}

		[HttpGet]
		public IActionResult Index()
		{
			if (User.Identity.IsAuthenticated)
			{
				return RedirectToAction("Index","Accounts");
			}
			else
			{
				return View();
			}
		}

		[HttpPost]
		public async Task<IActionResult> Index(UserList us)
		{
			var a = _context.UserLists.ToList();
			
			UserList? u = a.Where(u => u.UserEmail.ToUpper().Equals(us.UserEmail.ToUpper()) && u.Upassword.Equals(us.Upassword)).FirstOrDefault();
			if(u!= null)
			{
				var claims = new List<Claim>
				{
					new Claim(ClaimTypes.Name,u.UserId.ToString()),
					new Claim(ClaimTypes.Email, u.UserEmail.ToString()),
					new Claim(ClaimTypes.Role,u.UserRoleType.ToString()),
					new Claim("UserName",u.FullName.ToString())
				};
				
				var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
				await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(identity),new AuthenticationProperties { IsPersistent=true});
				return RedirectToAction("Index", "Accounts");
			}
			else
			{
				
				ViewData["ErrorMessage"] = "Login Failled. Try Again!";
				return View(new UserList());
				//ViewBag.ErrorMessage = "Login Failled. Try Again!";
			}
			
		}


		[Authorize]
		public async Task<IActionResult> Logout()
		{
			await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
			return RedirectToAction("Index");
		}

	}
}
