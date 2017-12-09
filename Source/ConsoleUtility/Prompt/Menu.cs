namespace ConsoleUtility.Prompt {
    /// <summary>
    /// Represents an application menu.
    /// </summary>
    public class Menu {
        /// <summary>
        /// Represents an application menu.
        /// </summary>
        /// <param name="header">The header text to display with the menu.</param>
        /// <param name="items">The choices in the menu.</param>
        public Menu(string[] header, MenuItem[] items) {
            Header = header ?? new string[0];
            Items = items ?? new MenuItem[0];
        }

        /// <summary>
        /// The header text to display with the menu.
        /// </summary>
        public string[] Header { get; private set; }

        /// <summary>
        /// The choices in the menu.
        /// </summary>
        public MenuItem[] Items { get; private set; }
    }
}