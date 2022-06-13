using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using CoffeeStore.Models;
using CoffeeStore.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using Microsoft.EntityFrameworkCore;

namespace CoffeeStore.Controllers
{
    public class HomeController : Controller
    {
        private readonly CoffeeStoreDbContext dbContext;
        private readonly IWebHostEnvironment webHostEnvironment;
        private readonly CoffeeStoreDbContext _context;        
        private ICoffeeStoreRepository repository;
        public int PageSize = 3;

        public HomeController(ICoffeeStoreRepository repo, CoffeeStoreDbContext context, IWebHostEnvironment hostEnvironment)
        {
            repository = repo;
            dbContext = context;
            webHostEnvironment = hostEnvironment;
            _context = context;
        }

        public IActionResult Index(string genre, int coffeePage = 1)
            => View(new CoffeeListViewModel
            {
                Coffees = repository.Coffees
                    .Where(p => genre == null || p.Genre == genre)
                    .OrderBy(p => p.CoffeeID)
                    .Skip((coffeePage - 1) * PageSize)
                    .Take(PageSize),
                PagingInfo = new PagingInfo
                {
                    CurrentPage = coffeePage,
                    ItemsPerPage = PageSize,
                    TotalItems = genre == null ?
                    repository.Coffees.Count() :
                    repository.Coffees.Where(e =>
                    e.Genre == genre).Count()
                },
                CurrentGenre = genre
            });

        public IActionResult Created()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Created(CoffeeViewModels model)
        {
            if (ModelState.IsValid)
            {
                string uniqueFileName = UploadedFile(model);

                Coffee coffee = new Coffee
                {

                    Title = model.Title,
                   
                    Genre = model.Genre,
                    Price = model.Price,
                    
                };
                dbContext.Add(coffee);
                await dbContext.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View();
        }
        private string UploadedFile(CoffeeViewModels model)
        {
            string uniqueFileName = null;

            if (model.ImageCoffee != null)
            {
                string uploadsFolder = Path.Combine(webHostEnvironment.WebRootPath, "Images");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + model.ImageCoffee.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    model.ImageCoffee.CopyTo(fileStream);
                }
            }
            return uniqueFileName;
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dienThoai = await _context.Coffees
                .FirstOrDefaultAsync(m => m.CoffeeID == id);
            if (dienThoai == null)
            {
                return NotFound();
            }

            return View(dienThoai);
        }

        // POST: SMartPhones/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var sMartPhone = await _context.Coffees.FindAsync(id);
            _context.Coffees.Remove(sMartPhone);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SMartPhoneExists(long id)
        {
            return _context.Coffees.Any(e => e.CoffeeID == id);
        }
    }
}
