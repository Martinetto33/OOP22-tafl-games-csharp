using Common;
using Builders;
using NUnit.Framework.Internal;
using SettingsLoaders;

namespace TaflGames.TestMatchSetup
{
    public class Tests
    {
        private const int BoardSize = 11;

        private ICellsCollectionBuilder _cellsCollBuilder;
        private IPiecesCollectionBuilder _piecesCollBuilder;

        [SetUp]
        public void Setup()
        {
            _cellsCollBuilder = new CellsCollectionBuilder();
            _piecesCollBuilder = new PiecesCollectionBuilder();
        }

        /// <summary>
        /// Test the match setup according to the classic mode settings.
        /// </summary>
        [Test]
        public void TestClassicModeMatchSetup()
        {
            ISettingsLoader loader = new SettingsLoader();
            try
            {
                loader.LoadClassicModeConfig(_cellsCollBuilder, _piecesCollBuilder);
            }
            catch (IOException ex)
            {
                Assert.Fail("Error: could not read configuration file. " + ex.ToString());
            }

            var cells = _cellsCollBuilder.Build();
            var pieces = _piecesCollBuilder.Build();

            // Check king and throne correct placement
            Assert.That(
                pieces[Player.Defender][new Position(5, 5)].GetType(),
                Is.EqualTo(typeof(Pieces.King))
            );
            Assert.That(
                cells[new Position(5, 5)].GetType(),
                Is.EqualTo(typeof(Cells.Throne))
            );

            // Check exits correct placement
            Assert.That(
                GetPositions(cells, typeof(Cells.Exit)),
                Is.EqualTo(
                    new HashSet<IPosition>()
                    {
                        new Position(0, 0),
                        new Position(0, 10),
                        new Position(10, 0),
                        new Position(10, 10)
                    }
                )
            );

            // Check classic cells correct placement
            Assert.That(
                GetPositions(cells, typeof(Cells.ClassicCell)),
                Is.EqualTo(
                    GenerateAllPositions().AsQueryable()
                        // filter out throne position
                        .Where(pos => !pos.Equals(new Position(5, 5)))
                        // filter out exits positions
                        .Where(pos => !GetPositions(cells, typeof(Cells.Exit)).Contains(pos))
                        .ToHashSet()
                )
            );

            // Check attacker basic pieces correct placement
            Assert.That(
                GetPositions(pieces[Player.Attacker], typeof(Pieces.BasicPiece)),
                Is.EqualTo(
                    new HashSet<IPosition>()
                    {
                        new Position(0, 3),
                        new Position(0, 4),
                        new Position(0, 5),
                        new Position(0, 6),
                        new Position(0, 7),
                        new Position(1, 5),
                        new Position(3, 0),
                        new Position(4, 0),
                        new Position(5, 0),
                        new Position(6, 0),
                        new Position(7, 0),
                        new Position(5, 1),
                        new Position(5, 9),
                        new Position(3, 10),
                        new Position(4, 10),
                        new Position(5, 10),
                        new Position(6, 10),
                        new Position(7, 10),
                        new Position(9, 5),
                        new Position(10, 3),
                        new Position(10, 4),
                        new Position(10, 5),
                        new Position(10, 6),
                        new Position(10, 7)
                    }
                )
            );

            // Check defender basic pieces correct placement
            Assert.That(
                GetPositions(pieces[Player.Defender], typeof(Pieces.BasicPiece)),
                Is.EqualTo(
                    new HashSet<IPosition>()
                    {
                        new Position(3, 5),
                        new Position(4, 4),
                        new Position(4, 5),
                        new Position(4, 6),
                        new Position(5, 3),
                        new Position(5, 4),
                        new Position(5, 6),
                        new Position(5, 7),
                        new Position(6, 4),
                        new Position(6, 5),
                        new Position(6, 6),
                        new Position(7, 5)
                    }
                )
            );
        }

