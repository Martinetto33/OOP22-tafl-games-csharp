using Builders;

namespace SettingsLoader
{
    /// <summary>
    /// This interface allows to load the configuration settings for the setup of the board
    /// for each game mode.
    /// </summary>
    public interface ISettingsLoader
    {
        /// <summary>
        /// Loads the classic mode configuration for the setup of the board
        /// and directs the cells and pieces collection building accordingly.
        /// </summary>
        /// <param name="cellsCollBuilder">the cells collection builder</param>
        /// <param name="piecesCollBuilder">the pieces collection builder</param>
        /// <exception cref="IOException">if an error occurs while loading the configuration settings</exception>
        void LoadClassicModeConfig(ICellsCollectionBuilder cellsCollBuilder, IPiecesCollectionBuilder piecesCollBuilder);

        /// <summary>
        /// Loads the variant mode configuration for the setup of the board
        /// and directs the cells and pieces collection building accordingly.
        /// </summary>
        /// <param name="cellsCollBuilder">the cells collection builder</param>
        /// <param name="piecesCollBuilder">the pieces collection builder</param>
        /// <exception cref="IOException">if an error occurs while loading the configuration settings</exception>
        void LoadVariantModeConfig(ICellsCollectionBuilder cellsCollBuilder, IPiecesCollectionBuilder piecesCollBuilder);
    }
}
