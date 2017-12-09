using System.Collections.Generic;

namespace ConsoleUtility.Input {
    /// <summary>
    /// A utility for reading application arguments.
    /// </summary>
    public class ArgumentReader {
        /// <summary>
        /// A utility for reading application arguments.
        /// </summary>
        /// <param name="arguments">Application arguments to read.</param>
        public ArgumentReader(string[] arguments) {
            if (arguments == null) {
                Values = null;
                return;
            }

            Values = new Dictionary<string, string>();
            foreach (string argument in arguments) {
                Process(argument);
            }
        }

        private void Process(string argument) {
            if (string.IsNullOrWhiteSpace(argument))
                return;

            var parts = argument.Split('=');
            if (parts.Length == 1)
                Values.Add(parts[0], "true");
            if (parts.Length == 2)
                Values.Add(parts[0], parts[1]);
            if (parts.Length > 2)
                Values.Add(parts[0], string.Join("=", parts, 1, parts.Length - 1));
        }

        /// <summary>
        /// Gets value with the specified key.
        /// </summary>
        /// <param name="key">The value key.</param>
        public string GetValue(string key) {
            if (Values == null)
                return null;
            if (Values.ContainsKey(key))
                return Values[key];
            return "false";
        }

        private Dictionary<string, string> Values { get; set; }
    }
}
