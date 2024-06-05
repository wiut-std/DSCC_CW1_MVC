using DSCC_CW1_MVC.Models;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using System.Text;

namespace DSCC_CW1_MVC.Controllers
{
    public class AuthorController : Controller
    {
        private readonly string BaseUrl = "https://localhost:7252/";

        // GET: AuthorController
        public async Task<ActionResult> Index()
        {
            List<Author> authors = new List<Author>();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(BaseUrl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.GetAsync("api/author/getallauthors");
                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    authors = JsonConvert.DeserializeObject<List<Author>>(responseContent);
                }
            }
            return View(authors);
        }

        // GET: AuthorController/Details/5
        public async Task<ActionResult> Details(int id)
        {
            Author author = new Author();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(BaseUrl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var response = await client.GetAsync($"api/author/getauthorbyid/{id}");
                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    author = JsonConvert.DeserializeObject<Author>(responseContent);
                }
            }
            return View(author);
        }

        // GET: AuthorController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AuthorController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(AuthorCreate author)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(BaseUrl);
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    var jsonContent = JsonConvert.SerializeObject(author);
                    var contentString = new StringContent(jsonContent, Encoding.UTF8, "application/json");
                    var response = await client.PostAsync("api/author/createauthor", contentString);

                    if (response.IsSuccessStatusCode)
                    {
                        return RedirectToAction(nameof(Index));
                    }
                }
                return View(author);
            }
            catch
            {
                return View();
            }
        }

        // GET: AuthorController/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            Author author = new Author();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(BaseUrl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var response = await client.GetAsync($"api/author/getauthorbyid/{id}");
                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    author = JsonConvert.DeserializeObject<Author>(responseContent);
                }
            }
            return View(author);
        }

        // POST: AuthorController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, Author author)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(BaseUrl);
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    var jsonContent = JsonConvert.SerializeObject(author);
                    var contentString = new StringContent(jsonContent, Encoding.UTF8, "application/json");
                    var response = await client.PutAsync($"api/author/updateauthor/{id}", contentString);

                    if (response.IsSuccessStatusCode)
                    {
                        return RedirectToAction(nameof(Index));
                    }
                }
                return View(author);
            }
            catch
            {
                return View();
            }
        }

        // GET: AuthorController/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            Author author = new Author();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(BaseUrl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var response = await client.GetAsync($"api/author/getauthorbyid/{id}");
                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    author = JsonConvert.DeserializeObject<Author>(responseContent);
                }
            }
            return View(author);
        }

        // POST: AuthorController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id, IFormCollection collection)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(BaseUrl);
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    var response = await client.DeleteAsync($"api/author/deleteauthor/{id}");

                    if (response.IsSuccessStatusCode)
                    {
                        return RedirectToAction(nameof(Index));
                    }
                }
                return View();
            }
            catch
            {
                return View();
            }
        }
    }
}
