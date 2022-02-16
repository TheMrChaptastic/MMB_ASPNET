using Microsoft.AspNetCore.Mvc;
using MMB_ASPNET.Models;
using MMB_ASPNET.Services;

namespace MMB_ASPNET.Controllers
{
    public class MatchController : Controller
    {
        private readonly IPlayerRepository pRepo;
        private readonly IMatchRepository mRepo;
        public MatchController(IPlayerRepository pRepo, IMatchRepository mRepo)
        {
            this.pRepo = pRepo;
            this.mRepo = mRepo;
        }
        public IActionResult Index()
        {
            var players = pRepo.GetAllPlayers();

            return View(players);
        }
        public IActionResult NewMatch(int wId, int lId)
        {
            Player winner = pRepo.GetPlayer(wId);
            Player loser = pRepo.GetPlayer(lId);

            mRepo.NewMatch(winner, loser);
            pRepo.PlayerMatch(winner, loser);

            return RedirectToAction("Index", "Player");
        }
    }
}
