using FluentAssertions;
using Moq;
using QuokkaDev.Saas.Abstractions;
using System.Threading.Tasks;
using Xunit;

namespace QuokkaDev.Saas.Tests
{
    public class TenantAccessServiceUnitTest
    {
        public TenantAccessServiceUnitTest()
        {
        }

        [Fact(DisplayName = "AccessService should work as expected")]
        public async Task AccessService_Should_Work_As_Expected()
        {
            // Arrange
            var strategyMock = new Mock<ITenantResolutionStrategy>();
            strategyMock.Setup(m => m.GetTenantIdentifier()).Returns("my-tenant-identifier");
            strategyMock.Setup(m => m.GetTenantIdentifierAsync()).ReturnsAsync("my-tenant-identifier");

            var storeMock = new Mock<ITenantStore<Tenant<int>, int>>();
            storeMock.Setup(m => m.GetTenant("my-tenant-identifier")).Returns(new Tenant<int>(1, "my-tenant-identifier"));
            storeMock.Setup(m => m.GetTenantAsync("my-tenant-identifier")).ReturnsAsync(new Tenant<int>(1, "my-tenant-identifier"));

            var service = new TenantAccessService<Tenant<int>, int>(strategyMock.Object, storeMock.Object);
            // Act
            var tenant1 = service.GetTenant();
            var tenant2 = await service.GetTenantAsync();

            // Assert
            tenant1.Should().NotBeNull();
            tenant1.Identifier.Should().Be("my-tenant-identifier");

            tenant2.Should().NotBeNull();
            tenant2.Identifier.Should().Be("my-tenant-identifier");

            strategyMock.Verify(m => m.GetTenantIdentifier(), Times.Once);
            strategyMock.Verify(m => m.GetTenantIdentifierAsync(), Times.Once);

            storeMock.Verify(m => m.GetTenant("my-tenant-identifier"), Times.Once);
            storeMock.Verify(m => m.GetTenantAsync("my-tenant-identifier"), Times.Once);
        }
    }
}
