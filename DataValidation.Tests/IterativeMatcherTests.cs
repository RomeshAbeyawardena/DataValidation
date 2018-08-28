using System.Text;
using DataValidation.Extensions;
using DataValidation.Interfaces;
using NUnit.Framework;

namespace DataValidation.Tests
{
    [TestFixture]
    public class IterativeMatcherTests
    {
        [Test]
        public void GetMatch_returns_null_when_validationFunction_returns_false()
        {
            _systemUnderTest = new IterativeMatcher();
            var stringBuilder = new StringBuilder();
            var letters = new[] {'d', 'o', 'g', 'g', 'i', 'e'};

            var i = 0;

            
            Assert.IsNull(_systemUnderTest.GetMatch(() => i < letters.Length ? stringBuilder.Append(letters[i++]) : stringBuilder,
                result => result.ToString().Equals("cat")));
        }
        [Test]
        public void GetMatch_returns_when_validationFunction_returns_true()
        {
            _systemUnderTest = new IterativeMatcher();
            var stringBuilder = new StringBuilder();
            var letters = new[] {'c', 'a', 't', 't', 'l', 'e'};

            var i = 0;
            Assert.AreEqual("cat", _systemUnderTest.GetMatch(() => stringBuilder.Append(letters[i++]) , result => result.ToString().Equals("cat")).ToString());
        }

        private IIterativeMatcher _systemUnderTest;
    }
}