using ConsoleUtility.Prompt;
using NUnit.Framework;

namespace ConsoleUtility.Test.Prompt {
    [TestFixture]
    public class MenuTests {
        [Test]
        public void ConstructorSetsHeader() {
            string[] expected = {
                "Welcome to this console application.",
                "There are a number of wonderful options.",
                "Please review the options and pick one." };
            Menu menu;

            menu = new Menu(expected);

            AssertArray(expected, menu.Header, "Header");
        }

        [Test]
        public void ConstructorSetsHeaderToEmptyArrayForNull() {
            Menu menu;

            menu = new Menu(null);

            AssertArray(new string[0], menu.Header, "Header");
        }

        private void AssertArray(string[] expected, string[] actual, string itemName) {
            if (expected != null) {
                Assert.IsNotNull(actual, itemName);
                for (int i = 0; i < expected.Length && i < actual.Length; ++i) {
                    Assert.AreEqual(expected[i], actual[i], $"{itemName}[{i}]");
                }
                Assert.AreEqual(expected.Length, actual.Length, $"{itemName} Count");
            } else {
                Assert.IsNull(actual, itemName);
            }
        }
    }
}
