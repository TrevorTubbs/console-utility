namespace ConsoleUtility.Prompt {
    public class Menu {
        public Menu(string[] header) {
            Header = header ?? new string[0];
        }

        public string[] Header { get; private set; }
    }
}
