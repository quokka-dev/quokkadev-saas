using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using QuokkaDev.Saas.Abstractions;
using QuokkaDev.Saas.DependencyInjection;
using System.Linq;
using Xunit;

namespace QuokkaDev.Saas.Tests
{
    public class TenantBuilderExtensionsUnitTest
    {
        public TenantBuilderExtensionsUnitTest()
        {
        }

        [Fact(DisplayName = "Default AccessService should be registered")]
        public void DefaultAccessService_Should_Be_Registered()
        {
            // Arrange
            IServiceCollection services = new ServiceCollection();
            TenantBuilder<Tenant<int>, int> tenantBuilder = new(services);

            // Act
            tenantBuilder.WithDefaultService();
            var service = services.FirstOrDefault(sd => sd.ServiceType == typeof(ITenantAccessService<Tenant<int>, int>));
            // Assert
            service.Should().NotBeNull();
            service?.ImplementationType.Should().NotBeNull();
            service?.ImplementationType.Should().Be(typeof(TenantAccessService<Tenant<int>, int>));
            service?.Lifetime.Should().Be(ServiceLifetime.Scoped);
            service?.ImplementationInstance.Should().BeNull();
        }

        [Fact(DisplayName = "Default Accessor should be registered")]
        public void DefaultAccessor_Should_Be_Registered()
        {
            // Arrange
            IServiceCollection services = new ServiceCollection();
            TenantBuilder<Tenant<int>, int> tenantBuilder = new(services);

            // Act
            tenantBuilder.WithDefaultAccessor();
            var accessor = services.FirstOrDefault(sd => sd.ServiceType == typeof(ITenantAccessor<Tenant<int>, int>));
            // Assert
            accessor.Should().NotBeNull();
            accessor?.ImplementationType.Should().NotBeNull();
            accessor?.ImplementationType.Should().Be(typeof(TenantAccessor<Tenant<int>, int>));
            accessor?.Lifetime.Should().Be(ServiceLifetime.Scoped);
            accessor?.ImplementationInstance.Should().BeNull();
        }
    }
}
