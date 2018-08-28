//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
//using WebPacMan.Models;

//namespace WebPacMan.Services
//{
//    public class Position : IPosition
//    {
//        //IManager _manager;
//        //GameField _game;
//        //public Position(IManager manager)
//        //{
//        //    _manager = manager;
//        //}

//        //string GameId { get; set; }

//        //public GameField GetGame(string id)
//        //{
//        //    GameId = id.Substring(4);

//        //    if (!_manager.GetValue(GameId, out _game))
//        //    {
//        //        _game = new GameField();
//        //    }
//        //    return _game;
//        //}

//        public Task ChangePosition(string[,] map, int oldX, int oldY, int newX, int newY, string oldActor, string newActor)
//        {
//            map[oldY, oldX] = oldActor;
//            map[newY, newX] = newActor;
//            re;
//        }

//    }
//}
