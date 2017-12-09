using ConsoleUtility.Prompt;
using System;

namespace Example {
    public class MenuUtility {
        private MenuUtility(Action<string> write, Action exitCallback) {
            Write = write ?? Empty;
            ExitCallback = exitCallback ?? Empty;
        }

        public static Menu BuildMenu(Action<string> write, Action exitCallback) {
            var utility = new MenuUtility(write, exitCallback);
            var menu = new Menu(new[] { "What math function would you like to use?" },
                new[] {
                    new MenuItem() { Key = "a", Text = "Add", Execute = utility.ChooseAdd },
                    new MenuItem() { Key = "s", Text = "Subtract", Execute = utility.ChooseSubtract },
                    new MenuItem() { Key = "m", Text = "Multiply", Execute = utility.ChooseMultiply },
                    new MenuItem() { Key = "d", Text = "Divide", Execute = utility.ChooseDivide },
                    new MenuItem() { Key = "e", Text = "Exit", Execute = utility.Exit }
                });
            return menu;
        }

        private Menu ChooseAdd(string input) {
            return ChooseNumbers("add", MathFunctions.Add);
        }

        private Menu ChooseSubtract(string input) {
            return ChooseNumbers("subtract", MathFunctions.Subtract);
        }

        private Menu ChooseMultiply(string input) {
            return ChooseNumbers("multiply", MathFunctions.Multiply);
        }

        private Menu ChooseDivide(string input) {
            return ChooseNumbers("divide", MathFunctions.Divide);
        }

        private Menu ChooseNumbers(string operationName, Func<double[], double> method) {
            return NumberCollectionUtility.CollectNumbers(operationName, (numbers) => {
                Write($"Result: { method(numbers) }");
            });
        }

        private Menu Exit(string input) {
            ExitCallback();
            return null;
        }

        private Action<string> Write { get; set; }

        private Action ExitCallback { get; set; }

        private void Empty() { }
        private void Empty(string input) { }
    }
}
