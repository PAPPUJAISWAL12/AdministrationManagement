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
    public class ReceiptsController : Controller
    {
        private readonly FeeManagementSystemContext _context;

        public ReceiptsController(FeeManagementSystemContext context)
        {
            _context = context;
        }

        // GET: Receipts
        public async Task<IActionResult> Index()
        {
            var feeManagementSystemContext = _context.Receipts.Include(r => r.CancelledUser).Include(r => r.EntryUser).Include(r => r.Std);
            return View(await feeManagementSystemContext.ToListAsync());
        }

        // GET: Receipts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Receipts == null)
            {
                return NotFound();
            }

            var receipt = await _context.Receipts
                .Include(r => r.CancelledUser)
                .Include(r => r.EntryUser)
                .Include(r => r.Std)
                .FirstOrDefaultAsync(m => m.Rid == id);
            if (receipt == null)
            {
                return NotFound();
            }

            return View(receipt);
        }

        // GET: Receipts/Create
        public IActionResult Create()
        {
            ViewData["CancelledUserId"] = new SelectList(_context.UserLists, "UserId", "UserId");
            ViewData["EntryUserId"] = new SelectList(_context.UserLists, "UserId", "UserId");
            ViewData["StdId"] = new SelectList(_context.UserLists, "UserId", "UserId");
            return View();
        }

        // POST: Receipts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Rid,ReceiptDate,StdId,ReceiptTime,TotalAmount,Discount,EntryUserId,CancelledDate,CancelledUserId,ReasonForCancelled")] Receipt receipt)
        {
            if (ModelState.IsValid)
            {
                _context.Add(receipt);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CancelledUserId"] = new SelectList(_context.UserLists, "UserId", "UserId", receipt.CancelledUserId);
            ViewData["EntryUserId"] = new SelectList(_context.UserLists, "UserId", "UserId", receipt.EntryUserId);
            ViewData["StdId"] = new SelectList(_context.UserLists, "UserId", "UserId", receipt.StdId);
            return View(receipt);
        }

        // GET: Receipts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Receipts == null)
            {
                return NotFound();
            }

            var receipt = await _context.Receipts.FindAsync(id);
            if (receipt == null)
            {
                return NotFound();
            }
            ViewData["CancelledUserId"] = new SelectList(_context.UserLists, "UserId", "UserId", receipt.CancelledUserId);
            ViewData["EntryUserId"] = new SelectList(_context.UserLists, "UserId", "UserId", receipt.EntryUserId);
            ViewData["StdId"] = new SelectList(_context.UserLists, "UserId", "UserId", receipt.StdId);
            return View(receipt);
        }

        // POST: Receipts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Rid,ReceiptDate,StdId,ReceiptTime,TotalAmount,Discount,EntryUserId,CancelledDate,CancelledUserId,ReasonForCancelled")] Receipt receipt)
        {
            if (id != receipt.Rid)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(receipt);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ReceiptExists(receipt.Rid))
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
            ViewData["CancelledUserId"] = new SelectList(_context.UserLists, "UserId", "UserId", receipt.CancelledUserId);
            ViewData["EntryUserId"] = new SelectList(_context.UserLists, "UserId", "UserId", receipt.EntryUserId);
            ViewData["StdId"] = new SelectList(_context.UserLists, "UserId", "UserId", receipt.StdId);
            return View(receipt);
        }

        // GET: Receipts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Receipts == null)
            {
                return NotFound();
            }

            var receipt = await _context.Receipts
                .Include(r => r.CancelledUser)
                .Include(r => r.EntryUser)
                .Include(r => r.Std)
                .FirstOrDefaultAsync(m => m.Rid == id);
            if (receipt == null)
            {
                return NotFound();
            }

            return View(receipt);
        }

        // POST: Receipts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Receipts == null)
            {
                return Problem("Entity set 'FeeManagementSystemContext.Receipts'  is null.");
            }
            var receipt = await _context.Receipts.FindAsync(id);
            if (receipt != null)
            {
                _context.Receipts.Remove(receipt);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ReceiptExists(int id)
        {
          return (_context.Receipts?.Any(e => e.Rid == id)).GetValueOrDefault();
        }
    }
}
