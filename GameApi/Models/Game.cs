using System;
using System.Collections.Generic;
using System.Linq;

namespace GameApi.Models {

    public static class GameStatic {
        public const byte Size = 5;
        public const byte SizeSqr = Size * Size;
    }

    public enum StepStatus
    {
        OK,
        WIN,
        GAMENOTFOUND,
        STEPINCORRECT,
    }
    public class StepResponse
    {
        public StepStatus Status;
        public StepResponse(StepStatus ss) { Status = ss; }
    }

    public class Game {

        public Game() {
            Random rnd = new Random(Environment.TickCount);
            for (int i = 0; i != GameStatic.SizeSqr; ++i) {
                Nums.Add(i);
            }
            Nums = Nums.OrderBy(item => rnd.Next()).ToList();
        }

        public string Key { get; set; }
        public List<int> Nums = new List<int>();
        public int EmptyIndex => Nums.IndexOf(0);

        private void swap(int a, int b) {
            int temp = Nums[a];
            Nums[a] = Nums[b];
            Nums[b] = temp;
        }

        public bool CanMove(int pos) {
            int abs = Math.Abs(pos - EmptyIndex);
            return abs == GameStatic.Size || (abs == 1 && pos / GameStatic.Size == EmptyIndex / GameStatic.Size);
        }
        public void Move(int pos) {
            swap(EmptyIndex, pos);
        }
        public bool CheckWin() {
            for (int i = 1; i != GameStatic.SizeSqr; ++i){
                if (Nums[i - 1] != i){ return false; }
            }
            return true;
        }
    }
}