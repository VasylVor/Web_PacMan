using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using WebPacMan.Models;
using WebPacMan.Pages;
using WebPacMan.Services;
namespace WebPacMan
{
    class ChatHub : Hub
    {
        Random random = new Random();
        GameField _game;
        IManager _manager;
        IDbConmmand _conmmand;

        //IPosition _position;
        public string GameId { get; set; }

        string[] posibleDirection =
        {
            "up",
            "down",
            "right",
            "left"
        };

        public ChatHub(IManager manager, IDbConmmand conmmand/*, IPosition position*/)
        {
            _manager = manager;
            _conmmand = conmmand;
            //_position = position;
        }

        public async Task Send(string message)
        {
            await this.Clients.All.SendAsync("Send", message, _conmmand.GetNickName(Context.User.Identity.Name));
        }

        public async Task Move(string nextDirection, string id)
        {
            //_game = _position.GetGame(id);

            GameId = id.Substring(4);

            if (!_manager.GetValue(GameId, out _game))
            {
                _game = new GameField();
            }

            if (_game.pacMan.Score == _game.CountScore)
            {
                await _conmmand.AddScoreAsync(_game.pacMan.Score, Context.User.Identity.Name);
                await this.Clients.All.SendAsync("Win");
            }

            switch (nextDirection)
            {
                case "up":
                    _game.Direction = "up";
                    break;
                case "down":
                    _game.Direction = "down";
                    break;
                case "right":
                    _game.Direction = "right";
                    break;
                case "left":
                    _game.Direction = "left";
                    break;
            }
            await MovePacMan();
        }

        public async Task MovePacMan()
        {
            _game.pacMan.Position(_game.Rows, _game.Colums, _game.Map);
            await Task.Delay(600);
            switch (_game.pacMan.Check(_game.Direction, _game.Map))
            {
                case BoardElements.Dot:
                    _game.pacMan.EatDot();
                    await this.Clients.All.SendAsync("Move", _game.pacMan, _game.Direction, _conmmand.GetNickName(Context.User.Identity.Name));
                    //_game.Map = _position.ChangePosition(_game.Map, _game.pacMan.GetPositionOldX, _game.pacMan.GetPositionOldY,
                    //                                _game.pacMan.GetPositionNewX, _game.pacMan.GetPositionNewY, " ", "0");
                    _game.Map[_game.pacMan.GetPositionOldY, _game.pacMan.GetPositionOldX] = " ";
                    _game.Map[_game.pacMan.GetPositionNewY, _game.pacMan.GetPositionNewX] = "0";
                    break;
                case BoardElements.Star:
                    _game.pacMan.EatStar();
                    await this.Clients.All.SendAsync("Move", _game.pacMan, _game.Direction, _conmmand.GetNickName(Context.User.Identity.Name));
                    //_game.Map = _position.ChangePosition(_game.Map, _game.pacMan.GetPositionOldX, _game.pacMan.GetPositionOldY,
                    //                                _game.pacMan.GetPositionNewX, _game.pacMan.GetPositionNewY, " ", "0");
                    _game.Map[_game.pacMan.GetPositionOldY, _game.pacMan.GetPositionOldX] = " ";
                    _game.Map[_game.pacMan.GetPositionNewY, _game.pacMan.GetPositionNewX] = "0";
                    break;
                case BoardElements.Empty:
                    await this.Clients.All.SendAsync("Move", _game.pacMan, _game.Direction, _conmmand.GetNickName(Context.User.Identity.Name));
                    //_game.Map = _position.ChangePosition(_game.Map, _game.pacMan.GetPositionOldX, _game.pacMan.GetPositionOldY,
                    //                                _game.pacMan.GetPositionNewX, _game.pacMan.GetPositionNewY, " ", "0");
                    _game.Map[_game.pacMan.GetPositionOldY, _game.pacMan.GetPositionOldX] = " ";
                    _game.Map[_game.pacMan.GetPositionNewY, _game.pacMan.GetPositionNewX] = "0";
                    break;
                case BoardElements.Wall:
                    break;
                case BoardElements.Monsters:
                    _game.pacMan.LoseLife();
                    await _conmmand.AddScoreAsync(_game.pacMan.Score, Context.User.Identity.Name);
                    if (_game.pacMan.Life >= 0)
                    {
                        await this.Clients.All.SendAsync("Lost", _game.GGhost, _game.pacMan);
                        _game.Map[_game.pacMan.GetPositionOldY, _game.pacMan.GetPositionOldX] = " ";
                        _game.Map[_game.pacMan.StartPositionY, _game.pacMan.StartPositionX] = "0";

                        _game.Map[_game.GGhost.GetPosOldX, _game.GGhost.GetPosOldY] = " ";
                        _game.Map[_game.GGhost.StartPositionX, _game.GGhost.StartPositionY] = "G";
                    }
                    else
                    {
                        _game = new GameField();
                        _manager.Add(GameId, _game);
                        await this.Clients.All.SendAsync("GameOver");
                    }
                    break;
            }
        }

