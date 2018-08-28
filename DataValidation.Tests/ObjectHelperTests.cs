using DataValidation.Extensions;
using NUnit.Framework;

namespace DataValidation.Tests
{
    [TestFixture]
    public class ObjectHelperTests
    {
        [Test]
        public void SetIf_returns_setValue_when_ifFunc_is_true()
        {
            var testString = "bananas";

            Assert.AreEqual("apples",
                testString.SetIf(value => value.Equals("bananas"), "apples"));

        }

        [Test]
        public void SetIf_returns_value_when_ifFunc_is_false()
        {
            var testString = "grapes";

            Assert.AreEqual("grapes",
                testString.SetIf(value => value.Equals("bananas"), "apples"));

        }
    }
}