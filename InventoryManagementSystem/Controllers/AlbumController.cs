using InventoryManagementSystem.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http; // Required for HttpClient

namespace InventoryManagementSystem.Controllers
{
    public class AlbumController : Controller
    {
        // Ideally, move this to appsettings.json, but this works for now
        private const string BaseUrl = "https://jsonplaceholder.typicode.com/albums";
        private readonly Uri _uri = new(BaseUrl);
        private readonly HttpClient _httpClient;

        public AlbumController()
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = _uri;
        }

        // GET: AlbumController
        public async Task<IActionResult> Index()
        {
            // Use await instead of .Result to prevent deadlocks
            HttpResponseMessage response = await _httpClient.GetAsync(_uri);

            if (response.IsSuccessStatusCode)
            {
                string json = await response.Content.ReadAsStringAsync();
                List<Album>? albums = JsonConvert.DeserializeObject<List<Album>>(json);
                return View(albums);
            }

            return View(new List<Album>()); // Better to return empty list than null
        }
    }
}