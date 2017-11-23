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

            menu = new Menu(expected, null);

            AssertArray(expected, menu.Header, "Header");
        }

        [Test]
        public void ConstructorSetsHeaderToEmptyArrayForNull() {
            Menu menu;

            menu = new Menu(null, null);

            AssertArray(new string[0], menu.Header, "Header");
        }

        [Test]
        public void ConstructorSetsItems() {
            MenuItem[] items = new[] {
                new MenuItem(),
                new MenuItem(),
                new MenuItem()
            };
            Menu menu;

            menu = new Menu(null, items);

            AssertArray(items, menu.Items, "Items");
        }

        [Test]
        public void ConstructorSetsItemsToEmptyArrayForNull() {
            Menu menu;

            menu = new Menu(null, null);

            AssertArray(new MenuItem[0], menu.Items, "Items");
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

        private void AssertArray(MenuItem[] expected, MenuItem[] actual, string itemName) {
            if (expected != null) {
                Assert.IsNotNull(actual, itemName);
                for (int i = 0; i < expected.Length && i < actual.Length; ++i) {
                    Assert.AreEqual(expected[i].Key, actual[i].Key, $"{itemName}[{i}].Key");
                    Assert.AreEqual(expected[i].Text, actual[i].Text, $"{itemName}[{i}].Text");
                    Assert.AreSame(expected[i].Execute, actual[i].Execute, $"{itemName}[{i}].Execute");
                }
                Assert.AreEqual(expected.Length, actual.Length, $"{itemName} Count");
            } else {
                Assert.IsNull(actual, itemName);
            }
        }
    }
}