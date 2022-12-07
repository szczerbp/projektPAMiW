using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using projekt.Data.Models;
using System.Net.Http.Headers;

namespace projekt.Controllers
{
    public class UzytkownikController : Controller
    {
        private readonly DatabaseDbContext _db;

        public UzytkownikController(DatabaseDbContext db)
        {
            _db = db;
        }

        public async Task<IActionResult> GetList()
        {
            var jwt = Request.Cookies["jwtCookie"];

            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jwt);
                using (var response = await httpClient.GetAsync("https://localhost:7162/Konto/AuthLoginWithRole"))
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        string str = response.Content.ReadAsStringAsync().Result;
                        List<string> list = str.Split('"').ToList();
                        List<string> cleanList = new List<string>();
                        foreach (string s in list)
                        {
                            if (s.Equals(",") || s.Equals("[") || s.Equals("]")) { }
                            else { cleanList.Add(s); }
                        }
                        IEnumerable<Uzytkownik> objUzytkownikList = new List<Uzytkownik>();
                        if (cleanList[1].Equals("admin"))
                        {
                            objUzytkownikList = _db.Uzytkownicy.ToList();
                            return View(objUzytkownikList);
                        }
                        else { return RedirectToAction("Zaloguj", "Konto"); }
                    }

                    if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                    {
                        return RedirectToAction("Zaloguj", "Konto");
                    }
                }
            }
            return RedirectToAction("Zaloguj", "Konto");
        }

        [HttpGet]
        public async Task<IActionResult> Edytuj(long Id)
        {
            var jwt = Request.Cookies["jwtCookie"];

            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jwt);
                using (var response = await httpClient.GetAsync("https://localhost:7162/Konto/AuthLoginWithRole"))
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        string str = response.Content.ReadAsStringAsync().Result;
                        List<string> list = str.Split('"').ToList();
                        List<string> cleanList = new List<string>();
                        foreach (string s in list)
                        {
                            if (s.Equals(",") || s.Equals("[") || s.Equals("]")) { }
                            else { cleanList.Add(s); }
                        }
                        if (cleanList[1].Equals("admin"))
                        {
                            Uzytkownik? uz = _db.Uzytkownicy.SingleOrDefault(u => u.Id.Equals(Id));
                            return View(uz);
                        }
                        else { return RedirectToAction("Zaloguj", "Konto"); }
                    }

                    if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                    {
                        return RedirectToAction("Zaloguj", "Konto");
                    }
                }
            }
            return RedirectToAction("Zaloguj", "Konto");
        }

        [HttpPost]
        public async Task<IActionResult> Edytuj(Uzytkownik u)
        {
            var jwt = Request.Cookies["jwtCookie"];

            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jwt);
                using (var response = await httpClient.GetAsync("https://localhost:7162/Konto/AuthLoginWithRole"))
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        string str = response.Content.ReadAsStringAsync().Result;
                        List<string> list = str.Split('"').ToList();
                        List<string> cleanList = new List<string>();
                        foreach (string s in list)
                        {
                            if (s.Equals(",") || s.Equals("[") || s.Equals("]")) { }
                            else { cleanList.Add(s); }
                        }
                        if (cleanList[1].Equals("admin"))
                        {
                            var uz = _db.Uzytkownicy.SingleOrDefault(uzy => uzy.Id.Equals(u.Id));
                            uz.Imie = u.Imie;
                            uz.Nazwisko = u.Nazwisko;
                            uz.Email = u.Email;
                            _db.SaveChanges();
                            return RedirectToAction("GetList", "Uzytkownik");
                        }
                        else { return RedirectToAction("Zaloguj", "Konto"); }
                    }

                    if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                    {
                        return RedirectToAction("Zaloguj", "Konto");
                    }
                }
            }
            return RedirectToAction("Zaloguj", "Konto");
        }

        public async Task<IActionResult> Usun(long Id)
        {
            var jwt = Request.Cookies["jwtCookie"];

            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jwt);
                using (var response = await httpClient.GetAsync("https://localhost:7162/Konto/AuthLoginWithRole"))
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        string str = response.Content.ReadAsStringAsync().Result;
                        List<string> list = str.Split('"').ToList();
                        List<string> cleanList = new List<string>();
                        foreach (string s in list)
                        {
                            if (s.Equals(",") || s.Equals("[") || s.Equals("]")) { }
                            else { cleanList.Add(s); }
                        }
                        if (cleanList[1].Equals("admin"))
                        {
                            _db.Uzytkownicy.Where(u => u.Id == Id).ExecuteDelete();
                            return RedirectToAction("GetList", "Uzytkownik");
                        }
                        else { return RedirectToAction("Zaloguj", "Konto"); }
                    }

                    if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                    {
                        return RedirectToAction("Zaloguj", "Konto");
                    }
                }
            }
            return RedirectToAction("Zaloguj", "Konto");
        }
    }
}
