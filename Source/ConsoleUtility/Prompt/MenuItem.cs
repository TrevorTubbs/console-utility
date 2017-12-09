using System;

namespace ConsoleUtility.Prompt {
    /// <summary>
    /// Represents and item in a menu.
    /// </summary>
    public class MenuItem {
        /// <summary>
        /// The unique key of this item relative to other items.
        /// </summary>
        public string Key { get; set; }

        /// <summary>
        /// The text to display.
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// The method to execute when this item is selected.
        /// </summary>
        public Func<string, Menu> Execute { get; set; }
    }
}