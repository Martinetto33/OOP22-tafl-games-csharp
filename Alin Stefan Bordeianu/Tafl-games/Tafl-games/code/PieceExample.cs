using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tafl_games.api;

namespace Tafl_games.code
{
    public class PieceExample
    {
        public Position Pos { get; set; }
        public int NumberOfLives { get; set; }

        public PieceExample(Position pos, int numberOfLives) 
        {
            Pos = pos;
            NumberOfLives = numberOfLives;
        }

        private void Restore(IPieceMemento pm)
        {
            Pos = pm.GetPosition();
            NumberOfLives = pm.GetNumberOfLives();
        }

        public IPieceMemento Save() => new PieceMementoImpl(this);

        public class PieceMementoImpl : IPieceMemento
        {
            private readonly PieceExample _parent;
            private Position InnerPosition { get; }
            private int InnerNumberOfLives { get; }

            public PieceMementoImpl(PieceExample parent)
            {
                _parent = parent;
                InnerPosition = parent.Pos;
                InnerNumberOfLives = parent.NumberOfLives;
            }

            public int GetNumberOfLives() => InnerNumberOfLives;

            public Position GetPosition() => InnerPosition;

            public void Restore() => _parent.Restore(this);
        }
    }
}
