using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebPacMan.Services;

namespace WebPacMan.Models
{
    public class GameField
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string Direction { get; set; }
        public string GhostColor { get; set; }


        public string[,] Map { get; set; } =
         {
            {"#", "#", "#", "#", "#", "#", "#", "#", "#", "#", "#", "#", "#", "#", "#", "#", "#", "#","#", "#","#","#"},
            {"#", "*", ".", ".", ".", "#", ".", ".", ".", ".", ".", ".", ".", ".", ".", "*", "#", ".",".", ".",".","#"},
            {"#", ".", "#", "#", ".", "#", ".", "#", "#", "#", "#", "#", "#", "#", "#", ".", "#", ".","#", "#",".","#"},
            {"#", ".", "#", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".",".", "#",".","#"},
            {"#", ".", "#", ".", "#", "#", ".", "#", "#", "#", " ", " ", "#", "#", "#", ".", "#", "#",".", "#",".","#"},
            {"#", ".", "#", ".", ".", ".", ".", "#", " ", "B", "R", " ", "P", " ", "#", ".", ".", ".",".", "#",".","#"},
            {"#", ".", ".", ".", "#", "#", ".", "#", "#", "#", "#", "#", "#", "#", "#", ".", "#", "#",".", ".",".","#"},
            {"#", ".", "#", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".",".", "#",".","#"},
            {"#", ".", "#", ".", "#", "#", ".", "#", "#", "#", "#", "#", "#", "#", "#", ".", "#", "#",".", "#",".","#"},
            {"#", ".", "#", ".", ".", ".", ".", ".", ".", ".", "0", ".", ".", ".", ".", ".", ".", ".","G", "#",".","#"},
            {"#", ".", "#", "#", ".", "#", ".", "#", "#", "#", "#", "#", "#", "#", "#", ".", "#", ".","#", "#",".","#"},
            {"#", ".", ".", ".", "*", "#", ".", ".", ".", ".", ".", ".", ".", ".", ".", "*", "#", ".",".", ".","*","#"},
            {"#", "#", "#", "#", "#", "#", "#", "#", "#", "#", "#", "#", "#", "#", "#", "#", "#", "#","#", "#","#","#"},
        };

        public int Rows { get { return Map.GetUpperBound(0) + 1; } }
        public int Colums { get { return Map.Length / Rows; } }
        public int CountScore { get; set; }

        public PacMan pacMan { get; set; }
        public GGhost GGhost { get; set; }
        public RGhost RGhost { get; set; }
        public PGhost PGhost { get; set; }
        public BGhost BGhost { get; set; }
        //public Ghost[] ghostsList;

        public GameField()
        {
            pacMan = new PacMan();
            GGhost = new GGhost("G");
            BGhost = new BGhost();
            PGhost = new PGhost();
            RGhost = new RGhost();
            //ghostsList = new Ghost[]
            //{
            //     new Ghost("G"),
            //     new Ghost("R"),
            //     new Ghost("B"),
            //     new Ghost("P")
            //};
        }
    }
}

