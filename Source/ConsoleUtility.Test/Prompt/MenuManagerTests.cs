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
            SubMenu = null;
        }

        public MenuManager Manager { get; set; }
        public Menu MainMenu { get; set; }

        [Test]
        public void ShowWritesMenuText() {
            Manager.Show();

            Assert.AreEqual(string.Empty, Output[0], "Line before menu header.");
            for (int i = 0; i + 1 < Output.Count && i < MainMenu.Header.Length; ++i) {
                Assert.AreEqual(MainMenu.Header[i], Output[i + 1], $"Output[{i + 1}]");
            }
            for (int i = 0; i + MainMenu.Header.Length + 1 < Output.Count && i < MainMenu.Items.Length; ++i) {
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

        [Test]
        public void ShowPrintsSubMenuAfterSelect() {
            SubMenu = new Menu(new[] { "header line 1", "header line 2" },
                      new[] {
                          new MenuItem() { Key = "u", Text = "uiop" },
                          new MenuItem() { Key = "j", Text = "jkl" },
                          new MenuItem() { Key = "n", Text = "nm" }
                      });
            Manager.Show();
            Manager.Select("1");
            Output.Clear();

            Manager.Show();

            Assert.AreEqual(string.Empty, Output[0], "Line before sub menu.");
            for (int i = 0; i + 1 < Output.Count && i < SubMenu.Header.Length; ++i) {
                Assert.AreEqual(SubMenu.Header[i], Output[i + 1], $"Output[{i + 1}]");
            }
            for (int i = 0; i + SubMenu.Header.Length + 1 < Output.Count && i < SubMenu.Items.Length; ++i) {
                Assert.AreEqual($"{i + 1}. {SubMenu.Items[i].Text}", Output[i + SubMenu.Header.Length + 1], $"Output[{i + SubMenu.Header.Length + 1}]");
            }
            Assert.AreEqual(SubMenu.Header.Length + SubMenu.Items.Length + 1, Output.Count, "Line Count");
        }

        [Test]
        public void ShowOmitsNumberPrefixForOneOption() {
            SubMenu = new Menu(new[] { "header line 1", "header line 2" },
                      new[] {
                          new MenuItem() { Key = "u", Text = "uiop" }
                      });
            Manager.Show();
            Manager.Select("1");
            Output.Clear();

            Manager.Show();

            Assert.AreEqual(string.Empty, Output[0], "Line before sub menu.");
            for (int i = 0; i + 1 < Output.Count && i < SubMenu.Header.Length; ++i) {
                Assert.AreEqual(SubMenu.Header[i], Output[i + 1], $"Output[{i + 1}]");
            }
            for (int i = 0; i + SubMenu.Header.Length + 1 < Output.Count && i < SubMenu.Items.Length; ++i) {
                Assert.AreEqual($"{SubMenu.Items[i].Text}", Output[i + SubMenu.Header.Length + 1], $"Output[{i + SubMenu.Header.Length + 1}]");
            }
            Assert.AreEqual(SubMenu.Header.Length + SubMenu.Items.Length + 1, Output.Count, "Line Count");
        }

        [Test]
        public void SelectInvokesExecuteMethodOnSubMenu() {
            int count = 0;
            SubMenu = new Menu(new[] { "header line 1", "header line 2" },
                      new[] {
                          new MenuItem() { Key = "u", Text = "uiop" },
                          new MenuItem() { Key = "j", Text = "jkl", Execute = (input) => {
                              ++count;
                              return null;
                          } },
                          new MenuItem() { Key = "n", Text = "nm" }
                      });
            Manager.Show();
            Manager.Select("1");
            Manager.Show();

            Manager.Select("j");

            Assert.AreEqual(1, count, "Execution Count");
        }

        [Test]
        public void ShowUsesMainMenuWhenExecuteReturnsNull() {
            int count = 0;
            SubMenu = new Menu(new[] { "header line 1", "header line 2" },
                      new[] {
                          new MenuItem() { Key = "u", Text = "uiop", Execute = (input) => {
                              ++count;
                              return null;
                          } },
                          new MenuItem() { Key = "j", Text = "jkl" },
                          new MenuItem() { Key = "n", Text = "nm" }
                      });
            Manager.Show();
            Manager.Select("1");
            Manager.Show();
            Manager.Select("u");
            Output.Clear();

            ShowWritesMenuText();
        }

        [Test]
        public void SelectChoosesFirstOptionWhenOnlyOneOption() {
            string expected = "this is my input";
            string actual = null;
            SubMenu = new Menu(null,
                      new[] {
                          new MenuItem() { Key = "u", Text = "uiop", Execute = (input) => {
                              actual = input;
                              return null;
                          } }
                      });
            Manager.Show();
            Manager.Select("1");
            Manager.Show();

            Manager.Select(expected);

            Assert.AreEqual(expected, actual, "Input");
        }

        public void WriteOutput(string output) {
            Output.Add(output);
        }

        private List<string> Output { get; set; }

        private Menu ExecuteFirstChoice(string key) {
            ExecuteList.Add(new KeyValuePair<int, string>(1, key));
            return SubMenu;
        }

        private Menu ExecuteSecondChoice(string key) {
            ExecuteList.Add(new KeyValuePair<int, string>(2, key));
            return SubMenu;
        }

        private Menu ExecuteThirdChoice(string key) {
            ExecuteList.Add(new KeyValuePair<int, string>(3, key));
            return SubMenu;
        }

        private Menu SubMenu { get; set; }

        private List<KeyValuePair<int, string>> ExecuteList { get; set; }
    }
}