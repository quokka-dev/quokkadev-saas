using Microsoft.AspNetCore.Http;
using QuokkaDev.Saas.Abstractions;

namespace QuokkaDev.Saas
{
    /// <summary>
    /// Extensions to HttpContext to make multi-tenancy easier to use
    /// </summary>
    public static class HttpContextExtensions
    {
        /// <summary>
        /// Returns the current tenant
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="context"></param>
        /// <returns></returns>
        public static T? GetTenant<T, TKey>(this HttpContext context) where T : Tenant<TKey>
        {
            if (!context.Items.ContainsKey(Constants.HTTP_CONTEXT_TENANT_KEY))
            {
                return null;
            }

            return context.Items[Constants.HTTP_CONTEXT_TENANT_KEY] as T;
        }
    }
}
