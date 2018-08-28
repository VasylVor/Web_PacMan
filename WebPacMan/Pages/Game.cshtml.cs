using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebPacMan.Models;
using WebPacMan.Services;

namespace WebPacMan.Pages
{
    public class GameModel : PageModel
    {
        IManager _manager;
        public GameField _game;

        public int GetRows { get { return _game.Rows; } }
        public int GetColums { get { return _game.Colums; } }
        public string[,] GetMap { get { return _game.Map; } }

        public GameModel(IManager manager)
        {
            _manager = manager;
        }

        public void OnGet(string id)
        {
            if (!_manager.GetValue(id, out _game))
            {
                _game = new GameField();
            }
            _game.pacMan.Position(GetRows, GetColums, GetMap);
            _game.pacMan.StartPositionX = _game.pacMan.GetPositionOldX;
            _game.pacMan.StartPositionY = _game.pacMan.GetPositionOldY;

            _game.GGhost.StartPosGhost(GetRows, GetColums, GetMap);
            _game.GGhost.StartPositionX = _game.GGhost.GetPosNewX;
            _game.GGhost.StartPositionY = _game.GGhost.GetPosNewY;

            _game.CountScore = GetCountScore(GetMap, GetRows, GetColums);
        }

        public int GetCountScore(string[,] map, int rows, int colums)
        {
            int score = 0;
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < colums; j++)
                {
                    if (map[i, j] == ".")
                        score++;
                    else if (map[i, j] == "*")
                        score += 100;
                }
            }
            return score;
        }
    }
}