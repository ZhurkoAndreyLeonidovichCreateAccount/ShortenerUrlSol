using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ShortenerUrl.BLL.Interfaces;
using ShortenerUrl.BLL.Models;
using ShortenerUrl.BLL.Providers;
using ShortenerUrl.DAL.Entity;
using ShortenerUrl.DAL.Interfaces;

namespace ShortenerUrlBLL.Controllers
{
    public class ShortendUrlsController : Controller
    {
        
       
        private readonly IServiceShortenerUrl _service;

        public ShortendUrlsController(IServiceShortenerUrl service)
        {
            
            _service = service;
        }

        // GET: ShortendUrls
        public async Task<IActionResult> Index()
        {
            
            ViewData["Error"] = TempData["Error"];
            var allShortendUrlView = await _service.IndexAsync();
            return View(allShortendUrlView);
        }

        

        // GET: ShortendUrls/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ShortendUrls/Create
       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(string longUrl)
        {
           
                var error = await _service.CreateAsync(longUrl);
                if (!string.IsNullOrEmpty(error))
                {
                    TempData["Error"] = error;
                }
                return RedirectToAction(nameof(Index));            
            
        }
        

        //GET: ShortendUrls/Edit/5
        public async Task<IActionResult> Edit(int id)
        {       
            var shortendUrl = await _service.GetShortendUrlAsync(id,false);
            if (shortendUrl is null)
            {
                return NotFound();
            }
          
            return View(shortendUrl);
        }

        // POST: ShortendUrls/Edit/5
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,LongUrl,Code,DateOfCreation,NumberOfTransitions")] ShortendUrl shortendUrl)
        {
            if (id != shortendUrl.Id || await _service.GetShortendUrlAsync(id,false) is null)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                await _service.UpdateShortendUrlAsync(shortendUrl);
                
                return RedirectToAction(nameof(Index));
            }
            return View(shortendUrl);

        }

        //GET: ShortendUrls/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
       
            var shortendUrl = await _service.GetShortendUrlAsync(id, false);
            if (shortendUrl is null)
            {
                return NotFound();
            }

            return View(shortendUrl);
        }

        // POST: ShortendUrls/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _service.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> GoToSite(string code)
        {
            var shortend = await _service.GoToSite(code);
            if(shortend is not null)
            {            
                return Redirect(shortend.LongUrl);
            }
            return RedirectToAction(nameof(Index));

        }
    }
}
