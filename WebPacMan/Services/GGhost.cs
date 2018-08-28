using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebPacMan.Services
{
    public class GGhost : Ghost
    {
        public string GhostColor { get; set; }
        public string Directin { get; private set; }
        public int StartPositionX { get; set; }
        public int StartPositionY { get; set; }

        public GGhost(string ghostColor)
        {
            GhostColor = ghostColor;
        }
        public void StartPosGhost(int rows, int colums, string[,] map)
        {
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < colums; j++)
                {
                    if (map[i, j] == "G")
                    {
                        GetPosNewY = j;
                        GetPosNewX = i;
                    }
                }
            }
        }



        public string ChageDiraction(int x, int y, string[,] board)
        {

            if (CheckMoveUP(x, y, board))
            {
                Directin = "up";
            }
            else if (!CheckMoveUP(x, y, board))
            {
                Directin = "right";
            }
            else if (CheckMoveDown(x, y, board))
            {
                Directin = "down";
            }
            else if (CheckMoveRight(x, y, board))
            {
                Directin = "right";
            }
            else if (CheckMoveLeft(x, y, board))
            {
                Directin = "left";
            }
            else if (CheckMoveUP(x, y, board) && CheckMoveDown(x, y, board) && CheckMoveRight(x, y, board) && CheckMoveLeft(x, y, board))
            {
                Directin = "right";
            }


            return Directin;
        }

        public bool CheckMoveLeft(int x, int y, string[,] board)
        {
            bool isEmpty = true;

            if (board[x, y - 1] == "#" || board[x, y - 1] == "P" || board[x, y - 1] == "B"
                || board[x, y - 1] == "R")
                isEmpty = false;

            if (isEmpty != false)
                LastDot = board[x, y - 1];

            return isEmpty;
        }


        public bool CheckMoveRight(int x, int y, string[,] board)
        {
            bool isEmpty = true;

            if (board[x, y + 1] == "#" || board[x, y + 1] == "P" || board[x, y + 1] == "B"
                || board[x, y + 1] == "R")
                isEmpty = false;

            if (isEmpty != false)
                LastDot = board[x, y + 1];

            return isEmpty;
        }


        public bool CheckMoveUP(int x, int y, string[,] board)
        {
            bool isEmpty = true;

            if (board[x - 1, y] == "#" || board[x - 1, y] == "P" || board[x - 1, y] == "B"
                || board[x - 1, y] == "R")
                isEmpty = false;

            if (isEmpty != false)
                LastDot = board[x - 1, y];

            return isEmpty;
        }


        public bool CheckMoveDown(int x, int y, string[,] board)
        {
            bool isEmpty = true;

            if (board[x + 1, y] == "#" || board[x + 1, y] == "P" || board[x + 1, y] == "B"
                || board[x + 1, y] == "R")
                isEmpty = false;

            if (isEmpty != false)
                LastDot = board[x + 1, y];

            return isEmpty;
        }
    }
}
