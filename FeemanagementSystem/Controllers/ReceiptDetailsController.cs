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
    public class ReceiptDetailsController : Controller
    {
        private readonly FeeManagementSystemContext _context;

        public ReceiptDetailsController(FeeManagementSystemContext context)
        {
            _context = context;
        }

        // GET: ReceiptDetails
        public async Task<IActionResult> Index()
        {
            var feeManagementSystemContext = _context.ReceiptDetails.Include(r => r.RidNavigation).Include(r => r.Sheet);
            return View(await feeManagementSystemContext.ToListAsync());
        }

        // GET: ReceiptDetails/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.ReceiptDetails == null)
            {
                return NotFound();
            }

            var receiptDetail = await _context.ReceiptDetails
                .Include(r => r.RidNavigation)
                .Include(r => r.Sheet)
                .FirstOrDefaultAsync(m => m.DetailId == id);
            if (receiptDetail == null)
            {
                return NotFound();
            }

            return View(receiptDetail);
        }

        // GET: ReceiptDetails/Create
        public IActionResult Create()
        {
            ViewData["Rid"] = new SelectList(_context.Receipts, "Rid", "Rid");
            ViewData["SheetId"] = new SelectList(_context.FeeSheets, "SheetId", "SheetId");
            return View();
        }

        // POST: ReceiptDetails/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("DetailId,Rid,SheetId,Amount")] ReceiptDetail receiptDetail)
        {
            if (ModelState.IsValid)
            {
                _context.Add(receiptDetail);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Rid"] = new SelectList(_context.Receipts, "Rid", "Rid", receiptDetail.Rid);
            ViewData["SheetId"] = new SelectList(_context.FeeSheets, "SheetId", "SheetId", receiptDetail.SheetId);
            return View(receiptDetail);
        }

        // GET: ReceiptDetails/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.ReceiptDetails == null)
            {
                return NotFound();
            }

            var receiptDetail = await _context.ReceiptDetails.FindAsync(id);
            if (receiptDetail == null)
            {
                return NotFound();
            }
            ViewData["Rid"] = new SelectList(_context.Receipts, "Rid", "Rid", receiptDetail.Rid);
            ViewData["SheetId"] = new SelectList(_context.FeeSheets, "SheetId", "SheetId", receiptDetail.SheetId);
            return View(receiptDetail);
        }

        // POST: ReceiptDetails/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("DetailId,Rid,SheetId,Amount")] ReceiptDetail receiptDetail)
        {
            if (id != receiptDetail.DetailId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(receiptDetail);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ReceiptDetailExists(receiptDetail.DetailId))
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
            ViewData["Rid"] = new SelectList(_context.Receipts, "Rid", "Rid", receiptDetail.Rid);
            ViewData["SheetId"] = new SelectList(_context.FeeSheets, "SheetId", "SheetId", receiptDetail.SheetId);
            return View(receiptDetail);
        }

        // GET: ReceiptDetails/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.ReceiptDetails == null)
            {
                return NotFound();
            }

            var receiptDetail = await _context.ReceiptDetails
                .Include(r => r.RidNavigation)
                .Include(r => r.Sheet)
                .FirstOrDefaultAsync(m => m.DetailId == id);
            if (receiptDetail == null)
            {
                return NotFound();
            }

            return View(receiptDetail);
        }

        // POST: ReceiptDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.ReceiptDetails == null)
            {
                return Problem("Entity set 'FeeManagementSystemContext.ReceiptDetails'  is null.");
            }
            var receiptDetail = await _context.ReceiptDetails.FindAsync(id);
            if (receiptDetail != null)
            {
                _context.ReceiptDetails.Remove(receiptDetail);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ReceiptDetailExists(int id)
        {
          return (_context.ReceiptDetails?.Any(e => e.DetailId == id)).GetValueOrDefault();
        }
    }
}
