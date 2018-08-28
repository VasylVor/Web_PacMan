using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebPacMan.Services
{
    public class RGhost : Ghost
    {
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

        public string ChageDiraction(string directin)
        {
            directin = "up";
            return directin;
        }

        public bool CheckMoveLeft(int x, int y, string[,] board)
        {
            bool isEmpty = true;

            if (board[x, y - 1] == "#" || board[x, y - 1] == "G" || board[x, y - 1] == "B"
                || board[x, y - 1] == "P")
                isEmpty = false;

            if (isEmpty != false)
                LastDot = board[x, y - 1];

            return isEmpty;
        }


        public bool CheckMoveRight(int x, int y, string[,] board)
        {
            bool isEmpty = true;

            if (board[x, y + 1] == "#" || board[x, y + 1] == "G" || board[x, y + 1] == "B"
                || board[x, y + 1] == "P")
                isEmpty = false;

            if (isEmpty != false)
                LastDot = board[x, y + 1];

            return isEmpty;
        }


        public bool CheckMoveUP(int x, int y, string[,] board)
        {
            bool isEmpty = true;

            if (board[x - 1, y] == "#" || board[x - 1, y] == "G" || board[x - 1, y] == "B"
                || board[x - 1, y] == "P")
                isEmpty = false;

            if (isEmpty != false)
                LastDot = board[x - 1, y];

            return isEmpty;
        }


        public bool CheckMoveDown(int x, int y, string[,] board)
        {
            bool isEmpty = true;

            if (board[x + 1, y] == "#" || board[x + 1, y] == "G" || board[x + 1, y] == "B"
                || board[x + 1, y] == "P")
                isEmpty = false;

            if (isEmpty != false)
                LastDot = board[x + 1, y];

            return isEmpty;
        }
    }
}
