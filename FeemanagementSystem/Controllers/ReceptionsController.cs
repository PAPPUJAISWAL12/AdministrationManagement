using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FeemanagementSystem.Models;

namespace FeemanagementSystem.Controllers
{
    public class ReceptionsController : Controller
    {
        private readonly FeeManagementSystemContext _context;

        public ReceptionsController(FeeManagementSystemContext context)
        {
            _context = context;
        }

        // GET: Receptions
        public async Task<IActionResult> Index()
        {
            var recepList = _context.Receptions.ToList();
            return View(recepList);
        }
        public  IActionResult CancelList()
        {
            var recepList = _context.Receptions.ToList();
            return View(recepList);
        }

        // GET: Receptions/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Receptions == null)
            {
                return NotFound();
            }

            var reception = await _context.Receptions
                .Include(r => r.CancelledUser)
                .Include(r => r.User)
                .FirstOrDefaultAsync(m => m.Rid == id);
            if (reception == null)
            {
                return NotFound();
            }

            return View(reception);
        }

        // GET: Receptions/Create
        public IActionResult Create()
        {
          
			return PartialView("_Create");
        }

        // POST: Receptions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]       
        public async Task<IActionResult> Create([Bind("Rid,PersonName,EntryDate,EntryTime,Purpose,PersonAddress,Phone,UserId,CancelledDate,CancelledUserId,FiscalYear,ResonForCancell,ReceptionStatus")] Reception rec)
        {           
            rec.EntryDate = Convert.ToDateTime(DateTime.Today.ToShortDateString());
            rec.EntryTime = DateTime.UtcNow.AddMinutes(345).ToShortTimeString();
            rec.UserId = Convert.ToInt32(User.Identity.Name);			          
                _context.Add(rec);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));   
        }

        // GET: Receptions/Edit/5
        public IActionResult Edit(int? id)
        {
            var reception = _context.Receptions.Where(x => x.Rid == id).FirstOrDefault();
           
            return PartialView("_Edit",reception);
        }

        // POST: Receptions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public async Task<IActionResult> Edit(int id, [Bind("Rid,PersonName,EntryDate,EntryTime,Purpose,PersonAddress,Phone,UserId,CancelledDate,CancelledUserId,FiscalYear,ResonForCancell,ReceptionStatus")] Reception reception)
        {
            
            reception.CancelledDate = DateTime.Today;
            reception.CancelledUserId = Convert.ToInt32(User.Identity.Name);
			_context.Update(reception);
			await _context.SaveChangesAsync();
			return RedirectToAction(nameof(Index));
			
        }    
        

        private bool ReceptionExists(int id)
        {
          return (_context.Receptions?.Any(e => e.Rid == id)).GetValueOrDefault();
        }
    }
}
