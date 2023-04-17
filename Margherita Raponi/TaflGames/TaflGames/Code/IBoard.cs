
using System.Numerics;
using System;

namespace TaflGames.Code
{
    internal interface IBoard
    {
        bool IsStartingPointValid(Position start, Player player);
    }
}
