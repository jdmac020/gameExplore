using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Resources.Scripts.LevelManagement
{
    public class LevelStatus
    {
        public int WorldNumber { get; set; }
        public int LevelNumber { get; set; }
        public int Attempts { get; set; }
        public bool IsComplete { get; set; }
        public int Score { get; set; }
        public string SceneName { get { return GetLevelName(); } }

        private string GetLevelName()
        {
            return $"{WorldNumber}-{LevelNumber}";
        }
    }
}
