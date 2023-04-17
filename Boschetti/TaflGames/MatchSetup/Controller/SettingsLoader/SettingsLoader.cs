using Builders;
using Common;
using System.Xml.Linq;

namespace SettingsLoader
{
    public class SettingsLoader : ISettingsLoader
    {
        private const string ConfigFilesPath = "Resources/Config/";
        private const string ClassicModeConfigFile = "ClassicModeSettings.xml";
        private const string VariantModeConfigFile = "VariantModeSettings.xml";

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

        private XElement LoadSettingsFromFile(string filename)
        {
            try
            {
                string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
                string xmlConfigFileText = File.ReadAllText(baseDirectory + ConfigFilesPath + filename);
                return XElement.Parse(xmlConfigFileText);
            }
            catch (Exception)
            {
                throw new IOException("An error occurred while trying to get or parse the configuration file.");
            }
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

        private void LoadClassicCellsData(ICellsCollectionBuilder cellsCollBuilder) => cellsCollBuilder.AddBasicCells();

        private void LoadBasicPiecesData(IPiecesCollectionBuilder piecesCollBuilder)
        {
            piecesCollBuilder.AddBasicPieces(GetPiecesPositionsForEachTeam("BasicPieces"));
        }

        private IDictionary<Player, ISet<IPosition>> GetPiecesPositionsForEachTeam(string piecesName)
        {
            return new Dictionary<Player, ISet<IPosition>>
                   {
                       { Player.Attacker, GetPositionsByTagName("Attacker" + piecesName + "Positions") },
                       { Player.Attacker, GetPositionsByTagName("Defender" + piecesName + "Positions") }
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
