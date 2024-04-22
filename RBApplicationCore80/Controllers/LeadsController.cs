using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.OutputCaching;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using RBApplicationCore80.Data;
using RBApplicationCore80.Models;
using RBApplicationCore80.ViewModel;

namespace RBApplicationCore80.Controllers
{
    public class LeadsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment webHostEnvironment;
        static string base64String = null;

        public LeadsController(ApplicationDbContext context , IWebHostEnvironment hostEnvironment)
        {
            _context = context;
            webHostEnvironment = hostEnvironment;
        }

        // GET: Leads
        [OutputCache(PolicyName = "PeoplePolicy")]
        public async Task<IActionResult> Index()
        {
            return View(await _context.SalesLead.ToListAsync());
        }

        // GET: Leads/Details/5
        //[ResponseCache(Duration = 180, Location = ResponseCacheLocation.Any, VaryByQueryKeys = new string[] { "id" })]
        //[ResponseCache(Duration = 60)]
        [OutputCache(PolicyName = "CachePost")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var salesLeadEntity = await _context.SalesLead
                .FirstOrDefaultAsync(m => m.Id == id);

            var speakerViewModel = new SpeakerViewModel()
            {
                Id = salesLeadEntity.Id,
                SpeakerName = salesLeadEntity.SpeakerName,
                Qualification = salesLeadEntity.Qualification,
                Experience = salesLeadEntity.Experience,
                SpeakingDate = salesLeadEntity.SpeakingDate,
                SpeakingTime = salesLeadEntity.SpeakingTime,
                Venue = salesLeadEntity.Venue,
                ExistingImage = salesLeadEntity.UserImage
            };

            if (salesLeadEntity == null)
            {
                return NotFound();
            }

            return View(salesLeadEntity);
        }

        // GET: Leads/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Leads/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(SpeakerViewModel model)
        {
            
            if (ModelState.IsValid)
            {
                string uniqueFileName = ProcessUploadedFile(model);
                string adharImagebyte = ConvertImagetoBase64(model);


                SalesLeadEntity speaker = new SalesLeadEntity
                {
                    FirstName= model.FirstName,
                    LastName= model.LastName,
                    Mobile=model.Mobile,
                    Email=model.Email,
                    Source=model.Source,
                    SpeakerName = model.SpeakerName,
                    Qualification = model.Qualification,
                    Experience = model.Experience,
                    SpeakingDate = model.SpeakingDate,
                    SpeakingTime = model.SpeakingTime,
                    Venue = model.Venue,
                    UserImage = uniqueFileName,
                    AadharNumber = adharImagebyte
                };


                _context.Add(speaker);
                await _context.SaveChangesAsync();                
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }
        private string ProcessUploadedFile(SpeakerViewModel model)
        {
            string uniqueFileName = null;

            if (model.SpeakerPicture != null)
            {
                string uploadsFolder = Path.Combine(webHostEnvironment.WebRootPath, "Uploads");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + model.SpeakerPicture.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    model.SpeakerPicture.CopyTo(fileStream);
                }
            }

            return uniqueFileName;
        }

        public byte[] UseStreamDotReadMethod(Stream stream)
        {
            byte[] bytes;
            List<byte> totalStream = new();
            byte[] buffer = new byte[32];
            int read;
            while ((read = stream.Read(buffer, 0, buffer.Length)) > 0)
            {
                totalStream.AddRange(buffer.Take(read));
            }
            bytes = totalStream.ToArray();
            return bytes;
        }

        private string ConvertImagetoBase64 (SpeakerViewModel model)
        {
            string uniqueFileName = null;
            byte[] bytes;
            IFormFile file = model.SpeakerPicture;

            if (file == null || file.Length == 0)
            {
                //return BadRequest("Invalid file");
            }

            if (model.SpeakerPicture != null)
            {
                try
                {
                    // Read the content of the IFormFile into a byte[]
                    using (MemoryStream ms = new MemoryStream())
                    {
                        file.CopyTo(ms);
                        byte[] fileBytes = ms.ToArray();                        
                        //byte[] fileBytes = UseStreamDotReadMethod(ms);
                        base64String = Convert.ToBase64String(fileBytes);                        
                    }
                }
                catch (Exception ex){
                    
                }
            }
            return base64String; 
        }

        // GET: Leads/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var salesLeadEntity = await _context.SalesLead.FindAsync(id);
            if (salesLeadEntity == null)
            {
                return NotFound();
            }
            return View(salesLeadEntity);
        }

        // POST: Leads/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FirstName,LastName,Mobile,Email,Source,PanNumber,AadharNumber,UserImage,SpeakerName,Qualification,Experience,SpeakingDate,SpeakingTime,Venue")] SalesLeadEntity salesLeadEntity)
        {
            if (id != salesLeadEntity.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(salesLeadEntity);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SalesLeadEntityExists(salesLeadEntity.Id))
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
            return View(salesLeadEntity);
        }

        // GET: Leads/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var salesLeadEntity = await _context.SalesLead
                .FirstOrDefaultAsync(m => m.Id == id);
            if (salesLeadEntity == null)
            {
                return NotFound();
            }

            return View(salesLeadEntity);
        }

        // POST: Leads/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var salesLeadEntity = await _context.SalesLead.FindAsync(id);
            if (salesLeadEntity != null)
            {
                _context.SalesLead.Remove(salesLeadEntity);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SalesLeadEntityExists(int id)
        {
            return _context.SalesLead.Any(e => e.Id == id);
        }
    }
}
