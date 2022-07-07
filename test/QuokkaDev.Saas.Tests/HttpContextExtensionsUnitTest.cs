using FluentAssertions;
using HttpContextMoq;
using Moq;
using QuokkaDev.Saas.Abstractions;
using Xunit;

namespace QuokkaDev.Saas.Tests;

public class HttpContextExtensionsUnitTest
{
    [Fact(DisplayName = "Context without tenant key should return null")]
    public void Context_Without_Tenant_Key_Should_Return_Null()
    {
        // Arrange
        var httpContextMock = new HttpContextMock();
        httpContextMock.ItemsMock.Mock.Setup(m => m.ContainsKey(Constants.HTTP_CONTEXT_TENANT_KEY)).Returns(false);

        // Act
#pragma warning disable RCS1196 // Call extension method as instance method.
        var tenant = HttpContextExtensions.GetTenant<Tenant<int>, int>(httpContextMock);
#pragma warning restore RCS1196 // Call extension method as instance method.

        // Assert
        tenant.Should().BeNull();
        httpContextMock.ItemsMock.Mock.Verify(m => m.ContainsKey(Constants.HTTP_CONTEXT_TENANT_KEY), Times.Once);
    }

    [Fact(DisplayName = "Context with tenant key should return a Tenant")]
    public void Context_With_Tenant_Key_Should_Return_A_Tenant()
    {
        // Arrange
        var httpContextMock = new HttpContextMock();
        httpContextMock.ItemsMock.Mock.Setup(m => m.ContainsKey(Constants.HTTP_CONTEXT_TENANT_KEY)).Returns(true);
        httpContextMock.ItemsMock.Mock.SetupGet(m => m[Constants.HTTP_CONTEXT_TENANT_KEY]).Returns(new Tenant<int>(1, "my-tenant-identifier"));
        // Act
#pragma warning disable RCS1196 // Call extension method as instance method.
        var tenant = HttpContextExtensions.GetTenant<Tenant<int>, int>(httpContextMock);
#pragma warning restore RCS1196 // Call extension method as instance method.

        // Assert
        tenant.Should().NotBeNull();
        httpContextMock.ItemsMock.Mock.Verify(m => m.ContainsKey(Constants.HTTP_CONTEXT_TENANT_KEY), Times.Once);
        httpContextMock.ItemsMock.Mock.Verify(m => m[Constants.HTTP_CONTEXT_TENANT_KEY], Times.Once);
    }
}