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

        private XElement _settings;

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
            _settings = LoadSettingsFromFile(VariantModeConfigFile);
            LoadBoardSize(cellsCollBuilder);
            LoadKingAndThroneData(cellsCollBuilder, piecesCollBuilder);
            LoadExitsData(cellsCollBuilder);
            LoadSlidersData(cellsCollBuilder);
            LoadClassicCellsData(cellsCollBuilder);
            LoadBasicPiecesData(piecesCollBuilder);
            LoadQueensData(piecesCollBuilder);
            LoadArchersData(piecesCollBuilder);
            LoadShieldsData(piecesCollBuilder);
            LoadSwappersData(piecesCollBuilder);
        }

        private XElement LoadSettingsFromFile(string resourceName)
        {
            try
            {
                Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(resourceName);
                StreamReader reader = new StreamReader(stream);
                string xmlConfigFileText = reader.ReadToEnd();
                return XElement.Parse(xmlConfigFileText);
            }
            catch (Exception ex)
            {
                throw new IOException(
                    "An error occurred while trying to get or parse the configuration file."
                    + ex.Message
                );
            }
        }

        private void LoadBoardSize(ICellsCollectionBuilder cellsCollBuilder)
        {
            string tagName = "BoardSize";
            string? boardSize = _settings.Element(tagName)?.Value;
            if (boardSize is not null)
            {
                cellsCollBuilder.AddBoardSize(Int32.Parse(boardSize));
            }
            else
            {
                throw new IOException($"Error: data at tag {tagName} is not present");
            }
        }

        private void LoadKingAndThroneData(ICellsCollectionBuilder cellsCollBuilder, IPiecesCollectionBuilder piecesCollBuilder)
        {
            string tagName = "KingPosition";
            XElement? kingPositionElement = _settings.Element(tagName)?.Element("Position");
            string? row = kingPositionElement?.Attribute("row")?.Value;
            string? column = kingPositionElement?.Attribute("column")?.Value;
            if (row is not null && column is not null)
            {
                IPosition kingPosition = new Position(Int32.Parse(row), Int32.Parse(column));
                piecesCollBuilder.AddKing(kingPosition);
                cellsCollBuilder.AddThrone(kingPosition);
            }
            else
            {
                throw new IOException(
                    $"Error: data at tag {tagName} is not present or does not follow the correct format"
                );
            }
        }

        private void LoadExitsData(ICellsCollectionBuilder cellsCollBuilder)
        {
            cellsCollBuilder.AddExits(GetPositionsByTagName("ExitsPositions"));
        }

        private void LoadSlidersData(ICellsCollectionBuilder cellsCollBuilder)
        {
            cellsCollBuilder.AddSliders(GetPositionsByTagName("SlidersPositions"));
        }

        private void LoadClassicCellsData(ICellsCollectionBuilder cellsCollBuilder) => cellsCollBuilder.AddClassicCells();

        private void LoadBasicPiecesData(IPiecesCollectionBuilder piecesCollBuilder)
        {
            piecesCollBuilder.AddBasicPieces(GetPiecesPositionsForEachTeam("BasicPieces"));
        }

        private void LoadQueensData(IPiecesCollectionBuilder piecesCollBuilder)
        {
            piecesCollBuilder.AddQueens(GetPiecesPositionsForEachTeam("Queens"));
        }

        private void LoadArchersData(IPiecesCollectionBuilder piecesCollBuilder)
        {
            piecesCollBuilder.AddArchers(GetPiecesPositionsForEachTeam("Archers"));
        }

        private void LoadShieldsData(IPiecesCollectionBuilder piecesCollBuilder)
        {
            piecesCollBuilder.AddShields(GetPiecesPositionsForEachTeam("Shields"));
        }

        private void LoadSwappersData(IPiecesCollectionBuilder piecesCollBuilder)
        {
            piecesCollBuilder.AddSwappers(GetPiecesPositionsForEachTeam("Swappers"));
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
            IEnumerable<XElement>? positionsElements = _settings.Element(tagName)?.Elements("Position");
            if (positionsElements is not null)
            {
                foreach (XElement? singlePosElement in positionsElements)
                {
                    string? row = singlePosElement?.Attribute("row")?.Value;
                    string? column = singlePosElement?.Attribute("column")?.Value;
                    if (row is not null && column is not null)
                    {
                        positions.Add(new Position(Int32.Parse(row), Int32.Parse(column)));
                    }
                    else
                    {
                        throw new IOException(
                            $"Error: data at tag {tagName} is not present or does not follow the correct format"
                        );
                    }
                }
                return positions;
            }
            else
            {
                throw new IOException(
                    $"Error: data at tag {tagName} is not present or does not follow the correct format"
                );
            }
        }

    }
}
