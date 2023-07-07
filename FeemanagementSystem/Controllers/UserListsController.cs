using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FeemanagementSystem.Models;
using System.Security.Cryptography;

namespace FeemanagementSystem.Controllers
{
    public class UserListsController : Controller
    {
        private readonly FeeManagementSystemContext _context;

        public UserListsController(FeeManagementSystemContext context)
        {
            _context = context;
        }

        // GET: UserLists
        public IActionResult Index()
        {
              return View();
        }


		public IActionResult Profile()
		{
			return View();
		}


		public IActionResult UserList()
        {
            List<UserList> userList=_context.UserLists.Where(x=>x.UserRoleType !="Student").ToList();
            return PartialView("_UserList",userList);
        }

        // GET: UserLists/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.UserLists == null)
            {
                return NotFound();
            }

            var userList = await _context.UserLists
                .FirstOrDefaultAsync(m => m.UserId == id);
            if (userList == null)
            {
                return NotFound();
            }

            return View(userList);
        }

        // GET: UserLists/Create
        public IActionResult Create()
        {
               return PartialView("_Create");
        }

        // POST: UserLists/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("UserId,UserEmail,Upassword,FullName,Phone,UserAddress,LoginStatus,UserRoleType")] UserList userList)
        {
            if (ModelState.IsValid)
            {
                _context.Add(userList);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return PartialView("_Create",userList);
        }

        // GET: UserLists/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.UserLists == null)
            {
                return NotFound();
            }

            var userList = await _context.UserLists.FindAsync(id);
            if (userList == null)
            {
                return NotFound();
            }
            return PartialView("_Edit",userList);
        }

        // POST: UserLists/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("UserId,UserEmail,Upassword,FullName,Phone,UserAddress,LoginStatus,UserRoleType")] UserList userList)
        {
            if (id != userList.UserId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(userList);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserListExists(userList.UserId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(userList);
        }

        

        // POST: UserLists/Delete/5
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
           
            var userList =  _context.UserLists.Where(x=>x.UserId==id).FirstOrDefault();
            if (userList != null)
            {
                _context.UserLists.Remove(userList);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

       

        public IActionResult OldPassword()
        {          
            return View();
        }
        [HttpPost]
        public IActionResult OldPassword(UserList u)
        {
			var password =  _context.UserLists.Where(x => x.UserId==Convert.ToInt32(User.Identity.Name) && x.Upassword==u.Upassword).FirstOrDefault();
            if (password != null)
            {
                return Content("Success");
            }
            else
            {
                return Content("wrong");
            }
			
        }
        public IActionResult ChangePassword()
        {
            var userl = _context.UserLists.Where(x => x.UserId == Convert.ToInt32(User.Identity.Name)).First();
            return PartialView("_ChangePassword",userl);
        }
        [HttpPost]
        public async Task<IActionResult> ChangePassword(int id,[Bind("UserId, UserEmail, Upassword, FullName, Phone, UserAddress, LoginStatus, UserRoleType")] UserList userList)
		{
            if (userList != null)
            {
                _context.Update(userList);
                await _context.SaveChangesAsync();
                return Content("success");
            }
            else
            {
                return Content("failled");
            }		
		}

        private bool UserListExists(int id)
        {
          return (_context.UserLists?.Any(e => e.UserId == id)).GetValueOrDefault();
        }
    }
}
