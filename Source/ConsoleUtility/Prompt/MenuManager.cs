using System;

namespace ConsoleUtility.Prompt {
    public class MenuManager {
        public MenuManager(Menu mainMenu, Action<string> write) {
            MainMenu = mainMenu;
            Write = write ?? EmptyWrite;
        }

        public void Show() {
            foreach (string line in MainMenu.Header) {
                Write(line);
            }
            Write(string.Empty);
            for (int i = 0; i < MainMenu.Items.Length; ++i) {
                Write($"{i + 1}. {MainMenu.Items[i].Text}");
            }
        }

        public void Select(string key) {
            bool useNumber = int.TryParse(key, out int number);
            for (int i = 0; i < MainMenu.Items.Length; ++i) {
                if ((useNumber && number - 1 == i)  || string.Compare(key, MainMenu.Items[i].Key, StringComparison.CurrentCultureIgnoreCase) == 0) {
                    MainMenu.Items[i].Execute(MainMenu.Items[i].Key);
                }
            }
        }

        private Menu MainMenu { get; set; }

        private Action<string> Write { get; set; }

        private void EmptyWrite(string ouptput) { }
    }
}