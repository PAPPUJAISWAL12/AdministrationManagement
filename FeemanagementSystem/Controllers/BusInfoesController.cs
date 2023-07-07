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
    public class BusInfoesController : Controller
    {
        private readonly FeeManagementSystemContext _context;

        public BusInfoesController(FeeManagementSystemContext context)
        {
            _context = context;
        }

        // GET: BusInfoes
        public async Task<IActionResult> Index()
        {
              return _context.BusInfos != null ? 
                          View(await _context.BusInfos.ToListAsync()) :
                          Problem("Entity set 'FeeManagementSystemContext.BusInfos'  is null.");
        }

        // GET: BusInfoes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.BusInfos == null)
            {
                return NotFound();
            }

            var busInfo = await _context.BusInfos
                .FirstOrDefaultAsync(m => m.Bid == id);
            if (busInfo == null)
            {
                return NotFound();
            }

            return View(busInfo);
        }

        // GET: BusInfoes/Create
        public IActionResult Create()
        {
            return PartialView();
        }

        // POST: BusInfoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("DestinationAddress,BusFee")] BusInfo busInfo)
        {
            
            
                _context.Add(busInfo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
          
        }

        // GET: BusInfoes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.BusInfos == null)
            {
                return NotFound();
            }

            var busInfo = await _context.BusInfos.FindAsync(id);
            if (busInfo == null)
            {
                return NotFound();
            }
            return PartialView(busInfo);
        }

        // POST: BusInfoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Bid,DestinationAddress,BusFee")] BusInfo busInfo)
        {
            if (id != busInfo.Bid)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(busInfo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BusInfoExists(busInfo.Bid))
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
            return View(busInfo);
        }

        // GET: BusInfoes/Delete/5
       /* public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.BusInfos == null)
            {
                return NotFound();
            }

            var busInfo = await _context.BusInfos
                .FirstOrDefaultAsync(m => m.Bid == id);
            if (busInfo == null)
            {
                return NotFound();
            }

            return View(busInfo);
        }*/

        // POST: BusInfoes/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
          
            if (_context.BusInfos == null)
            {
                return Problem("Entity set 'FeeManagementSystemContext.BusInfos'  is null.");
            }
            var busInfo = await _context.BusInfos.FindAsync(id);
            if (busInfo != null)
            {
                _context.BusInfos.Remove(busInfo);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BusInfoExists(int id)
        {
          return (_context.BusInfos?.Any(e => e.Bid == id)).GetValueOrDefault();
        }
    }
}
