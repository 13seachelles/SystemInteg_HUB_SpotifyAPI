using HUB.Models;
using HUB.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace HUB.Controllers
{
    public class SpotifyAPIController : Controller
    {
        private readonly ISpotifyAccountService _spotifyAccountService;
        private readonly IConfiguration _configuration;
        private readonly ISpotifyService _spotifyService;
        public SpotifyAPIController(
            ISpotifyAccountService spotifyAccountService,
            IConfiguration configuration,
            ISpotifyService spotifyService)

        {
            _spotifyAccountService = spotifyAccountService;
            _configuration = configuration;
            _spotifyService = spotifyService;
        }

        public async Task<IActionResult> Index()
        {
            var newReleases = await GetReleases();
            return View(newReleases);
        }
        public async Task<IEnumerable<Release>> GetReleases()
        {
            try
            {
                var token = await _spotifyAccountService.GetToken(_configuration["Spotify:ClientId"], _configuration["Spotify:ClientSecret"]);
                Debug.WriteLine($"Token: {token}");

                var newReleases = await _spotifyService.GetNewReleases("FR", 20, token);
                Debug.WriteLine($"New Releases Count: {newReleases.Count()}");

                return newReleases;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error fetching releases: {ex.Message}");
                return Enumerable.Empty<Release>();
            }
        }

        // GET: SpotifyAPIController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: SpotifyAPIController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: SpotifyAPIController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: SpotifyAPIController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: SpotifyAPIController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: SpotifyAPIController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: SpotifyAPIController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
