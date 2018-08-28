using DataValidation.Mvc;
using NUnit.Framework;

namespace DataValidation.Tests
{
    [TestFixture]
    public class AreaRouteTests
    {
        [Test]
        public void AreaRoute_includes_area_name()
        {
            var expectedAreaName = "MyTestArea";
            var expectedRouteTemplate = AreaRouteAttribute.DefaultAreaRouteTemplate;
            var systemUnderTest = new AreaRouteAttribute(expectedAreaName);
            Assert.AreEqual(expectedAreaName, systemUnderTest.Area);
            Assert.AreEqual($"{expectedAreaName}/{expectedRouteTemplate}", systemUnderTest.Template);
        }
    }
}