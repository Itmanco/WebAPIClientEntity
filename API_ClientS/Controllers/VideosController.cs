using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using API_ClientS.Data;
using API_ClientS.Models;
using System.Net.Http;
using System.Net.Http.Headers;
using Microsoft.AspNetCore.Http;

namespace API_ClientS.Controllers
{
    public class VideosController : Controller
    {
        private readonly ApplicationDbContext _context;

        public VideosController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Videos
        public async Task<IActionResult> Index()
        {
            if (HttpContext.Session.GetString("Token") == null)
            {
                return View("/Views/Account/AccessDenied.cshtml");
            }


            //var applicationDbContext = _context.Videos.Include(v => v.IdUserNavigation);
            //return View(await applicationDbContext.ToListAsync());

            Videos[] reports = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Constants.BaseAPIAddress);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("Token"));
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response;
                response = await client.GetAsync("api/allvideos");
                if (response.IsSuccessStatusCode)
                {
                    reports = await response.Content.ReadAsAsync<Videos[]>();
                }
                return View(reports);
            }
        }

        // GET: Videos/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (HttpContext.Session.GetString("Token") == null)
            {
                return View("/Views/Account/AccessDenied.cshtml");
            }

            if (id == null)
            {
                return NotFound();
            }
            Videos res = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Constants.BaseAPIAddress);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("Token"));
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response;
                response = await client.GetAsync("api/getvideobyid?id=" + id.ToString());
                if (response.IsSuccessStatusCode)
                {
                    res = await response.Content.ReadAsAsync<Videos>();
                }
            }

            if (res == null)
            {
                return NotFound();
            }

            return View(res);
        }

        // GET: Videos/Create
        public IActionResult Create()
        {
            if (HttpContext.Session.GetString("Token") == null)
            {
                return View("/Views/Account/AccessDenied.cshtml");
            }
            //ViewData["IdUser"] = new SelectList(_context.Set<Users>(), "IdUser", "IdUser");

            //TODO: Add user logged
            @ViewData["userLogged"] = new Guid("0E984725-C51C-4BF4-9960-E1C80E27ABA0");

            return View();
        }

        // POST: Videos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdVideo,IdUser,Title,Description,DateTime")] Videos videos)
        {
            if (HttpContext.Session.GetString("Token") == null)
            {
                return View("/Views/Account/AccessDenied.cshtml");
            }
            videos.IdVideo = Guid.NewGuid();
            if (ModelState.IsValid)
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(Constants.BaseAPIAddress);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("Token"));
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    HttpResponseMessage response = await client.PostAsJsonAsync("api/putvideo?id=" + videos.IdVideo.ToString(), videos);
                    return RedirectToAction("Index", "Videos");
                }
            }
            //ViewData["IdUser"] = new SelectList(_context.Set<Users>(), "IdUser", "IdUser", videos.IdUser);
            return View(videos);
        }

        // GET: Videos/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (HttpContext.Session.GetString("Token") == null)
            {
                return View("/Views/Account/AccessDenied.cshtml");
            }
            if (id == null)
            {
                return NotFound();
            }

            //ViewData["IdUser"] = new SelectList(_context.Set<Users>(), "IdUser", "IdUser", videos.IdUser);

            Videos res = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Constants.BaseAPIAddress);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("Token"));
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response;
                response = await client.GetAsync("api/getvideobyid?id=" + id.ToString());
                if (response.IsSuccessStatusCode)
                {
                    res = await response.Content.ReadAsAsync<Videos>();
                }
            }

            if (res == null)
            {
                return NotFound();
            }

            return View(res);
        }

        // POST: Videos/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("IdVideo,IdUser,Title,Description,DateTime")] Videos videos)
        {
            if (HttpContext.Session.GetString("Token") == null)
            {
                return View("/Views/Account/AccessDenied.cshtml");
            }

            if (id != videos.IdVideo)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {

                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(Constants.BaseAPIAddress);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("Token"));
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    HttpResponseMessage response = await client.PutAsJsonAsync("api/updatevideo?id=" + videos.IdVideo.ToString(), videos);
                }
            }
            ViewData["IdUser"] = new SelectList(_context.Set<Users>(), "IdUser", "IdUser", videos.IdUser);
            return View(videos);
        }

        // GET: Videos/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (HttpContext.Session.GetString("Token") == null)
            {
                return View("/Views/Account/AccessDenied.cshtml");
            }

            if (id == null)
            {
                return NotFound();
            }

            Videos videos = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Constants.BaseAPIAddress);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("Token"));
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response;
                response = await client.GetAsync("api/getvideobyid?id=" + id.ToString());
                if (response.IsSuccessStatusCode)
                {
                    videos = await response.Content.ReadAsAsync<Videos>();
                }
            }
            if (videos == null)
            {
                return NotFound();
            }

            return View(videos);
        }

        // POST: Videos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            if (HttpContext.Session.GetString("Token") == null)
            {
                return View("/Views/Account/AccessDenied.cshtml");
            }

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Constants.BaseAPIAddress);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("Token"));
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.DeleteAsync("api/deletevideobyid?id=" + id.ToString());

                return RedirectToAction("Index", "Videos");
            }
        }

    }
}
