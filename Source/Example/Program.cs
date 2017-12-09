using ConsoleUtility.Input;
using ConsoleUtility.Prompt;
using System;

namespace Example {
    /// <summary>
    /// A simple example program to demonstrate how the console utility can be used.
    /// </summary>
    class Program {
        static int Main(string[] args) {
            var reader = new ArgumentReader(args);
            var name = reader.GetValue("name");
            Console.WriteLine($"Hello {name ?? "World"}!");

            var manager = new MenuManager(BuildMainMenu(), Console.WriteLine);
            while (Running) {
                manager.Show();
                manager.Select(Console.ReadLine());
            }
            
            return 0;
        }

        private static Menu BuildMainMenu() {
            return MenuUtility.BuildMenu(Console.WriteLine, Exit);
        }

        private static void Exit() {
            Running = false;
        }

        private static bool Running { get; set; } = true;
    }
}
