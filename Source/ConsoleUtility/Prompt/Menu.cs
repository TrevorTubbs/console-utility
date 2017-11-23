namespace ConsoleUtility.Prompt {
    public class Menu {
        public Menu(string[] header, MenuItem[] items) {
            Header = header ?? new string[0];
            Items = items ?? new MenuItem[0];
        }

        public string[] Header { get; private set; }

        public MenuItem[] Items { get; private set; }
    }
}