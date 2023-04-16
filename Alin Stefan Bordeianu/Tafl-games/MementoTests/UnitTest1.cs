using Tafl_games.api;
using Tafl_games.code;

namespace MementoTests
{
    /// <summary>
    /// Tests the Memento feature in a simplified context.
    /// </summary>
    [TestClass]
    public class UnitTest1
    {
        private MatchExample _match;
        private BoardExample _board;
        private ICaretaker _caretaker;

        /// <summary>
        /// Initializes the fields. This method will be called
        /// by all subsequent tests, in order to have fresh configurations
        /// of the board and the match every time.
        /// </summary>
        [TestInitialize()]
        public void Init()
        {
            _board = new BoardExample();
            _match = new MatchExample("Attacker", _board);
            _caretaker = new CaretakerImpl(_match);
            _caretaker.UpdateHistory();
        }

        /// <summary>
        /// A preliminary check that the normal movement and
        /// piece killing function as expected.
        /// </summary>
        [TestMethod]
        public void TestMovement()
        {
            this.Init();

            // selecting the first attacker to the left of the bottom lines
            Position attackerStartPos = new(9, 0);
            Position attackerEndPos = new(4, 0);
            this.PlayRoundWithoutPassingTurn(attackerStartPos, attackerEndPos, null);

            Assert.IsTrue(_board.Cells[attackerStartPos].CellStatus);
            Assert.IsFalse(_board.Cells[attackerEndPos].CellStatus);

            //passing turn
            this.PassTurn("Defender");

            Position defenderStartPos = new(1, 1);
            Position defenderEndPos = new(4, 1);
            this.PlayRoundWithoutPassingTurn(defenderStartPos, defenderEndPos, null);

            Assert.IsTrue(_board.Cells[attackerStartPos].CellStatus);
            Assert.IsFalse(_board.Cells[attackerEndPos].CellStatus);

            Assert.IsTrue(_board.Cells[defenderStartPos].CellStatus);
            Assert.IsFalse(_board.Cells[defenderEndPos].CellStatus);

            this.PassTurn("Attacker");
            Position secondAttackerStartPos = new(9, 2);
            Position secondAttackerEndPos = new(4, 2);
            this.PlayRoundWithoutPassingTurn(secondAttackerStartPos, secondAttackerEndPos, defenderEndPos);

            //checking if the piece was killed
            this.WasPieceKilled(defenderEndPos);

        }

        /// <summary>
        /// Tests if the undo feature works correctly in the case of a simple movement.
        /// </summary>
        [TestMethod]
        public void TestMovementUndo()
        {
            this.Init();

            Position attackerStartPos = new(9, 0);
            Position attackerEndPos = new(4, 0);
            PieceExample movingPiece = _board.Pieces[attackerStartPos];
            this.PlayRoundWithoutPassingTurn(attackerStartPos, attackerEndPos, null);

            Assert.IsFalse(_board.Cells[attackerEndPos].CellStatus); //the ending position is occupied
            Assert.IsTrue(_board.Cells[attackerStartPos].CellStatus); //the starting position is free

            Assert.IsTrue(_caretaker.IsLocked());
            _caretaker.UnlockHistory();
            _caretaker.Undo();

            Assert.IsFalse(_board.Cells[attackerStartPos].CellStatus); //the piece has returned back to the starting position
            Assert.IsTrue(_board.Cells[attackerEndPos].CellStatus); //the end position is now free again
            Assert.AreEqual(attackerStartPos, movingPiece.Pos);
            Assert.AreEqual("Attacker", _match.ActivePlayer);
            Assert.AreEqual(0, _match.TurnNumber);

            this.PlayRoundWithoutPassingTurn(attackerStartPos, attackerEndPos, null); //redoing the move
            this.PassTurn("Defender");

            /* Now there will be an attempt to undo before any move */
            _caretaker.UnlockHistory();
            _caretaker.Undo();

            /* Nothing changed! */
            Assert.AreEqual("Defender", _match.ActivePlayer);
            Assert.AreEqual(1, _match.TurnNumber);
        }

        /// <summary>
        /// Tests if the undo can manage correctly dead pieces.
        /// </summary>
        [TestMethod]
        public void TestEatingUndo()
        {
            this.Init();


            // selecting the first attacker to the left of the bottom lines
            Position attackerStartPos = new(9, 0);
            Position attackerEndPos = new(4, 0);
            this.PlayRoundWithoutPassingTurn(attackerStartPos, attackerEndPos, null);
            this.PassTurn("Defender");

            Position defenderStartPos = new(1, 1);
            Position defenderEndPos = new(4, 1);
            this.PlayRoundWithoutPassingTurn(defenderStartPos, defenderEndPos, null);
            this.PassTurn("Attacker");

            Position secondAttackerStartPos = new(9, 2);
            Position secondAttackerEndPos = new(4, 2);
            this.PlayRoundWithoutPassingTurn(secondAttackerStartPos, secondAttackerEndPos, defenderEndPos);

            //checking if the piece was killed
            this.WasPieceKilled(defenderEndPos);

            _caretaker.UnlockHistory();
            _caretaker.Undo();

            Assert.IsTrue(_board.Cells[secondAttackerEndPos].CellStatus);
            Assert.IsFalse(_board.Cells[defenderEndPos].CellStatus); //the defendere is still there
            Assert.AreEqual(1, _board.Pieces[defenderEndPos].NumberOfLives);
        }

        private void WasPieceKilled(Position attackedPosition)
        {
            Assert.IsTrue(_board.Cells[attackedPosition].CellStatus = true);
            Assert.AreEqual(_board.Pieces.Values
                                       .Where(piece => piece.Pos.Equals(attackedPosition))
                                       .Select(piece => piece.NumberOfLives)
                                       .First(), 0);
        }

        private void PlayRoundWithoutPassingTurn(Position startPos, Position endPos, Position? attackedPos)
        {
            _caretaker.UpdateHistory();
            _match.Move(startPos, endPos);
            if (attackedPos != null) 
            {
                _match.Eat(attackedPos);
            }
        }

        // The update of the caretaker should follow naturally any operation of turn passing.
        private void PassTurn(string nextPlayerInTurn)
        {
            _match.PassTurn(nextPlayerInTurn);
            _caretaker.UpdateHistory();
        }
    }
}