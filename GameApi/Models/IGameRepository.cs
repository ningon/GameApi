using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameApi.Models {
    public interface IGameRepository {
        string Create();
        Game Get(string key); 
        Game Remove(string key);
    }
}