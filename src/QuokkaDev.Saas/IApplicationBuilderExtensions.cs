using Microsoft.AspNetCore.Builder;
using QuokkaDev.Saas.Abstractions;

namespace QuokkaDev.Saas
{
    /// <summary>
    /// Nice method to register our middleware
    /// </summary>
    public static class IApplicationBuilderExtensions
    {
        /// <summary>
        /// Use the Tenant Middleware to process the request
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="builder"></param>
        /// <returns></returns>
        public static IApplicationBuilder UseMultiTenancy<T, TKey>(this IApplicationBuilder builder) where T : Tenant<TKey>
            => builder.UseMiddleware<TenantMiddleware<T, TKey>>();

        /// <summary>
        /// Use the Tenant Middleware to process the request
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="builder"></param>
        /// <returns></returns>
        public static IApplicationBuilder UseMultiTenancyWithDefaultTenantType<TKey>(this IApplicationBuilder builder)
            => builder.UseMiddleware<TenantMiddleware<Tenant<TKey>, TKey>>();

        /// <summary>
        /// Use the Tenant Middleware to process the request
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="builder"></param>
        /// <returns></returns>
        public static IApplicationBuilder UseMultiTenancyWithDefaultTenantType(this IApplicationBuilder builder)
            => builder.UseMiddleware<TenantMiddleware<Tenant<int>, int>>();
    }
}
