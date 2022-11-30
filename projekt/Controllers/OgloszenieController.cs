using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.Net.Http.Headers;
using Newtonsoft.Json;
using NuGet.Common;
using projekt.Data.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Net.Http.Headers;
using PagedList;
using NuGet.Packaging.Signing;
using System.Diagnostics.CodeAnalysis;

namespace projekt.Controllers
{
    public class OgloszenieController : Controller
    {
        private readonly DatabaseDbContext _db;

        public OgloszenieController(DatabaseDbContext db)
        {
            _db = db;
        }

        [HttpGet]
        public IActionResult GetList(string sortOrder, string currentFilter, string searchString, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;

            if (page == null || page <= 0)
            {
                page = 1;
            }
            ViewBag.CurrentPage = page;
            

            var ogloszenia = from s in _db.Ogloszenia
                           select s;

            if (!String.IsNullOrEmpty(searchString))
            {
                ogloszenia = ogloszenia.Where(s => s.Tytul.Contains(searchString)
                                       || s.Opis.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "name_desc":
                    ogloszenia = ogloszenia.OrderByDescending(s => s.Tytul);
                    break;
                case "Date":
                    ogloszenia = ogloszenia.OrderBy(s => s.DataStworzenia);
                    break;
                case "date_desc":
                    ogloszenia = ogloszenia.OrderByDescending(s => s.DataStworzenia);
                    break;
                case "name_asc":
                    ogloszenia = ogloszenia.OrderBy(s => s.Tytul);
                    break;
                default:
                    ogloszenia = ogloszenia.OrderBy(s => s.Tytul);
                    break;
            }

            int pageSize = 2;
            int pageNumber = (page ?? 1);

            List<Ogloszenie> ogloszeniaList = ogloszenia.ToList();
            List<Ogloszenie> ogloszeniaToShow = new();

            int i = (pageNumber - 1) * pageSize;
            for (int j=0; j<pageSize && i+j<ogloszeniaList.Count; j++)
            {
                ogloszeniaToShow.Add(ogloszeniaList[i+j]);
            }

            return View(ogloszeniaToShow);
        }

        //[HttpGet]
        //public async Task<IActionResult> AccessCreate()
        //{
        //    var jwt = Request.Cookies["jwtCookie"];

        //    using (var httpClient = new HttpClient())
        //    {
        //        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jwt);
        //        using (var response = await httpClient.GetAsync("https://localhost:7162/Ogloszenie/GetUzytkownikToCreate")) // change API URL to yours 
        //        {
        //            if (response.StatusCode == System.Net.HttpStatusCode.OK)
        //            {
        //                string apiResponse = await response.Content.ReadAsStringAsync();
        //                return RedirectToAction("Create");
        //            }

        //            if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
        //            {
        //                return RedirectToAction("Zaloguj", "Konto");//, new { message = "Please Login again" });
        //            }
        //        }
        //    }

        //    return View();
        //}

        ////[Authorize]
        ////public Uzytkownik GetUzytkownikToCreate()
        ////{
        ////    Uzytkownik u = new();
        ////    string authHeader = this.HttpContext.Request.Headers["Authorization"];
        ////    var stream = "[encoded jwt]";
        ////    var handler = new JwtSecurityTokenHandler();
        ////    var jsonToken = handler.ReadToken(stream);
        ////    var tokenS = jsonToken as JwtSecurityToken;

        ////    return u;
        ////}
        //[Authorize]
        //public string GetUzytkownikToCreate()
        //{
        //    Uzytkownik u = new();
        //    string token = "";
        //    string authHeader = Request.Headers[HeaderNames.Authorization];
        //    if (AuthenticationHeaderValue.TryParse(authHeader, out var headerValue))
        //    {
        //        // we have a valid AuthenticationHeaderValue that has the following details:

        //        var scheme = headerValue.Scheme;
        //        token = headerValue.Parameter;

        //        // scheme will be "Bearer"
        //        // parmameter will be the token itself.
        //    }


        //    return authHeader;
        //}

        //[HttpGet]
        //public async Task<IActionResult> CreateAuthCheck()
        //{

        //    var jwt = Request.Cookies["jwtCookie"];

        //    using (var httpClient = new HttpClient())
        //    {
        //        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jwt);
        //        using (var response = await httpClient.GetAsync("https://localhost:7162/Ogloszenie/AuthCheck"))
        //        {
        //            if (response.StatusCode == System.Net.HttpStatusCode.OK)
        //            {
        //                return RedirectToAction("Create");
        //            }

        //            if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
        //            {
        //                return RedirectToAction("Zaloguj", "Konto");
        //            }
        //        }
        //    }

        //    return View();
        //}

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        //[Authorize]
        //public IActionResult AuthCheck()
        //{
        //    return View();
        //}

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Ogloszenie o)
        {
            var jwt = Request.Cookies["jwtCookie"];

            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jwt);
                using (var response = await httpClient.GetAsync("https://localhost:7162/Konto/AuthLogin"))
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        string login = response.Content.ReadAsStringAsync().Result;
                        Konto? k = _db.Konta.Single(k => k.Login.Equals(login));

                        o.AutorId = k.UzytkownikId;
                        o.DataStworzenia = DateTime.Now;
                        o.DataWygasniecia = DateTime.Now.AddDays(30);
                        _db.Ogloszenia.Add(o);

                        _db.SaveChanges();
                        return RedirectToAction("Create");
                    }

                    if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                    {
                        return RedirectToAction("Zaloguj", "Konto");
                    }
                }
            }

            return View();
        }

        [HttpGet]
        public async Task<IActionResult> TwojeOgloszenia()
        {
            var jwt = Request.Cookies["jwtCookie"];

            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jwt);
                using (var response = await httpClient.GetAsync("https://localhost:7162/Konto/AuthLogin"))
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        string login = response.Content.ReadAsStringAsync().Result;
                        Konto? k = _db.Konta.Single(k => k.Login.Equals(login));

                        IEnumerable<Ogloszenie> list =_db.Ogloszenia.Where(o => o.AutorId.Equals(k.UzytkownikId));
                        list = list.OrderByDescending(o => o.DataStworzenia);

                        return View(list);
                    }

                    if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                    {
                        return RedirectToAction("Zaloguj", "Konto");
                    }
                }
            }
            return View();
        }

    }
}