        /// <summary>
        /// Test the match setup according to the variant mode settings.
        /// </summary>
        [Test]
        public void TestVariantModeMatchSetup()
        {
            ISettingsLoader loader = new SettingsLoader();
            try
            {
                loader.LoadVariantModeConfig(_cellsCollBuilder, _piecesCollBuilder);
            }
            catch (IOException ex)
            {
                Assert.Fail("Error: could not read configuration file. " + ex.ToString());
            }

            var cells = _cellsCollBuilder.Build();
            var pieces = _piecesCollBuilder.Build();

            // Check king and throne correct placement
            Assert.That(
                pieces[Player.Defender][new Position(5, 5)].GetType(),
                Is.EqualTo(typeof(Pieces.King))
            );
            Assert.That(
                cells[new Position(5, 5)].GetType(),
                Is.EqualTo(typeof(Cells.Throne))
            );

            // Check exits correct placement
            Assert.That(
                GetPositions(cells, typeof(Cells.Exit)),
                Is.EqualTo(
                    new HashSet<IPosition>()
                    {
                        new Position(0, 0),
                        new Position(0, 10),
                        new Position(10, 0),
                        new Position(10, 10)
                    }
                )
            );

            // Check sliders correct placement
            Assert.That(
                GetPositions(cells, typeof(Cells.Slider)),
                Is.EqualTo(
                    new HashSet<IPosition>()
                    {
                        new Position(2, 2),
                        new Position(2, 8),
                        new Position(8, 2),
                        new Position(8, 8)
                    }
                )
            );

            // Check classic cells correct placement
            Assert.That(
                GetPositions(cells, typeof(Cells.ClassicCell)),
                Is.EqualTo(
                    GenerateAllPositions().AsQueryable()
                        // filter out throne position
                        .Where(pos => !pos.Equals(new Position(5, 5)))
                        // filter out exits positions
                        .Where(pos => !GetPositions(cells, typeof(Cells.Exit)).Contains(pos))
                        // filter out sliders positions
                        .Where(pos => !GetPositions(cells, typeof(Cells.Slider)).Contains(pos))
                        .ToHashSet()
                )
            );

            // Check queens correct placement
            Assert.That(
                GetPositions(pieces[Player.Attacker], typeof(Pieces.Queen)),
                Is.EqualTo(
                    new HashSet<IPosition>()
                    {
                        new Position(0, 5)
                    }
                )
            );
            Assert.That(
                GetPositions(pieces[Player.Defender], typeof(Pieces.Queen)),
                Is.EqualTo(
                    new HashSet<IPosition>()
                    {
                        new Position(6, 5)
                    }
                )
            );

            // Check archers correct placement
            Assert.That(
                GetPositions(pieces[Player.Attacker], typeof(Pieces.Archer)),
                Is.EqualTo(
                    new HashSet<IPosition>()
                    {
                        new Position(5, 0),
                        new Position(5, 10)
                    }
                )
            );
            Assert.That(
                GetPositions(pieces[Player.Defender], typeof(Pieces.Archer)),
                Is.EqualTo(
                    new HashSet<IPosition>()
                    {
                        new Position(5, 4),
                        new Position(5, 6)
                    }
                )
            );

            // Check shields correct placement
            Assert.That(
                GetPositions(pieces[Player.Attacker], typeof(Pieces.Shield)),
                Is.EqualTo(
                    new HashSet<IPosition>()
                    {
                        new Position(1, 5),
                        new Position(9, 5)
                    }
                )
            );
            Assert.That(
                GetPositions(pieces[Player.Defender], typeof(Pieces.Shield)),
                Is.EqualTo(
                    new HashSet<IPosition>()
                    {
                        new Position(5, 3),
                        new Position(5, 7)
                    }
                )
            );

            // Check swappers correct placement
            Assert.That(
                GetPositions(pieces[Player.Attacker], typeof(Pieces.Swapper)),
                Is.EqualTo(
                    new HashSet<IPosition>()
                    {
                        new Position(10, 5)
                    }
                )
            );
            Assert.That(
                GetPositions(pieces[Player.Defender], typeof(Pieces.Swapper)),
                Is.EqualTo(
                    new HashSet<IPosition>()
                    {
                        new Position(4, 5)
                    }
                )
            );

            // Check attacker basic pieces correct placement
            Assert.That(
                GetPositions(pieces[Player.Attacker], typeof(Pieces.BasicPiece)),
                Is.EqualTo(
                    new HashSet<IPosition>()
                    {
                        new Position(0, 3),
                        new Position(0, 4),
                        new Position(0, 6),
                        new Position(0, 7),
                        new Position(3, 0),
                        new Position(4, 0),
                        new Position(6, 0),
                        new Position(7, 0),
                        new Position(5, 1),
                        new Position(5, 9),
                        new Position(3, 10),
                        new Position(4, 10),
                        new Position(6, 10),
                        new Position(7, 10),
                        new Position(10, 3),
                        new Position(10, 4),
                        new Position(10, 6),
                        new Position(10, 7)
                    }
                )
            );

            // Check defender basic pieces correct placement
            Assert.That(
                GetPositions(pieces[Player.Defender], typeof(Pieces.BasicPiece)),
                Is.EqualTo(
                    new HashSet<IPosition>()
                    {
                        new Position(3, 5),
                        new Position(4, 4),
                        new Position(4, 6),
                        new Position(6, 4),
                        new Position(6, 6),
                        new Position(7, 5)
                    }
                )
            );
        }

        private ISet<IPosition> GetPositions<T1, T2>(IDictionary<IPosition, T1> map, T2 targetType)
        {
            return map.AsQueryable()
                    // filter out the objects that are not instances of the target class
                    .Where(entry => entry.Value.GetType().Equals(targetType))
                    // get the positions of objects that are instances of the target class
                    .Select(entry => entry.Key)
                    .ToHashSet();
        }

        private ISet<IPosition> GenerateAllPositions()
        {
            return Enumerable.Range(0, BoardSize)
                    .Select(row => Enumerable.Range(0, BoardSize)
                                    .Select(col => new Position(row, col))
                                    .ToHashSet())
                    .Aggregate(new HashSet<IPosition>(), (set, subset) => set.Concat(subset).ToHashSet());
        }
    }
}