using Microsoft.AspNetCore.Mvc;
using MMB_ASPNET.Models;

namespace MMB_ASPNET.Controllers
{
    public class PlayerController : Controller
    {
        private readonly IPlayerRepository repo;
        public PlayerController(IPlayerRepository repo)
        {
            this.repo = repo;
        }

        public IActionResult Index()
        {
            var players = repo.GetAllPlayers();

            return View(players);
        }
        public IActionResult ViewPlayer(int id)
        {
            var player = repo.GetPlayer(id);
            player.Matches = repo.GetPlayersMatches(id);

            return View(player);
        }
        public IActionResult UpdatePlayer(int id)
        {
            Player player = repo.GetPlayer(id);

            if (player == null)
            {
                return View("PlayerNotFound");
            }

            return View(player);
        }
        public IActionResult UpdatePlayerToDatabase(Player player)
        {
            repo.UpdatePlayerName(player);

            return RedirectToAction("ViewPlayer", new { id = player.Id });
        }
        public IActionResult InsertPlayer()
        {
            var player = new Player();

            return View(player);
        }
        public IActionResult InsertPlayerToDatabase(Player playerInsert)
        {
            repo.InsertPlayer(playerInsert);

            return RedirectToAction("Index");
        }
        public IActionResult DeletePlayer(Player player)
        {
            repo.DeletePlayer(player);

            return RedirectToAction("Index");
        }
    }
}
