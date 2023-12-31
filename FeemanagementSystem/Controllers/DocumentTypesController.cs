﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FeemanagementSystem.Models;

namespace FeemanagementSystem.Controllers
{
    public class DocumentTypesController : Controller
    {
        private readonly FeeManagementSystemContext _context;

        public DocumentTypesController(FeeManagementSystemContext context)
        {
            _context = context;
        }

        // GET: DocumentTypes
        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> GetDocumentTypeList()
        {
            List<DocumentType> documentTypeList =await _context.DocumentTypes.ToListAsync();
            return PartialView("_GetDocumentTypeList", documentTypeList);
        }

        // GET: DocumentTypes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.DocumentTypes == null)
            {
                return NotFound();
            }

            var documentType = await _context.DocumentTypes
                .FirstOrDefaultAsync(m => m.TypeId == id);
            if (documentType == null)
            {
                return NotFound();
            }

            return View(documentType);
        }

        // GET: DocumentTypes/Create
        public IActionResult Create()
        {
            return PartialView("_Create");
        }

        // POST: DocumentTypes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TypeId,DocumetCat")] DocumentType documentType)
        {
            if (ModelState.IsValid)
            {
                _context.Add(documentType);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(documentType);
        }

        // GET: DocumentTypes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.DocumentTypes == null)
            {
                return NotFound();
            }

            var documentType = await _context.DocumentTypes.FindAsync(id);
            if (documentType == null)
            {
                return NotFound();
            }
            return PartialView("_Edit",documentType);
        }

        // POST: DocumentTypes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("TypeId,DocumetCat")] DocumentType documentType)
        {
            
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(documentType);
                    await _context.SaveChangesAsync();
                    return Content("UpdateS");
                }
                catch (DbUpdateConcurrencyException)
                {
                    
                    return Content("update Failled");
                }
               
            }
            return NotFound();
        }

        // GET: DocumentTypes/Delete/5
        public async Task<IActionResult> Delete(int id)
        {

			
            var documentType = _context.DocumentTypes.Where(x => x.TypeId == id).FirstOrDefault();
         
			if (documentType != null)
			{
				_context.DocumentTypes.Remove(documentType);
			}

			await _context.SaveChangesAsync();
			return RedirectToAction(nameof(Index));

		}

      

        private bool DocumentTypeExists(int id)
        {
          return (_context.DocumentTypes?.Any(e => e.TypeId == id)).GetValueOrDefault();
        }
    }
}
