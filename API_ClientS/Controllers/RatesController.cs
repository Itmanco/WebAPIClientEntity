using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using API_ClientS.Data;
using API_ClientS.Models;
using System.Net.Http;
using System.Net.Http.Headers;

namespace API_ClientS.Controllers
{
    public class RatesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public RatesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Rates
        public async Task<IActionResult> Index()
        {
            return View(await _context.Rates.ToListAsync());
        }

        // GET: Rates/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rates = await _context.Rates
                .SingleOrDefaultAsync(m => m.IdRate == id);
            if (rates == null)
            {
                return NotFound();
            }

            return View(rates);
        }

        // GET: Rates/Create
        public IActionResult Create()
        {
            @ViewData["SelectedMovie"] = new Guid("0E984725-C51C-4BF4-9960-E1C80E27ABA1");
            //TODO: Add user logged
            @ViewData["userLogged"] = new Guid("0E984725-C51C-4BF4-9960-E1C80E27ABA0");

            return View();
        }

        // POST: Rates/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdRate,IdUser,IdVideo,Rate,DateTime")] Rates rate)
        {
            if (ModelState.IsValid)
            {
                rate.IdRate = Guid.NewGuid();
                //Start
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(Constants.BaseAPIAddress);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    HttpResponseMessage response = await client.PostAsJsonAsync("api/putcomment?id=" + rate.IdVideo.ToString(), rate);

                    return RedirectToAction("Details", "Movies", new { id = rate.IdVideo.ToString() });
                }
                //--- END
            }
            return View(rate);
        }

        // GET: Rates/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rates = await _context.Rates.SingleOrDefaultAsync(m => m.IdRate == id);
            if (rates == null)
            {
                return NotFound();
            }
            return View(rates);
        }

        // POST: Rates/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("IdRate,IdUser,IdVideo,Rate,DateTime")] Rates rates)
        {
            if (id != rates.IdRate)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(rates);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RatesExists(rates.IdRate))
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
            return View(rates);
        }

        // GET: Rates/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rates = await _context.Rates
                .SingleOrDefaultAsync(m => m.IdRate == id);
            if (rates == null)
            {
                return NotFound();
            }

            return View(rates);
        }

        // POST: Rates/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var rates = await _context.Rates.SingleOrDefaultAsync(m => m.IdRate == id);
            _context.Rates.Remove(rates);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RatesExists(Guid id)
        {
            return _context.Rates.Any(e => e.IdRate == id);
        }
    }
}
