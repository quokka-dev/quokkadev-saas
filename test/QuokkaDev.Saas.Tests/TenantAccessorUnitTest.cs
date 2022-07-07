using FluentAssertions;
using HttpContextMoq;
using Microsoft.AspNetCore.Http;
using Moq;
using QuokkaDev.Saas.Abstractions;
using Xunit;

namespace QuokkaDev.Saas.Tests
{
    public class TenantAccessorUnitTest
    {
        public TenantAccessorUnitTest()
        {
        }

        [Fact]
        public void TenantAccessor_Should_Return_Tenant()
        {
            // Arrange
            var httpContextMock = new HttpContextMock();
            httpContextMock.ItemsMock.Mock.Setup(m => m.ContainsKey(Constants.HTTP_CONTEXT_TENANT_KEY)).Returns(true);
            httpContextMock.ItemsMock.Mock.SetupGet(m => m[Constants.HTTP_CONTEXT_TENANT_KEY]).Returns(new Tenant<int>(1, "my-tenant-identifier"));

            var httpContextAccessorMock = new Mock<IHttpContextAccessor>();
            httpContextAccessorMock.SetupGet(m => m.HttpContext).Returns(httpContextMock);

            TenantAccessor<Tenant<int>, int> accessor = new(httpContextAccessorMock.Object);

            // Act
            var tenant = accessor.Tenant;

            // Assert
            tenant.Should().NotBeNull();
            tenant?.Identifier.Should().Be("my-tenant-identifier");
        }

        [Fact(DisplayName = "Empty HttpAccessor should return null tenant")]
        public void Empty_HttpAccessor_Should_Return_Null_Tenant()
        {
            // Arrange 
            TenantAccessor<Tenant<int>, int> accessor = new(null!);

            // Act
            var tenant = accessor.Tenant;

            // Assert
            tenant.Should().BeNull();
        }

        [Fact(DisplayName = "Empty HttpContext should return null tenant")]
        public void Empty_HttpContext_Should_Return_Null_Tenant()
        {
            // Arrange 
            var httpContextAccessorMock = new Mock<IHttpContextAccessor>();
            httpContextAccessorMock.SetupGet(m => m.HttpContext).Returns((HttpContext)null!);

            TenantAccessor<Tenant<int>, int> accessor = new(httpContextAccessorMock.Object);

            // Act
            var tenant = accessor.Tenant;

            // Assert
            tenant.Should().BeNull();
        }
    }
}
