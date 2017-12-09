using System;

namespace ConsoleUtility.Prompt {
    public class MenuManager {
        public MenuManager(Menu mainMenu, Action<string> write) {
            MainMenu = mainMenu;
            Write = write ?? EmptyWrite;
        }

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