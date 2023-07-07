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
    public class ProgramInfoesController : Controller
    {
        private readonly FeeManagementSystemContext _context;

        public ProgramInfoesController(FeeManagementSystemContext context)
        {
            _context = context;
        }

        // GET: ProgramInfoes
        public async Task<IActionResult> Index()
        {
            var feeManagementSystemContext = _context.ProgramInfos.Include(p => p.CancelledUser).Include(p => p.User);
            return View(await feeManagementSystemContext.ToListAsync());
        }

        // GET: ProgramInfoes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.ProgramInfos == null)
            {
                return NotFound();
            }

            var programInfo = await _context.ProgramInfos
                .Include(p => p.CancelledUser)
                .Include(p => p.User)
                .FirstOrDefaultAsync(m => m.Pid == id);
            if (programInfo == null)
            {
                return NotFound();
            }

            return View(programInfo);
        }

        // GET: ProgramInfoes/Create
        public IActionResult Create()
        {
           
            ViewData["UserId"] = new SelectList(_context.UserLists, "UserId", nameof(UserList.FullName));
            return PartialView("_Create");
        }

        // POST: ProgramInfoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Pid,Pname,Pdescription,Venue,StartDate,StartTime,EndDate,EndTime,UserId,EntryDate,CancelledUserId,CancelledDate,ReasonForCancell")] ProgramInfo programInfo)
        {
            if (ModelState.IsValid)
            {
                _context.Add(programInfo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CancelledUserId"] = new SelectList(_context.UserLists, "UserId", "UserId", programInfo.CancelledUserId);
            ViewData["UserId"] = new SelectList(_context.UserLists, "UserId", "UserId", programInfo.UserId);
            return View(programInfo);
        }

        // GET: ProgramInfoes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.ProgramInfos == null)
            {
                return NotFound();
            }

            var programInfo = await _context.ProgramInfos.FindAsync(id);
            if (programInfo == null)
            {
                return NotFound();
            }
            ViewData["CancelledUserId"] = new SelectList(_context.UserLists, "UserId", "UserId", programInfo.CancelledUserId);
            ViewData["UserId"] = new SelectList(_context.UserLists, "UserId", "UserId", programInfo.UserId);
            return View(programInfo);
        }

        // POST: ProgramInfoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Pid,Pname,Pdescription,Venue,StartDate,StartTime,EndDate,EndTime,UserId,EntryDate,CancelledUserId,CancelledDate,ReasonForCancell")] ProgramInfo programInfo)
        {
            if (id != programInfo.Pid)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(programInfo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProgramInfoExists(programInfo.Pid))
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
            ViewData["CancelledUserId"] = new SelectList(_context.UserLists, "UserId", "UserId", programInfo.CancelledUserId);
            ViewData["UserId"] = new SelectList(_context.UserLists, "UserId", "UserId", programInfo.UserId);
            return View(programInfo);
        }

        // GET: ProgramInfoes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.ProgramInfos == null)
            {
                return NotFound();
            }

            var programInfo = await _context.ProgramInfos
                .Include(p => p.CancelledUser)
                .Include(p => p.User)
                .FirstOrDefaultAsync(m => m.Pid == id);
            if (programInfo == null)
            {
                return NotFound();
            }

            return View(programInfo);
        }

        // POST: ProgramInfoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.ProgramInfos == null)
            {
                return Problem("Entity set 'FeeManagementSystemContext.ProgramInfos'  is null.");
            }
            var programInfo = await _context.ProgramInfos.FindAsync(id);
            if (programInfo != null)
            {
                _context.ProgramInfos.Remove(programInfo);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProgramInfoExists(int id)
        {
          return (_context.ProgramInfos?.Any(e => e.Pid == id)).GetValueOrDefault();
        }
    }
}
