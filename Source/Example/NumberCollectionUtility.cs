using ConsoleUtility.Prompt;
using System;

namespace Example {
    public class NumberCollectionUtility {
        private NumberCollectionUtility(Action<double[]> callback) {
            Callback = callback;
        }
        
        public static Menu CollectNumbers(string operationName, Action<double[]> callback) {
            var utility = new NumberCollectionUtility(callback);
            return new Menu(null, new[] { new MenuItem() { Text = $"How manu numbers would you like to {operationName ?? "use"}?", Execute = utility.SelectNumberCount } });
        }

        private Menu SelectNumberCount(string input) {
            int.TryParse(input, out int count);
            if (count == 0)
                return null;

            Numbers = new double[count];
            return GetNumberMenu();
        }

        private Menu StoreNumber(string input) {
            double.TryParse(input, out double number);
            Numbers[Index++] = number;

            if (Index < Numbers.Length)
                return GetNumberMenu();
            
            Callback(Numbers);
            return null;
        }

        private Menu GetNumberMenu() {
            return new Menu(null, new[] { new MenuItem() { Text = $"#{Index + 1}:", Execute = StoreNumber } });
        }

        private int Index { get; set; }
        private double[] Numbers { get; set; }
        private Action<double[]> Callback { get; set; }
    }
}
