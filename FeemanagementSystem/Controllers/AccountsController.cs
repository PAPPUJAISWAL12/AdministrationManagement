using Microsoft.AspNetCore.Mvc;

namespace FeemanagementSystem.Controllers
{
    public class AccountsController : Controller
    {
        public IActionResult Index()
        {
            if (User.IsInRole("Admin"))
            {
                return RedirectToAction("Index", "Admin");
            }
            else if(User.IsInRole("Student"))
            {
                return RedirectToAction("Index", "UserLists");
            }
            else if(User.IsInRole("Receptionist"))
            {
                return RedirectToAction("Index", "Receptions");
            }
            else
            {
                return View();
            }
        }
    }
}
