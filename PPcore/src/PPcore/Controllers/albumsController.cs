using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PPcore.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System.IO;
using Microsoft.Net.Http.Headers;

namespace PPcore.Controllers
{
    public class albumsController : Controller
    {
        private readonly PalangPanyaDBContext _context;
        private IConfiguration _configuration;
        private IHostingEnvironment _env;

        private void prepareViewBag()
        {
            ViewBag.images_album = _configuration.GetSection("Paths").GetSection("images_album").Value;

            ViewBag.sex = new SelectList(new[] { new { Value = "F", Text = "Female" }, new { Value = "M", Text = "Male" }, new { Value = "A", Text = "Alternative" } }, "Value", "Text", "F");
            ViewBag.cid_type = new SelectList(new[] { new { Value = "C", Text = "???????????" }, new { Value = "H", Text = "????????????????" }, new { Value = "P", Text = "Passport" } }, "Value", "Text", "F");
            ViewBag.marry_status = new SelectList(new[] { new { Value = "N", Text = "???" }, new { Value = "Y", Text = "????" } }, "Value", "Text");
            ViewBag.zone = new SelectList(new[] { new { Value = "N", Text = "?????" }, new { Value = "E", Text = "????????" }, new { Value = "W", Text = "???????" }, new { Value = "S", Text = "???" }, new { Value = "L", Text = "??????????????????" } }, "Value", "Text");

            ViewBag.mem_group = new SelectList(_context.mem_group.OrderBy(g => g.mem_group_code), "mem_group_code", "mem_group_desc");
            ViewBag.mem_type = new SelectList(_context.mem_type.OrderBy(t => t.mem_group_code).OrderBy(t => t.mem_type_code), "mem_type_code", "mem_type_desc", "3  ");
            ViewBag.mem_level = new SelectList(_context.mem_level.OrderBy(t => t.mlevel_code), "mlevel_code", "mlevel_desc", "3  ");
            ViewBag.mem_status = new SelectList(_context.mem_status.OrderBy(s => s.mstatus_code), "mstatus_code", "mstatus_desc", "3  ");

            ViewBag.ini_country = new SelectList(_context.ini_country.OrderBy(c => c.country_code), "country_code", "country_desc", "1");
        }

        public albumsController(PalangPanyaDBContext context, IConfiguration configuration, IHostingEnvironment env)
        {
            _context = context;
            _configuration = configuration;
            _env = env;
        }

        // GET: albums
        public async Task<IActionResult> Index()
        {
            var album = _context.album;
            ViewBag.countRecords = album.Count();
            return View(await album.OrderByDescending(m => m.rowversion).ToListAsync());
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
            ViewBag.FormAction = "Create";
            ViewBag.album_code = DateTime.Now.ToString("yyMMddhhmmss");
            return View();
        }

        // POST: albums/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("album_code,album_desc,album_name,created_by,album_date,rowversion")] album album)
        {
            album.x_status = "Y";
            //album.album_code = DateTime.Now.ToString("yyMMddhhmmss");
            album.created_by = "Administrator";

            _context.Add(album);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        // GET: albums/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var album = await _context.album.SingleOrDefaultAsync(m => m.id == new Guid(id));
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

        [HttpPost]
        public async Task<IActionResult> UploadAlbumPhoto(ICollection<IFormFile> file, string albumCode)
        {
            var uploads = Path.Combine(_env.WebRootPath, _configuration.GetSection("Paths").GetSection("images_album").Value);
            uploads = Path.Combine(uploads, albumCode);
            Directory.CreateDirectory(uploads);

            //var fileName = DateTime.Now.ToString("ddhhmmss") + "_";
            var fileName = "";
            foreach (var fi in file)
            {
                if (fi.Length > 0)
                {
                    fileName += ContentDispositionHeaderValue.Parse(fi.ContentDisposition).FileName.Trim('"');
                    fileName = fileName.Substring(0, (fileName.Length <= 50 ? fileName.Length : 50)) + Path.GetExtension(fileName);
                    using (var SourceStream = fi.OpenReadStream())
                    {
                        using (var fileStream = new FileStream(Path.Combine(uploads, fileName), FileMode.Create))
                        {
                            await SourceStream.CopyToAsync(fileStream);
                        }
                    }
                }
            }
            return Json(new { result = "success", uploads = uploads, fileName = fileName });
        }
    }
}
