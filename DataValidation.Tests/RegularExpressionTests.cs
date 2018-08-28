using System.Text.RegularExpressions;
using DataValidation.Extensions;
using NUnit.Framework;

namespace DataValidation.Tests
{
    [TestFixture]
    public class RegularExpressionTests
    {
        [TestCase("E13 8LL")]
        [TestCase("LU2 7FH")]
        [TestCase("GX11 1AA")]
        public void UkPostalCode_IsValid(string postalCode)
        {
            Assert.IsTrue(Regex.IsMatch(postalCode, RegularExpressions.UkPostalCode));
        }

        [TestCase("E13 8L1")]
        [TestCase("2U2 7FH")]
        [TestCase("8X11 1AA")]
        public void UkPostalCode_IsInvalid(string postalCode)
        {
            Assert.IsFalse(Regex.IsMatch(postalCode, RegularExpressions.UkPostalCode));
        }

        [TestCase("35801")]
        [TestCase("52801")]
        [TestCase("84323")]
        [TestCase("82941")]
        public void UsZipCode_IsValid(string zipCode)
        {
            Assert.IsTrue(Regex.IsMatch(zipCode, RegularExpressions.UsZipCode));
        }

        [TestCase("3")]
        [TestCase("52")]
        [TestCase("843")]
        [TestCase("8294")]
        [TestCase("8294A")]
        public void UsZipCode_IsInvalid(string zipCode)
        {
            Assert.IsFalse(Regex.IsMatch(zipCode, RegularExpressions.UsZipCode));
        }
    }
}
