using Microsoft.AspNetCore.Mvc;
using MMB_ASPNET.Models;

namespace MMB_ASPNET.Controllers
{
    public class HistoryController : Controller
    {
        private readonly IMatchRepository mRepo;
        public HistoryController(IMatchRepository mRepo)
        {
            this.mRepo = mRepo;
        }
        public IActionResult Index()
        {
            var matches = mRepo.GetAllMatches();

            return View(matches);
        }
    }
}
