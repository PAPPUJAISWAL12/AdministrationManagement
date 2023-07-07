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
    public class FeeStructuresController : Controller
    {
        private readonly FeeManagementSystemContext _context;

        public FeeStructuresController(FeeManagementSystemContext context)
        {
            _context = context;
        }

        // GET: FeeStructures
        public IActionResult Index()
        {
            ViewBag.Classlist = new SelectList(_context.Classes, nameof(Class.Cid), nameof(Class.Cname));
            return View();
        }
        public async Task<IActionResult> StructureList(int Cid)
        {
            var structurList = await _context.FeeStructureViews.ToListAsync();
            return PartialView("_StructureList", structurList.Where(x=>x.Cid==Cid).ToList());
        }

        // GET: FeeStructures/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.FeeStructures == null)
            {
                return NotFound();
            }

            var feeStructure = await _context.FeeStructures
                .Include(f => f.CancelledUser)
                .Include(f => f.CidNavigation)
                .Include(f => f.EntryUser)
                .Include(f => f.FidNavigation)
                .FirstOrDefaultAsync(m => m.FsId == id);
            if (feeStructure == null)
            {
                return NotFound();
            }

            return View(feeStructure);
        }

        // GET: FeeStructures/Create
        public IActionResult Create(int id)
        {
            return Json(id);
            ViewData["Cid"] = new SelectList(_context.Classes, "Cid", nameof(Class.Cname));
            ViewBag.feeHeader = new SelectList(_context.FeeHeaders, "Fid", nameof(FeeHeader.Title));
            return PartialView("_Create");
        }

        // POST: FeeStructures/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("FsId,Cid,Fid,Amount,DueDate,EntryUserId,EntryTime")] FeeStructure feeStructure)
        {
            feeStructure.EntryTime=DateTime.Now.ToShortTimeString();
            feeStructure.DueDate = DateTime.Today;
            feeStructure.EntryUserId = Convert.ToInt32(User.Identity.Name);
                _context.Add(feeStructure);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            
        }

        // GET: FeeStructures/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.FeeStructures == null)
            {
                return NotFound();
            }

            var feeStructure = await _context.FeeStructures.FindAsync(id);
            if (feeStructure == null)
            {
                return NotFound();
            }
            ViewData["CancelledUserId"] = new SelectList(_context.UserLists, "UserId", "UserId", feeStructure.CancelledUserId);
            ViewData["Cid"] = new SelectList(_context.Classes, "Cid", "Cid", feeStructure.Cid);
            ViewData["EntryUserId"] = new SelectList(_context.UserLists, "UserId", "UserId", feeStructure.EntryUserId);
            ViewData["Fid"] = new SelectList(_context.FeeHeaders, "Fid", "Fid", feeStructure.Fid);
            return View(feeStructure);
        }

        // POST: FeeStructures/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("FsId,Cid,Fid,Amount,DueDate,EntryUserId,EntryTime,CancelledDate,CancelledUserId,ReasonForCancelled")] FeeStructure feeStructure)
        {
            if (id != feeStructure.FsId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(feeStructure);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FeeStructureExists(feeStructure.FsId))
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
            ViewData["CancelledUserId"] = new SelectList(_context.UserLists, "UserId", "UserId", feeStructure.CancelledUserId);
            ViewData["Cid"] = new SelectList(_context.Classes, "Cid", "Cid", feeStructure.Cid);
            ViewData["EntryUserId"] = new SelectList(_context.UserLists, "UserId", "UserId", feeStructure.EntryUserId);
            ViewData["Fid"] = new SelectList(_context.FeeHeaders, "Fid", "Fid", feeStructure.Fid);
            return View(feeStructure);
        }

        // GET: FeeStructures/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.FeeStructures == null)
            {
                return NotFound();
            }

            var feeStructure = await _context.FeeStructures
                .Include(f => f.CancelledUser)
                .Include(f => f.CidNavigation)
                .Include(f => f.EntryUser)
                .Include(f => f.FidNavigation)
                .FirstOrDefaultAsync(m => m.FsId == id);
            if (feeStructure == null)
            {
                return NotFound();
            }

            return View(feeStructure);
        }

        // POST: FeeStructures/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.FeeStructures == null)
            {
                return Problem("Entity set 'FeeManagementSystemContext.FeeStructures'  is null.");
            }
            var feeStructure = await _context.FeeStructures.FindAsync(id);
            if (feeStructure != null)
            {
                _context.FeeStructures.Remove(feeStructure);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FeeStructureExists(int id)
        {
          return (_context.FeeStructures?.Any(e => e.FsId == id)).GetValueOrDefault();
        }
    }
}
