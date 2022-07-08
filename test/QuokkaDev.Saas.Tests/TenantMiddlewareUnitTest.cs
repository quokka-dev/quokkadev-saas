using HttpContextMoq;
using Microsoft.AspNetCore.Http;
using Moq;
using QuokkaDev.Saas.Abstractions;
using System;
using System.Threading.Tasks;
using Xunit;

namespace QuokkaDev.Saas.Tests
{
    public class TenantMiddlewareUnitTest
    {
        public TenantMiddlewareUnitTest()
        {
        }

        [Fact(DisplayName = "Tenant should be added to context")]
        public async Task Tenant_Should_Be_Added_To_Context()
        {
            // Arrange
            var tenant = new Tenant<int>(1, "my-tenant-identifier");

            (var middleware, var httpContextMock) = SetupMocks(tenant, false);

            // Act
            await middleware.Invoke(httpContextMock);

            // Assert
            httpContextMock.ItemsMock.Mock.Verify(m => m.Add(Constants.HTTP_CONTEXT_TENANT_KEY, tenant), Times.Once);
        }

        [Fact(DisplayName = "Item key already present should skip middleware")]
        public async Task Item_Key_Already_Present_Should_Skip_Middleware()
        {
            // Arrange
            var tenant = new Tenant<int>(1, "my-tenant-identifier");

            (var middleware, var httpContextMock) = SetupMocks(tenant, true);

            // Act
            await middleware.Invoke(httpContextMock);

            // Assert
            httpContextMock.ItemsMock.Mock.Verify(m => m.Add(Constants.HTTP_CONTEXT_TENANT_KEY, tenant), Times.Never);
            httpContextMock.RequestServicesMock.Mock.Verify(m => m.GetService(It.IsAny<Type>()), Times.Never);
        }

        [Fact(DisplayName = "TenantAccessService not registered should skip middleware")]
        public async Task TenantAccessService_Not_Registered_Should_Skip_Middleware()
        {
            // Arrange
            var tenant = new Tenant<int>(1, "my-tenant-identifier");

            (var middleware, var httpContextMock) = SetupMocks(tenant, false, false);

            // Act
            await middleware.Invoke(httpContextMock);

            // Assert
            httpContextMock.ItemsMock.Mock.Verify(m => m.Add(Constants.HTTP_CONTEXT_TENANT_KEY, tenant), Times.Never);
            httpContextMock.RequestServicesMock.Mock.Verify(m => m.GetService(It.IsAny<Type>()), Times.Once);
        }

        private static (TenantMiddleware<Tenant<int>, int> Middleware, HttpContextMock Context) SetupMocks(Tenant<int> tenant, bool keyPresent, bool registerAccessService = true)
        {
            var delegateMock = new Mock<RequestDelegate>();
            TenantMiddleware<Tenant<int>, int> middleware = new(delegateMock.Object);

            var accessServiceMock = new Mock<ITenantAccessService<Tenant<int>, int>>();
            accessServiceMock.Setup(m => m.GetTenantAsync()).ReturnsAsync(tenant);

            var httpContextMock = new HttpContextMock();
            httpContextMock.ItemsMock.Mock.Setup(m => m.ContainsKey(Constants.HTTP_CONTEXT_TENANT_KEY)).Returns(keyPresent);

            if (registerAccessService)
            {
                httpContextMock.RequestServicesMock.Mock.Setup(m => m.GetService(typeof(ITenantAccessService<Tenant<int>, int>))).Returns(accessServiceMock.Object);
            }

            return (middleware, httpContextMock);
        }
    }
}
