using GameApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace GameApi.Controllers
{
    [Route("api/[controller]")]
    public class GameController : Controller {

        public IGameRepository Games { get; set; }

        public GameController(IGameRepository games) {
            Games = games;
        }

        [HttpGet]
        public string StartNewGame() {
            return Games.Create();
        }

        [HttpGet("{id}")]
        public IActionResult GetData(string id) {
            var Game = Games.Get(id);
            if (Game == null) {
                return NotFound();
            }
            return new ObjectResult(Game);
        }

        [HttpGet("{id}/{pos}")]
        public IActionResult ProcessStep(string id, int pos) {
            var Game = Games.Get(id);
            if(Game == null) {
                return new ObjectResult(new StepResponse(StepStatus.GAMENOTFOUND));
            }
            if (Game.CanMove(pos)) {
                Game.Move(pos);
                if(Game.CheckWin()) {
                    return new ObjectResult(new StepResponse(StepStatus.WIN));
                }
                return new ObjectResult(new StepResponse(StepStatus.OK));
            }
            return new ObjectResult(new StepResponse(StepStatus.STEPINCORRECT));
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(string id)
        {
            var Game = Games.Get(id);
            if (Game == null)
            {
                return NotFound();
            }
            Games.Remove(id);
            return new NoContentResult();
        }
    }
}