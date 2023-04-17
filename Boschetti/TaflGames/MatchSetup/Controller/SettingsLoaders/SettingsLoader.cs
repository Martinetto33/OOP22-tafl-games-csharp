using Builders;
using Common;
using System.Reflection;
using System.Xml.Linq;

namespace SettingsLoaders
{
    /// <summary>
    /// This class loads the settings for the setup of the board for each game mode
    /// from configuration files.
    /// </summary>
    public class SettingsLoader : ISettingsLoader
    {
        private const string ClassicModeConfigFile = "MatchSetup.Resources.Config.ClassicModeSettings.xml";
        private const string VariantModeConfigFile = "MatchSetup.Resources.Config.VariantModeSettings.xml";

        private XElement? _settings;

        public void LoadClassicModeConfig(ICellsCollectionBuilder cellsCollBuilder, IPiecesCollectionBuilder piecesCollBuilder)
        {
            _settings = LoadSettingsFromFile(ClassicModeConfigFile);
            LoadBoardSize(cellsCollBuilder);
            LoadKingAndThroneData(cellsCollBuilder, piecesCollBuilder);
            LoadExitsData(cellsCollBuilder);
            LoadClassicCellsData(cellsCollBuilder);
            LoadBasicPiecesData(piecesCollBuilder);
        }

        public void LoadVariantModeConfig(ICellsCollectionBuilder cellsCollBuilder, IPiecesCollectionBuilder piecesCollBuilder)
        {
            throw new NotImplementedException();
        }

        private XElement LoadSettingsFromFile(string resourceName)
        {
            Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(resourceName);
            StreamReader reader = new StreamReader(stream);
            string xmlConfigFileText = reader.ReadToEnd();
            return XElement.Parse(xmlConfigFileText);
        }

        private void LoadBoardSize(ICellsCollectionBuilder cellsCollBuilder)
        {
            int boardSize = Int32.Parse(_settings.Element("BoardSize").Value);
            cellsCollBuilder.AddBoardSize(boardSize);
        }

        private void LoadKingAndThroneData(ICellsCollectionBuilder cellsCollBuilder, IPiecesCollectionBuilder piecesCollBuilder)
        {
            XElement kingPositionElement = _settings.Element("KingPosition").Element("Position");
            IPosition kingPosition = new Position(
                Int32.Parse(kingPositionElement.Attribute("row").Value),
                Int32.Parse(kingPositionElement.Attribute("column").Value)
            );
            piecesCollBuilder.AddKing(kingPosition);
            cellsCollBuilder.AddThrone(kingPosition);
        }

        private void LoadExitsData(ICellsCollectionBuilder cellsCollBuilder)
        {
            ISet<IPosition> exitsPositions = GetPositionsByTagName("ExitsPositions");
            cellsCollBuilder.AddExits(exitsPositions);
        }

        private void LoadClassicCellsData(ICellsCollectionBuilder cellsCollBuilder) => cellsCollBuilder.AddClassicCells();

        private void LoadBasicPiecesData(IPiecesCollectionBuilder piecesCollBuilder)
        {
            piecesCollBuilder.AddBasicPieces(GetPiecesPositionsForEachTeam("BasicPieces"));
        }

        private IDictionary<Player, ISet<IPosition>> GetPiecesPositionsForEachTeam(string piecesName)
        {
            return new Dictionary<Player, ISet<IPosition>>
                   {
                       { Player.Attacker, GetPositionsByTagName("Attacker" + piecesName + "Positions") },
                       { Player.Defender, GetPositionsByTagName("Defender" + piecesName + "Positions") }
                   };
        }

        private ISet<IPosition> GetPositionsByTagName(string tagName)
        {
            ISet<IPosition> positions = new HashSet<IPosition>();
            XElement positionsElement = _settings.Element(tagName);
            foreach (XElement singlePosElement in positionsElement.Elements("Position"))
            {
                IPosition position = new Position(
                    Int32.Parse(singlePosElement.Attribute("row").Value),
                    Int32.Parse(singlePosElement.Attribute("column").Value)
                );
                positions.Add(position);
            }
            return positions;
        }

    }
}
