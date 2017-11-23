using ConsoleUtility.Prompt;
using NUnit.Framework;
using System.Collections.Generic;

namespace ConsoleUtility.Test.Prompt {
    [TestFixture]
    public class MenuManagerTests {
        [SetUp]
        public void SetUp() {
            ExecuteList = new List<KeyValuePair<int, string>>();
            Output = new List<string>();
            MainMenu = new Menu(new[] { "Just a simple header", "with 2 lines." }, new[] {
                new MenuItem() { Text = "First Choice", Key = "WER", Execute = ExecuteFirstChoice },
                new MenuItem() { Text = "Second Choice", Key = "SDF", Execute = ExecuteSecondChoice },
                new MenuItem() { Text = "Third Choice", Key = "XCV", Execute = ExecuteThirdChoice }
            });
            Manager = new MenuManager(MainMenu, WriteOutput);
        }

        [TearDown]
        public void TearDown() {
            Manager = null;
            MainMenu = null;
            Output = null;
            ExecuteList = null;
        }

        public MenuManager Manager { get; set; }
        public Menu MainMenu { get; set; }

        [Test]
        public void ShowWritesMenuText() {
            Manager.Show();

            for (int i = 0; i < Output.Count && i < MainMenu.Header.Length; ++i) {
                Assert.AreEqual(MainMenu.Header[i], Output[i], $"Output[{i}]");
            }
            Assert.AreEqual(string.Empty, Output[MainMenu.Header.Length], "Line between header and menu items.");
            for (int i = 0; i + MainMenu.Header.Length + 1 < Output.Count  && i < MainMenu.Items.Length; ++i) {
                Assert.AreEqual($"{i + 1}. {MainMenu.Items[i].Text}", Output[i + MainMenu.Header.Length + 1], $"Output[{i + MainMenu.Header.Length + 1}]");
            }
            Assert.AreEqual(MainMenu.Header.Length + MainMenu.Items.Length + 1, Output.Count, "Line Count");
        }

        [TestCase("1", 1, "WER")]
        [TestCase("WER", 1, "WER")]
        [TestCase("wer", 1, "WER")]
        [TestCase("2", 2, "SDF")]
        [TestCase("SDF", 2, "SDF")]
        [TestCase("sdf", 2, "SDF")]
        [TestCase("3", 3, "XCV")]
        [TestCase("XCV", 3, "XCV")]
        [TestCase("xcv", 3, "XCV")]
        public void SelectInvokesTheCorrectExecuteMethod(string input, int expectedOption, string expectedKey) {
            Manager.Select(input);

            Assert.LessOrEqual(1, ExecuteList.Count, "At least one option executed.");
            Assert.AreEqual(expectedOption, ExecuteList[0].Key, "Option");
            Assert.AreEqual(expectedKey, ExecuteList[0].Value, "Select Key");
            Assert.AreEqual(1, ExecuteList.Count, "Execution Count");
        }

        public void WriteOutput(string output) {
            Output.Add(output);
        }

        private List<string> Output { get; set; }

        private Menu ExecuteFirstChoice(string key) {
            ExecuteList.Add(new KeyValuePair<int, string>(1, key));
            return null;
        }

        private Menu ExecuteSecondChoice(string key) {
            ExecuteList.Add(new KeyValuePair<int, string>(2, key));
            return null;
        }

        private Menu ExecuteThirdChoice(string key) {
            ExecuteList.Add(new KeyValuePair<int, string>(3, key));
            return null;
        }

        private List<KeyValuePair<int, string>> ExecuteList { get; set; }
    }
}