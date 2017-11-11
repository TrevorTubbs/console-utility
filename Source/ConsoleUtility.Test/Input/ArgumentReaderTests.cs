using ConsoleUtility.Input;
using NUnit.Framework;

namespace ConsoleUtility.Test.Input {
    [TestFixture]
    public class ArgumentReaderTests {
        [SetUp]
        public void SetUp() {
            Reader = new ArgumentReader();
        }

        [TearDown]
        public void TearDown() {
            Reader = null;
        }

        private ArgumentReader Reader { get; set; }

        [Test]
        public void SetArgumentsAcceptsNull() {
            Reader.SetArguments(null);
        }

        [Test]
        public void SetArgumentsAcceptsEmptyArray() {
            Reader.SetArguments(new string[0]);
        }

        [Test]
        public void SetArgumentsAcceptsArrayWithNullElement() {
            Reader.SetArguments(new[] { "foo", null, "bar" });
        }

        [TestCaseSource("ArgumentsTestCaseSource")]
        public void GetValue(string[] arguments, string key, string expected) {
            Reader.SetArguments(arguments);

            string actual = Reader.GetValue(key);

            Assert.AreEqual(expected, actual, $"GetValue(\"{key}\")");
        }

        public static object[] ArgumentsTestCaseSource {
            get {
                var argumentList1 = new[] { "seed=87", "key=lkljsdf", "autostart", "name=Jake", "hint=42=21+21" };
                return new object[] {
                    new object[] { argumentList1, "seed", "87" },
                    new object[] { argumentList1, "key", "lkljsdf" },
                    new object[] { argumentList1, "autostart", "true" },
                    new object[] { argumentList1, "autostop", "false" },
                    new object[] { argumentList1, "name", "Jake" },
                    new object[] { argumentList1, "hint", "42=21+21" }
                };
            }
        }
    }
}