        public async Task Ghost(string id)
        {
            //_game = _position.GetGame(id);

            GameId = id.Substring(4);

            if (!_manager.GetValue(GameId, out _game))
            {
                _game = new GameField();
            }

            await MoveGhost(_game.GGhost);
            await Task.Delay(600);

            //await MoveGhost();
            //await MoveGhost();
            //await MoveGhost();

            //foreach (var ghost in _game.ghostsList)
            //{
            //    ghost.StartPosGhost(_game.Rows, _game.Colums, _game.Map);

            //    if (random.Next(0, 2) != 0)
            //    {
            //        ghost.Direction = posibleDirection[random.Next(0, posibleDirection.Length)];
            //    }

            //    //ghost.Direction = "left";

            //    await MoveGhost(ghost);

            //}
        }

        public async Task MoveGhost(GGhost ghost)
        {
            ghost.StartPosGhost(_game.Rows, _game.Colums, _game.Map);
            _game.GhostColor = ghost.GhostColor;
            switch (/*ghost.ChageDiraction(ghost.GetPosNewX, ghost.GetPosNewY, _game.Map)*/"left")
            {
                case "left":
                    if (ghost.CheckMoveLeft(ghost.GetPosNewX, ghost.GetPosNewY, _game.Map))
                    {
                        ghost.MoveLeft();
                        if (ghost.GetPosNewY != _game.pacMan.GetPositionNewX || ghost.GetPosNewX != _game.pacMan.GetPositionNewY)
                        {
                            await this.Clients.All.SendAsync("MoveGhost", ghost, _game.GhostColor);
                            //Task mapTask = _position.ChangePosition(_game.Map, ghost.GetPosOldX, ghost.GetPosOldY, ghost.GetPosNewX, ghost.GetPosNewY,
                            //    ghost.LastDot, ghost.GhostColor);
                            _game.Map[ghost.GetPosNewX, ghost.GetPosNewY] = ghost.GhostColor;
                            _game.Map[ghost.GetPosOldX, ghost.GetPosOldY] = ghost.LastDot;
                        }
                        else
                        {
                            _game.Direction = "ghost";
                            Task pacManTask = MovePacMan();
                        }
                    }
                    break;
                case "right":
                    if (ghost.CheckMoveRight(ghost.GetPosNewX, ghost.GetPosNewY, _game.Map))
                    {
                        ghost.MoveRight();
                        if (ghost.GetPosNewY != _game.pacMan.GetPositionNewX || ghost.GetPosNewX != _game.pacMan.GetPositionNewY)
                        {
                            await this.Clients.All.SendAsync("MoveGhost", ghost, _game.GhostColor);
                            //_game.Map = _position.ChangePosition(_game.Map, ghost.GetPosOldX, ghost.GetPosOldY, ghost.GetPosNewX, ghost.GetPosNewY,
                            //    ghost.LastDot, ghost.GhostColor);
                            _game.Map[ghost.GetPosNewX, ghost.GetPosNewY] = ghost.GhostColor;
                            _game.Map[ghost.GetPosOldX, ghost.GetPosOldY] = ghost.LastDot;
                        }
                        else
                        {
                            _game.Direction = "ghost";
                            Task pacManTask = MovePacMan();
                        }
                    }
                    break;
                case "up":
                    if (ghost.CheckMoveUP(ghost.GetPosNewX, ghost.GetPosNewY, _game.Map))
                    {
                        ghost.MoveUP();
                        if (ghost.GetPosNewY != _game.pacMan.GetPositionNewX || ghost.GetPosNewX != _game.pacMan.GetPositionNewY)
                        {
                            await this.Clients.All.SendAsync("MoveGhost", ghost, _game.GhostColor);
                            //_game.Map = _position.ChangePosition(_game.Map, ghost.GetPosOldX, ghost.GetPosOldY, ghost.GetPosNewX, ghost.GetPosNewY,
                            //    ghost.LastDot, ghost.GhostColor);
                            _game.Map[ghost.GetPosNewX, ghost.GetPosNewY] = ghost.GhostColor;
                            _game.Map[ghost.GetPosOldX, ghost.GetPosOldY] = ghost.LastDot;
                        }
                        else
                        {
                            _game.Direction = "ghost";
                            Task pacManTask = MovePacMan();
                        }
                    }
                    break;
                case "down":
                    if (ghost.CheckMoveDown(ghost.GetPosNewX, ghost.GetPosNewY, _game.Map))
                    {
                        ghost.MoveDown();
                        if (ghost.GetPosNewY != _game.pacMan.GetPositionNewX || ghost.GetPosNewX != _game.pacMan.GetPositionNewY)
                        {
                            await this.Clients.All.SendAsync("MoveGhost", ghost, _game.GhostColor);
                            //_game.Map = _position.ChangePosition(_game.Map, ghost.GetPosOldX, ghost.GetPosOldY, ghost.GetPosNewX, ghost.GetPosNewY,
                            //    ghost.LastDot, ghost.GhostColor);
                            _game.Map[ghost.GetPosNewX, ghost.GetPosNewY] = ghost.GhostColor;
                            _game.Map[ghost.GetPosOldX, ghost.GetPosOldY] = ghost.LastDot;
                        }
                        else
                        {
                            _game.Direction = "ghost";
                            Task pacManTask = MovePacMan();
                        }
                    }
                    break;
            }
        }
    }
}
