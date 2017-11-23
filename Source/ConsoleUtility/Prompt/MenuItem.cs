using System;

namespace ConsoleUtility.Prompt {
    public class MenuItem {
        public string Key { get; set; }

        public string Text { get; set; }

        public Func<string, Menu> Execute { get; set; }
    }
}