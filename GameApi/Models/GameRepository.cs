using System;
using System.Collections.Concurrent;

namespace GameApi.Models {
    public class GameRepository : IGameRepository {

        private static ConcurrentDictionary<string, Game> _games =
              new ConcurrentDictionary<string, Game>();

        public GameRepository() { } 

        public string Create() {
            Game game = new Game();
            game.Key = Guid.NewGuid().ToString();
            _games[game.Key] = game;
            return game.Key;
        }

        public Game Get(string key) {
            Game game;
            _games.TryGetValue(key, out game);
            return game;
        }

        public Game Remove(string key) {
            Game game;
            _games.TryRemove(key, out game);
            return game;
        }
    }
}
