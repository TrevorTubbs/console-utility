using System;

namespace ConsoleUtility.Prompt {
    /// <summary>
    /// A utility for managing an application menu system.
    /// </summary>
    public class MenuManager {
        /// <summary>
        /// A utility for managing an application menu system.
        /// </summary>
        /// <param name="mainMenu">The main menu of the application.</param>
        /// <param name="write">The method to use for writing output.</param>
        public MenuManager(Menu mainMenu, Action<string> write) {
            MainMenu = mainMenu;
            Write = write ?? EmptyWrite;
        }

        /// <summary>
        /// Writes the menu.
        /// </summary>
        public void Show() {
            Menu menu = SelectedMenu ?? MainMenu;
            Write(string.Empty);
            foreach (string line in menu.Header) {
                Write(line);
            }
            if (menu.Items.Length != 1) {
                for (int i = 0; i < menu.Items.Length; ++i) {
                    Write($"{i + 1}. {menu.Items[i].Text}");
                }
            } else {
                Write(menu.Items[0].Text);
            }
        }

        /// <summary>
        /// Selects the menu item specified by the key.
        /// </summary>
        /// <param name="key">A key indicating which item to select.</param>
        public void Select(string key) {
            Menu menu = SelectedMenu ?? MainMenu;
            MenuItem selection = null;
            string input = null;

            if (menu.Items.Length == 1) {
                selection = menu.Items[0];
                input = key;
            } else {
                bool useNumber = int.TryParse(key, out int number);
                if (useNumber && number - 1 < menu.Items.Length) {
                    selection = menu.Items[number - 1];
                } else {
                    for (int i = 0; i < menu.Items.Length; ++i) {
                        if (string.Compare(key, menu.Items[i].Key, StringComparison.CurrentCultureIgnoreCase) == 0) {
                            selection = menu.Items[i];
                            break;
                        }
                    }
                }
            }

            if (selection != null) {
                SelectedMenu = selection.Execute(input ?? selection.Key);
            }
        }

        private Menu MainMenu { get; set; }

        private Menu SelectedMenu { get; set; }

        private Action<string> Write { get; set; }

        private void EmptyWrite(string ouptput) { }
    }
}