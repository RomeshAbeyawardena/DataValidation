using DataValidation.Extensions;
using DataValidation.Mvc;
using NUnit.Framework;

namespace DataValidation.Tests
{
    [TestFixture]
    public class UkPostalCodeAttributeTests
    {
        [Test]
        public void UkPostalCodeAttribute_is_valid()
        {
            var expectedRegularExpression = "[A-Z]{1}";
            _systemUnderTest = new UkPostalCodeAttribute();
            
            Assert.AreEqual(RegularExpressions.UkPostalCode, _systemUnderTest.Pattern);

            Assert.IsTrue(
                _systemUnderTest.IsValid("LU2 7FH"));

            _systemUnderTest = new UkPostalCodeAttribute(expectedRegularExpression);

            Assert.AreEqual(expectedRegularExpression, _systemUnderTest.Pattern);

        }

        private UkPostalCodeAttribute _systemUnderTest;
    }
}