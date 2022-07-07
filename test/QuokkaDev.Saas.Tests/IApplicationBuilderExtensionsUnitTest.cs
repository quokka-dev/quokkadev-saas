using FluentAssertions;
using Microsoft.AspNetCore.Builder;
using Moq;
using QuokkaDev.Saas.Abstractions;
using System;
using System.Linq;
using Xunit;

namespace QuokkaDev.Saas.Tests
{
    public class IApplicationBuilderExtensionsUnitTest
    {
        public IApplicationBuilderExtensionsUnitTest()
        {
        }

        [Fact(DisplayName = "ExtensionsMethods should register middleware")]
        public void ExtensionsMethods_Should_Register_Middleware()
        {
            // Arrange
            var mock = new Mock<IApplicationBuilder>();

            // Act
#pragma warning disable RCS1196 // Call extension method as instance method.
            IApplicationBuilderExtensions.UseMultiTenancy<FakeTenant, Guid>(mock.Object);
            IApplicationBuilderExtensions.UseMultiTenancyWithDefaultTenantType<Guid>(mock.Object);
            IApplicationBuilderExtensions.UseMultiTenancyWithDefaultTenantType(mock.Object);
#pragma warning restore RCS1196 // Call extension method as instance method.

            // Assert
            mock.Invocations.Count(invocation => invocation.Method.Name.Contains("Use"))
               .Should().Be(3);
        }
    }

    public class FakeTenant : Tenant<Guid>
    {
        public FakeTenant(Guid id, string identifier) : base(id, identifier)
        {
        }
    }
}
