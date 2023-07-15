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
    public class FeeSheetsController : Controller
    {
        private readonly FeeManagementSystemContext _context;

        public FeeSheetsController(FeeManagementSystemContext context)
        {
            _context = context;
        }

        // GET: FeeSheets
        public async Task<IActionResult> Index()
        {
            var sheetList = await _context.FeeSheetViews.ToListAsync();
            return View(sheetList);
        }

        // GET: FeeSheets/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.FeeSheets == null)
            {
                return NotFound();
            }

            var feeSheet = await _context.FeeSheets
                .Include(f => f.CancelledUser)
                .Include(f => f.EntryUser)
                .Include(f => f.FidNavigation)
                .Include(f => f.Std)
                .FirstOrDefaultAsync(m => m.SheetId == id);
            if (feeSheet == null)
            {
                return NotFound();
            }

            return View(feeSheet);
        }

        // GET: FeeSheets/Create
        public IActionResult Create(int id, int param, decimal ammount)
        {
            var studentList = _context.StudentViews.Where(x => x.Cid == id).ToList();
           
            ViewBag.stdlist = new SelectList(studentList,nameof(StudentView.StdId),nameof(StudentView.FullName));
            

            FeeSheet? sheet = new FeeSheet
            {
                Fid=param,
                Amount=ammount,

            };
           return PartialView("_Create",sheet);
        }

        // POST: FeeSheets/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SheetId,StdId,Fid,Amount,DueDate,EntryUserId,EntryTime,CancelledDate,CancelledUserId,ReasonForCancelled,FeeSheetStatus")] FeeSheet feeSheet)
        {
            feeSheet.EntryUserId = Convert.ToInt32(User.Identity.Name);
            feeSheet.DueDate = DateTime.Today;
            feeSheet.EntryTime = DateTime.Now.ToShortTimeString();
                _context.Add(feeSheet);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));          
           
        }

        // GET: FeeSheets/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.FeeSheets == null)
            {
                return NotFound();
            }

            var feeSheet = await _context.FeeSheets.FindAsync(id);
            if (feeSheet == null)
            {
                return NotFound();
            }
            ViewData["CancelledUserId"] = new SelectList(_context.UserLists, "UserId", "UserId", feeSheet.CancelledUserId);
            ViewData["EntryUserId"] = new SelectList(_context.UserLists, "UserId", "UserId", feeSheet.EntryUserId);
            ViewData["Fid"] = new SelectList(_context.FeeHeaders, "Fid", "Fid", feeSheet.Fid);
            ViewData["StdId"] = new SelectList(_context.Students, "StdId", "StdId", feeSheet.StdId);
            return View(feeSheet);
        }

        // POST: FeeSheets/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("SheetId,StdId,Fid,Amount,DueDate,EntryUserId,EntryTime,CancelledDate,CancelledUserId,ReasonForCancelled,FeeSheetStatus")] FeeSheet feeSheet)
        {
            if (id != feeSheet.SheetId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(feeSheet);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FeeSheetExists(feeSheet.SheetId))
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
            ViewData["CancelledUserId"] = new SelectList(_context.UserLists, "UserId", "UserId", feeSheet.CancelledUserId);
            ViewData["EntryUserId"] = new SelectList(_context.UserLists, "UserId", "UserId", feeSheet.EntryUserId);
            ViewData["Fid"] = new SelectList(_context.FeeHeaders, "Fid", "Fid", feeSheet.Fid);
            ViewData["StdId"] = new SelectList(_context.Students, "StdId", "StdId", feeSheet.StdId);
            return View(feeSheet);
        }

        // GET: FeeSheets/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.FeeSheets == null)
            {
                return NotFound();
            }

            var feeSheet = await _context.FeeSheets
                .Include(f => f.CancelledUser)
                .Include(f => f.EntryUser)
                .Include(f => f.FidNavigation)
                .Include(f => f.Std)
                .FirstOrDefaultAsync(m => m.SheetId == id);
            if (feeSheet == null)
            {
                return NotFound();
            }

            return View(feeSheet);
        }

        // POST: FeeSheets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.FeeSheets == null)
            {
                return Problem("Entity set 'FeeManagementSystemContext.FeeSheets'  is null.");
            }
            var feeSheet = await _context.FeeSheets.FindAsync(id);
            if (feeSheet != null)
            {
                _context.FeeSheets.Remove(feeSheet);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FeeSheetExists(int id)
        {
          return (_context.FeeSheets?.Any(e => e.SheetId == id)).GetValueOrDefault();
        }
    }
}
