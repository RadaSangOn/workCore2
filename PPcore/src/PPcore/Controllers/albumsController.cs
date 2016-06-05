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
using Newtonsoft.Json;

namespace PPcore.Controllers
{
    public class albumsController : Controller
    {
        private readonly PalangPanyaDBContext _context;
        private IConfiguration _configuration;
        private IHostingEnvironment _env;

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
            return View(await album.OrderByDescending(m => m.album_date).ToListAsync());
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
            ViewBag.FormAction = "Edit";
            ViewBag.album_code = album.album_code;
            return View(album);
        }

        // POST: albums/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("album_code,album_desc,album_name,created_by,album_date,id,rowversion,x_log,x_note,x_status")] album album)
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

        [HttpGet]
        public IActionResult ListAlbumPhoto(string albumCode)
        {
            var uploads = Path.Combine(_env.WebRootPath, _configuration.GetSection("Paths").GetSection("images_album").Value);
            uploads = Path.Combine(uploads, albumCode);
            string[] fileEntries = Directory.GetFiles(uploads);
            List<photo> p = new List<photo>();
            string fi;
            foreach (string fileName in fileEntries)
            {
                fi = Path.Combine(albumCode,Path.GetFileName(fileName));
                fi = Path.Combine(_configuration.GetSection("Paths").GetSection("images_album").Value, fi);
                p.Add(new photo { image_code = "", fileName = fi });
            }
            string pjson = JsonConvert.SerializeObject(p);
            //return Json(new { result = "success", uploads = uploads, photos = pjson });
            return Json(pjson);
        }
    }




    public class photo
    {
        public string image_code;
        public string fileName;
    }
}
