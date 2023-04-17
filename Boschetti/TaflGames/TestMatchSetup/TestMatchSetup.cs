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

        }
    }
}