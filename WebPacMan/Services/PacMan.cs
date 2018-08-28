using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebPacMan.Models;

namespace WebPacMan.Services
{
    public class PacMan
    {
        public string NextDirection { get; set; }

        public int GetPositionOldX { get; set; }
        public int GetPositionOldY { get; set; }
        public int GetPositionNewX { get; set; }
        public int GetPositionNewY { get; set; }
        public int StartPositionX { get; set; }
        public int StartPositionY { get; set; }

        public int Score { get; set; }
        public int Life { get; set; } = 3;



        public void MoveUp()
        {
            GetPositionNewY = GetPositionOldY - 1;
            GetPositionNewX = GetPositionOldX;
        }

        public void MoveDown()
        {
            GetPositionNewY = GetPositionOldY + 1;
            GetPositionNewX = GetPositionOldX;
        }

        public void MoveRight()
        {
            GetPositionNewX = GetPositionOldX + 1;
            GetPositionNewY = GetPositionOldY;
        }

        public void MoveLeft()
        {
            GetPositionNewX = GetPositionOldX - 1;
            GetPositionNewY = GetPositionOldY;
        }

        public void EatDot()
        {
            Score++;
        }

        public void EatStar()
        {
            Score += 100;
        }

        public void LoseLife()
        {
            Life--;
        }

        public void Position(int rows, int colums, string[,] map)
        {
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < colums; j++)
                {
                    if (map[i, j] == "0")
                    {
                        GetPositionOldX = j;
                        GetPositionOldY = i;
                    }
                }
            }
        }

        public BoardElements Check(string direction, string[,] map)
        {
            switch (direction)
            {
                case "up":
                    MoveUp();
                    return CheckPosition(map, GetPositionOldY - 1, GetPositionOldX);
                case "down":
                    MoveDown();
                    return CheckPosition(map, GetPositionOldY + 1, GetPositionOldX);
                case "right":
                    MoveRight();
                    return CheckPosition(map, GetPositionOldY, GetPositionOldX + 1);
                case "left":
                    MoveLeft();
                    return CheckPosition(map, GetPositionOldY, GetPositionOldX - 1);
                case "ghost":
                    return BoardElements.Monsters;
            }
            return BoardElements.Empty;
        }

        public BoardElements CheckPosition(string[,] map, int y, int x)
        {
            switch (map[y, x])
            {
                case ".":
                    return BoardElements.Dot;
                case "*":
                    return BoardElements.Star;
                case " ":
                    return BoardElements.Empty;
                case "#":
                    return BoardElements.Wall;
                case "G":
                    return BoardElements.Monsters;
                case "B":
                    return BoardElements.Monsters;
                case "P":
                    return BoardElements.Monsters;
                case "R":
                    return BoardElements.Monsters;
            }
            return BoardElements.Empty;
        }
    }
}
