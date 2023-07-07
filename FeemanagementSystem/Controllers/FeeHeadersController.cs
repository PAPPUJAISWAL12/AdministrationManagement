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
    public class FeeHeadersController : Controller
    {
        private readonly FeeManagementSystemContext _context;

        public FeeHeadersController(FeeManagementSystemContext context)
        {
            _context = context;
        }

        // GET: FeeHeaders
        public async Task<IActionResult> Index()
        {
            var feeheaderlist = await _context.FeeHeaders.ToListAsync();
            return View(feeheaderlist);
        }

        // GET: FeeHeaders/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.FeeHeaders == null)
            {
                return NotFound();
            }

            var feeHeader = await _context.FeeHeaders
                .Include(f => f.CancelledUser)
                .Include(f => f.EntryUser)
                .FirstOrDefaultAsync(m => m.Fid == id);
            if (feeHeader == null)
            {
                return NotFound();
            }

            return View(feeHeader);
        }

        // GET: FeeHeaders/Create
        public IActionResult Create()
        {
            FeeHeader header = new FeeHeader
            {
                EntryUserId = Convert.ToInt32(User.Identity.Name)
            };
            return PartialView("_Create",header);
        }

        // POST: FeeHeaders/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Fid,Title,EntryUserId,CancelledDate,CancelledUserId,ReasonForCancelled")] FeeHeader feeHeader)
        {
            feeHeader.EntryUserId = Convert.ToInt32(User.Identity.Name);
            
                _context.Add(feeHeader);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            
        }

        // GET: FeeHeaders/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.FeeHeaders == null)
            {
                return NotFound();
            }

            var feeHeader = await _context.FeeHeaders.FindAsync(id);
            if (feeHeader == null)
            {
                return NotFound();
            }
           
           
            return View(feeHeader);
        }

        // POST: FeeHeaders/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Fid,Title,EntryUserId,CancelledDate,CancelledUserId,ReasonForCancelled")] FeeHeader feeHeader)
        {
           
            feeHeader.CancelledDate = DateTime.Today;
            feeHeader.CancelledUserId = Convert.ToInt32(User.Identity.Name);
                             _context.Update(feeHeader);
                    await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
           
        }        
        private bool FeeHeaderExists(int id)
        {
          return (_context.FeeHeaders?.Any(e => e.Fid == id)).GetValueOrDefault();
        }
    }
}
