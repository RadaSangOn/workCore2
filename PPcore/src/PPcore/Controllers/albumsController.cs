using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PPcore.Models;

namespace PPcore.Controllers
{
    public class albumsController : Controller
    {
        private readonly PalangPanyaDBContext _context;

        public albumsController(PalangPanyaDBContext context)
        {
            _context = context;    
        }

        // GET: albums
        public async Task<IActionResult> Index()
        {
            return View(await _context.album.ToListAsync());
        }

        // GET: albums/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var album = await _context.album.SingleOrDefaultAsync(m => m.album_code == id);
            if (album == null)
            {
                return NotFound();
            }

            return View(album);
        }

        // GET: albums/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: albums/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("album_code,album_desc,album_name,created_by,created_date,id,rowversion,x_log,x_note,x_status")] album album)
        {
            if (ModelState.IsValid)
            {
                _context.Add(album);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(album);
        }

        // GET: albums/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var album = await _context.album.SingleOrDefaultAsync(m => m.album_code == id);
            if (album == null)
            {
                return NotFound();
            }
            return View(album);
        }

        // POST: albums/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("album_code,album_desc,album_name,created_by,created_date,id,rowversion,x_log,x_note,x_status")] album album)
        {
            if (id != album.album_code)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(album);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!albumExists(album.album_code))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index");
            }
            return View(album);
        }

        // GET: albums/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var album = await _context.album.SingleOrDefaultAsync(m => m.album_code == id);
            if (album == null)
            {
                return NotFound();
            }

            return View(album);
        }

        // POST: albums/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var album = await _context.album.SingleOrDefaultAsync(m => m.album_code == id);
            _context.album.Remove(album);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool albumExists(string id)
        {
            return _context.album.Any(e => e.album_code == id);
        }
    }
}
