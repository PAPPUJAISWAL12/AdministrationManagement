using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FeemanagementSystem.Models;
using System.Diagnostics;
using System.Numerics;
using Microsoft.CodeAnalysis;
using System.Xml.Linq;

namespace FeemanagementSystem.Controllers
{
    public class UploadFilesController : Controller
    {
        private readonly FeeManagementSystemContext _context;
		private readonly IWebHostEnvironment _env;
		public UploadFilesController(FeeManagementSystemContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        // GET: UploadFiles
        public async Task<IActionResult> Index()
        {
            List<UploadFileView> fileList = await _context.UploadFileViews.ToListAsync();

            return View(fileList);
        }
        public IActionResult GetProfile()
        {
            UploadFileView? uploadFile = _context.UploadFileViews.Where(x => x.UserId == Convert.ToInt32(User.Identity.Name)).FirstOrDefault();            
            return PartialView("_GetProfile", uploadFile);
        }
        // GET: UploadFiles/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.UploadFiles == null)
            {
                return NotFound();
            }

            var uploadFile = await _context.UploadFiles
                .Include(u => u.Doc)
                .Include(u => u.Type)
                .FirstOrDefaultAsync(m => m.UploadId == id);
            if (uploadFile == null)
            {
                return NotFound();
            }

            return View(uploadFile);
        }

        // GET: UploadFiles/Create
        public IActionResult Create()
        {		

			List<DocumentType> documentTypes = _context.DocumentTypes.FromSqlRaw("select * from documentType where TypeId NOT IN(select UploadFileView.TypeId from UploadFileView where UserId ="+Convert.ToInt32(User.Identity.Name)+" )").ToList();
           
            if(documentTypes != null && documentTypes.Count>0)
            {
				ViewBag.TypeId = new SelectList(documentTypes, nameof(DocumentType.TypeId), nameof(DocumentType.DocumetCat));
				return PartialView("_Create");
            }
            else
            {
                return NotFound();
            }

			
        }

        // POST: UploadFiles/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([FromForm][Bind("FileUpload,UploadId,DocId,TypeId,UserId")]UploadFileEdit fileEdit)
        {
           
			try
            {                   
				if (fileEdit.FileUpload != null && fileEdit.FileUpload.Length > 0 && fileEdit.FileUpload.Length < 1600000)
                {
                    string fileName = "upimage" + Guid.NewGuid().ToString() + Path.GetExtension(fileEdit.FileUpload.FileName);
                    string imgpath = Path.Combine(_env.WebRootPath, "documentImage", fileName);
					using (FileStream filestream = new FileStream(imgpath, FileMode.Create))
                    {
						fileEdit.FileUpload.CopyTo(filestream);
					}
					fileEdit.DocFile = fileName;
                  
				}
                else
                {
                    return Content("largefilesize");
                    //Please select your file less than 1 mb

                }

                Models.Document doc =new Models.Document
				{
					UserId = Convert.ToInt32(User.Identity.Name)
				};
				_context.Add(doc);
				await _context.SaveChangesAsync();

				UploadFile upfile = new UploadFile
                {
                    DocId = doc.DocId,
                    TypeId = fileEdit.TypeId,
                    DocFile = fileEdit.DocFile
                };
                _context.Add(upfile);
                await _context.SaveChangesAsync();
                return Content("Success");
            }
            catch
            {
                return Content("Error");
            }
			
        }
            
       /* //UserUpdate
        public IActionResult UserUpdate()
        {
			var uploadFile = _context.UploadFileViews.Where(x => x.UserId == Convert.ToInt32(User.Identity.Name));
            return PartialView();
		}*/



