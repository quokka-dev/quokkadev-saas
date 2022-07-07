using Microsoft.Extensions.DependencyInjection;
using QuokkaDev.Saas.Abstractions;
using QuokkaDev.Saas.DependencyInjection;

namespace QuokkaDev.Saas
{
    public static class TenantBuilderExtensions
    {
        public static TenantBuilder<TTenant, TKey> WithDefaultService<TTenant, TKey>(this TenantBuilder<TTenant, TKey> builder, ServiceLifetime serviceLifetime = ServiceLifetime.Scoped) where TTenant : Tenant<TKey>
        {
            return builder.WithService<TenantAccessService<TTenant, TKey>>(serviceLifetime);
        }

        public static TenantBuilder<TTenant, TKey> WithDefaultAccessor<TTenant, TKey>(this TenantBuilder<TTenant, TKey> builder, ServiceLifetime serviceLifetime = ServiceLifetime.Scoped) where TTenant : Tenant<TKey>
        {
            return builder.WithAccessor<TenantAccessor<TTenant, TKey>>(serviceLifetime);
        }
    }
}
