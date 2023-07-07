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
    public class ReceiptPrintsController : Controller
    {
        private readonly FeeManagementSystemContext _context;

        public ReceiptPrintsController(FeeManagementSystemContext context)
        {
            _context = context;
        }

        // GET: ReceiptPrints
        public async Task<IActionResult> Index()
        {
            var feeManagementSystemContext = _context.ReceiptPrints.Include(r => r.PrintUser).Include(r => r.RidNavigation);
            return View(await feeManagementSystemContext.ToListAsync());
        }

        // GET: ReceiptPrints/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.ReceiptPrints == null)
            {
                return NotFound();
            }

            var receiptPrint = await _context.ReceiptPrints
                .Include(r => r.PrintUser)
                .Include(r => r.RidNavigation)
                .FirstOrDefaultAsync(m => m.PrintId == id);
            if (receiptPrint == null)
            {
                return NotFound();
            }

            return View(receiptPrint);
        }

        // GET: ReceiptPrints/Create
        public IActionResult Create()
        {
            ViewData["PrintUserId"] = new SelectList(_context.UserLists, "UserId", "UserId");
            ViewData["Rid"] = new SelectList(_context.Receipts, "Rid", "Rid");
            return View();
        }

        // POST: ReceiptPrints/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PrintId,Rid,PrintTime,PrintDate,PrintUserId")] ReceiptPrint receiptPrint)
        {
            if (ModelState.IsValid)
            {
                _context.Add(receiptPrint);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["PrintUserId"] = new SelectList(_context.UserLists, "UserId", "UserId", receiptPrint.PrintUserId);
            ViewData["Rid"] = new SelectList(_context.Receipts, "Rid", "Rid", receiptPrint.Rid);
            return View(receiptPrint);
        }

        // GET: ReceiptPrints/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.ReceiptPrints == null)
            {
                return NotFound();
            }

            var receiptPrint = await _context.ReceiptPrints.FindAsync(id);
            if (receiptPrint == null)
            {
                return NotFound();
            }
            ViewData["PrintUserId"] = new SelectList(_context.UserLists, "UserId", "UserId", receiptPrint.PrintUserId);
            ViewData["Rid"] = new SelectList(_context.Receipts, "Rid", "Rid", receiptPrint.Rid);
            return View(receiptPrint);
        }

        // POST: ReceiptPrints/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PrintId,Rid,PrintTime,PrintDate,PrintUserId")] ReceiptPrint receiptPrint)
        {
            if (id != receiptPrint.PrintId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(receiptPrint);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ReceiptPrintExists(receiptPrint.PrintId))
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
            ViewData["PrintUserId"] = new SelectList(_context.UserLists, "UserId", "UserId", receiptPrint.PrintUserId);
            ViewData["Rid"] = new SelectList(_context.Receipts, "Rid", "Rid", receiptPrint.Rid);
            return View(receiptPrint);
        }

        // GET: ReceiptPrints/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.ReceiptPrints == null)
            {
                return NotFound();
            }

            var receiptPrint = await _context.ReceiptPrints
                .Include(r => r.PrintUser)
                .Include(r => r.RidNavigation)
                .FirstOrDefaultAsync(m => m.PrintId == id);
            if (receiptPrint == null)
            {
                return NotFound();
            }

            return View(receiptPrint);
        }

        // POST: ReceiptPrints/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.ReceiptPrints == null)
            {
                return Problem("Entity set 'FeeManagementSystemContext.ReceiptPrints'  is null.");
            }
            var receiptPrint = await _context.ReceiptPrints.FindAsync(id);
            if (receiptPrint != null)
            {
                _context.ReceiptPrints.Remove(receiptPrint);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ReceiptPrintExists(int id)
        {
          return (_context.ReceiptPrints?.Any(e => e.PrintId == id)).GetValueOrDefault();
        }
    }
}