        // GET: UploadFiles/Edit/5
        public IActionResult Edit( int id)
        {
            var uploadFile = _context.UploadFileViews.Where(x => x.UserId == Convert.ToInt32(User.Identity.Name) && x.DocumetCat=="Profile").FirstOrDefault();           
           
            if (uploadFile == null)
            {
                return NotFound();
            }
           
            return PartialView("_Edit",uploadFile);
        }
		public IActionResult Slccerti(int id)
		{
			var uploadFile = _context.UploadFileViews.Where(x => x.UserId == Convert.ToInt32(User.Identity.Name) && x.DocumetCat == "S.L.C Certificate").FirstOrDefault();

			if (uploadFile == null)
			{
				return NotFound();
			}

			return PartialView("_Slccerti", uploadFile);
		}

		public IActionResult Citizenship(int id)
		{
			var uploadFile = _context.UploadFileViews.Where(x => x.UserId == Convert.ToInt32(User.Identity.Name) && x.DocumetCat == "Citizenship").FirstOrDefault();

			if (uploadFile == null)
			{
				return NotFound();
			}

			return PartialView("_Citizenship", uploadFile);
		}

        public IActionResult PlushTwo(int id)
		{
			var uploadFile = _context.UploadFileViews.Where(x => x.UserId == Convert.ToInt32(User.Identity.Name) && x.DocumetCat == "+2 Certificate").FirstOrDefault();

			if (uploadFile == null)
			{
				return NotFound();
			}

			return PartialView("_PlushTwo", uploadFile);
		}

		public IActionResult OtherTraining(int id)
		{
			var uploadFile = _context.UploadFileViews.Where(x => x.UserId == Convert.ToInt32(User.Identity.Name) && x.DocumetCat == "+2 Certificate").FirstOrDefault();

			if (uploadFile == null)
			{
				return NotFound();
			}

			return PartialView("_OtherTraining", uploadFile);
		}



		// POST: UploadFiles/Edit/5
		// To protect from overposting attacks, enable the specific properties you want to bind to.
		// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
        public async Task<IActionResult> Edit(int id,[Bind("UploadId,DocId,TypeId,FileUpload")] UploadFileView uploadFile)
        {
            id = uploadFile.UploadId;
          
            if (id != uploadFile.UploadId)
            {
                return NotFound();
            }
            
			try
            {
				if (uploadFile.FileUpload != null && uploadFile.FileUpload.Length > 0 && uploadFile.FileUpload.Length < 1600000)
				{
					string fileName = "updateimage" + Guid.NewGuid().ToString() + Path.GetExtension(uploadFile.FileUpload.FileName);
					string imgpath = Path.Combine(_env.WebRootPath, "documentImage", fileName);
					using (FileStream filestream = new FileStream(imgpath, FileMode.Create))
					{
						uploadFile.FileUpload.CopyTo(filestream);
					}
					uploadFile.DocFile = fileName;

				}
				else
				{
					return Content("largefilesize");
					//Please select your file less than 1 mb

				}
				UploadFile file = new UploadFile()
				{
                    UploadId=id,
					DocId = uploadFile.DocId,
					TypeId = uploadFile.TypeId,
					DocFile = uploadFile.DocFile
				};
				_context.Update(file);
                await _context.SaveChangesAsync();
                return Content("success");
               
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UploadFileExists(uploadFile.UploadId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
        }

        // GET: UploadFiles/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.UploadFiles == null)
            {
                return NotFound();
            }

            var uploadFile = await _context.UploadFiles
                .Include(u => u.Doc)
                .Include(u => u.Type)
                .FirstOrDefaultAsync(m => m.UploadId == id);
            if (uploadFile == null)
            {
                return NotFound();
            }

            return View(uploadFile);
        }

        // POST: UploadFiles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.UploadFiles == null)
            {
                return Problem("Entity set 'FeeManagementSystemContext.UploadFiles'  is null.");
            }
            var uploadFile = await _context.UploadFiles.FindAsync(id);
            if (uploadFile != null)
            {
                _context.UploadFiles.Remove(uploadFile);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UploadFileExists(int id)
        {
          return (_context.UploadFiles?.Any(e => e.UploadId == id)).GetValueOrDefault();
        }
    }
}
