using Microsoft.AspNetCore.Mvc;
using projekt.Data.Models;

namespace projekt.Controllers
{
    public class UzytkownikController : Controller
    {
        private readonly DatabaseDbContext _db;

        public UzytkownikController(DatabaseDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            IEnumerable<Uzytkownik> objUzytkownikList = _db.Uzytkownicy.ToList();
            return View(objUzytkownikList);
        }
    }
}
