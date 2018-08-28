using DataValidation.Interfaces;
using DataValidation.Mvc;
using Microsoft.AspNetCore.Mvc.Internal;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using NUnit.Framework;
using NUnit.Framework.Internal;

namespace DataValidation.Tests
{
    [TestFixture]
    public class AreaConfigurationTests
    {
        [Test]
        public void RegisterAreas_calls_RouterBuilder_Routes_Add()
        {
            SetupDependencies();
            _routerBuilderMock.Setup(routeBuilder => routeBuilder.Routes.Add(It.IsAny<Route>())).Verifiable();
            _systemUnderTest = AreaConfiguration.DefaultAreaConfiguration;
            _systemUnderTest = new AreaConfiguration
            {
                DefaultAreaRouteName = "Test",
                DefaultAreaRouteTemplate = "Test2"
            };
            _systemUnderTest.RegisterAreas(_routerBuilderMock.Object);
            _routerBuilderMock.Verify(routeBuilder => routeBuilder.Routes.Add(It.IsAny<Route>()));
        }

        private void SetupDependencies()
        {
            var router = new Mock<IRouter>();
            var inlineConstraintResolver = new Mock<IInlineConstraintResolver>();
            var serviceProvider = new Mock<System.IServiceProvider>();
            _routerBuilderMock = new Mock<IRouteBuilder>();
            _routerBuilderMock.Setup(routeBuilder => routeBuilder.DefaultHandler).Returns(router.Object);
            _routerBuilderMock.Setup(routeBuilder => routeBuilder.ServiceProvider).Returns(serviceProvider.Object);
            serviceProvider.Setup(sProvider => sProvider.GetService(typeof(IInlineConstraintResolver))).Returns(inlineConstraintResolver.Object);
        }

        private Mock<IRouteBuilder> _routerBuilderMock = new Mock<IRouteBuilder>();
        private IAreaConfiguration _systemUnderTest;
    }
}