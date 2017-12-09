using ConsoleUtility.Input;
using NUnit.Framework;

namespace ConsoleUtility.Test.Input {
    [TestFixture]
    public class ArgumentReaderTests {
        [Test]
        public void SetArgumentsAcceptsNull() {
            new ArgumentReader(null);
        }

        [Test]
        public void SetArgumentsAcceptsEmptyArray() {
            new ArgumentReader(new string[0]);
        }

        [Test]
        public void SetArgumentsAcceptsArrayWithNullElement() {
            new ArgumentReader(new[] { "foo", null, "bar" });
        }

        [TestCaseSource("ArgumentsTestCaseSource")]
        public void GetValue(string[] arguments, string key, string expected) {
            var reader = new ArgumentReader(arguments);

            string actual = reader.GetValue(key);

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
