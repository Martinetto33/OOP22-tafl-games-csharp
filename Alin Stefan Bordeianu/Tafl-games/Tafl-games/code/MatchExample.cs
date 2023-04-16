using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tafl_games.api;

namespace Tafl_games.code
{
    public class MatchExample
    {
        private int TurnNumber { get; set; }
        private string ActivePlayer { get; set; }
        private BoardExample _board;

        public MatchExample(int turnNumber, string playerInTurn, BoardExample board)
        {
            TurnNumber = turnNumber;
            ActivePlayer = playerInTurn;
            _board = board;
        }

        public void Move(Position startPos, Position endPos) => _board.Move(startPos, endPos);

        public void Eat(Position attackedPosition) => _board.Eat(attackedPosition);

        public IMatchMemento Save() => new MatchMementoImpl(this.TurnNumber, this.ActivePlayer, _board.Save());

        public void Restore(IMatchMemento m)
        {
            this.TurnNumber = m.GetTurnNumber();
            this.ActivePlayer = m.GetActivePlayer();
            m.GetBoardMemento().Restore();
        }

        public class MatchMementoImpl : IMatchMemento
        {
            private int InnerTurnNumber { get; }
            private string InnerActivePlayer { get; }
            private IBoardMemento InnerBoardMemento { get; }

            public MatchMementoImpl(int innerTurnNumber, string innerActivePlayer, IBoardMemento innerBoardMemento)
            {
                InnerTurnNumber = innerTurnNumber;
                InnerActivePlayer = innerActivePlayer;
                InnerBoardMemento = innerBoardMemento;
            }

            public string GetActivePlayer() => InnerActivePlayer;

            public IBoardMemento GetBoardMemento() => InnerBoardMemento;

            public int GetTurnNumber() => InnerTurnNumber;
        }

    }
}
