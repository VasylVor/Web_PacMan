using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebPacMan.Models;

namespace WebPacMan.Services
{
    public class Ghost
    {
        public string Direction { get; set; } = "up";
        public string LastDot { get; set; }

        //public string GhostColor { get; set; }

        public int GetPosOldX { get; set; }
        public int GetPosOldY { get; set; }
        public int GetPosNewX { get; set; }
        public int GetPosNewY { get; set; }

        public void MoveRight()
        {
            if (GetPosNewY + 1 < 21)
            {
                GetPosOldX = GetPosNewX;
                GetPosOldY = GetPosNewY;
                GetPosNewY++;
            }
        }

        public void MoveLeft()
        {
            if (GetPosNewY - 1 > 0)
            {
                GetPosOldX = GetPosNewX;
                GetPosOldY = GetPosNewY;
                GetPosNewY--;
            }
        }

        public void MoveDown()
        {
            if (GetPosNewX + 1 < 12)
            {
                GetPosOldX = GetPosNewX;
                GetPosOldY = GetPosNewY;
                GetPosNewX++;
            }
        }

        public void MoveUP()
        {
            if (GetPosNewX - 1 > 0)
            {
                GetPosOldX = GetPosNewX;
                GetPosOldY = GetPosNewY;
                GetPosNewX--;
            }
        }

    }
}
