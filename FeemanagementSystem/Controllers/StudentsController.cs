using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FeemanagementSystem.Models;
using System.Runtime.CompilerServices;
using MimeKit;
using MailKit.Net.Smtp;
using MailKit.Security;
using Org.BouncyCastle.Cms;
using System.Net;

namespace FeemanagementSystem.Controllers
{
    public class StudentsController : Controller
    {
        private readonly FeeManagementSystemContext _context;

        public StudentsController(FeeManagementSystemContext context)
        {
            _context = context;
        }

        // GET: Students
        public IActionResult  Index()
        {
            var classList = _context.Classes.ToList();
            if (classList != null)
            {
                ViewBag.ClassList = new SelectList(classList, nameof(Class.Cid), nameof(Class.Cname));
            }
            else
            {
				return View();
			}

            return View();
        }
        [HttpGet]
        public IActionResult StudentList(int Cid)
        {
           
            var stdlist = _context.StudentViews.ToList();
			
			return PartialView("_StudentList",stdlist.Where(x=>x.Cid==Cid).ToList());
        }
		// GET: Students/Details/5
		public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Students == null)
            {
                return NotFound();
            }

            var student = await _context.Students
                .Include(s => s.BidNavigation)
                .Include(s => s.CidNavigation)
                .Include(s => s.User)
                .FirstOrDefaultAsync(m => m.StdId == id);
            if (student == null)
            {
                return NotFound();
            }

            return View(student);
        }

        // GET: Students/Create
        public IActionResult Create()
        {
            List<Class> classes = _context.Classes.ToList();
            ViewBag.ClassList = new SelectList(classes, nameof(Class.Cid), nameof(Class.Cname));

            List<BusInfo> busList = _context.BusInfos.ToList();            
          	ViewBag.BusList = new SelectList(busList, nameof(BusInfo.Bid), nameof(BusInfo.DestinationAddress));		      
            return PartialView("_Create");
        }

        // POST: Students/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("StdId,Cid,Bid,UserId,RollNo,UserEmail,Upassword,FullName,Phone,UserAddress,UserRoleType")] StudentEdit studentEdit)
		{
            
            studentEdit.UserRoleType = "Student";
                UserList userList = new UserList
                {
                    UserEmail = studentEdit.UserEmail,
                    UserAddress = studentEdit.UserAddress,
                    Upassword = studentEdit.Upassword,
                    FullName = studentEdit.FullName,
                    Phone = studentEdit.Phone,
                    UserRoleType = studentEdit.UserRoleType
                };
                _context.Add(userList);
                await _context.SaveChangesAsync();

                Student student = new Student
                {
                    Bid = studentEdit.Bid,
                    Cid = studentEdit.Cid,
                    RollNo = studentEdit.RollNo,
                    UserId = userList.UserId
                };

                _context.Add(student);
                await _context.SaveChangesAsync();
                return Content("success");
           
          
        }

        // GET: Students/Edit/5
        public IActionResult Edit(int? id)
        {
            

            var student =  _context.StudentViews.Where(x=>x.StdId==id).FirstOrDefault();
     
            if (student == null)
            {
                return NotFound();
            }
          
            return PartialView("_Edit",student);
        }

        // POST: Students/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id,[Bind("StdId,Cid,Bid,UserId,RollNo,UserEmail,Upassword,FullName,Phone,UserAddress,UserRoleType")] StudentEdit student)
        {
            if (id != student.StdId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(student);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StudentExists(student.StdId))
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
            ViewData["Bid"] = new SelectList(_context.BusInfos, "Bid", "Bid", student.Bid);
            ViewData["Cid"] = new SelectList(_context.Classes, "Cid", "Cid", student.Cid);
            ViewData["UserId"] = new SelectList(_context.UserLists, "UserId", "UserId", student.UserId);
            return View(student);
        }

       /* // GET: Students/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Students == null)
            {
                return NotFound();
            }

            var student = await _context.Students
                .Include(s => s.BidNavigation)
                .Include(s => s.CidNavigation)
                .Include(s => s.User)
                .FirstOrDefaultAsync(m => m.StdId == id);
            if (student == null)
            {
                return NotFound();
            }

            return View(student);
        }
*/
        // POST: Students/Delete/5
        [HttpGet]       
        public async Task<IActionResult> Delete(int id)
        {
            
            var student =  _context.Students.Where(x=>x.StdId==id).FirstOrDefault();            
            if (student != null)
            {
                _context.Students.Remove(student);
            }            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StudentExists(int id)
        {
          return (_context.Students?.Any(e => e.StdId == id)).GetValueOrDefault();
        }
    }
}
