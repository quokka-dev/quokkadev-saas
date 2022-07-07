using Microsoft.AspNetCore.Http;
using QuokkaDev.Saas.Abstractions;

namespace QuokkaDev.Saas
{
    /// <summary>
    /// Custom middleware for evaluate current Tenant
    /// </summary>
    /// <typeparam name="T">Type of tenant</typeparam>
    public class TenantMiddleware<T, TKey> where T : Tenant<TKey>
    {
        private readonly RequestDelegate next;

        public TenantMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task Invoke(HttpContext context)
        {

            if (!context.Items.ContainsKey(Constants.HTTP_CONTEXT_TENANT_KEY) &&
                 context.RequestServices.GetService(typeof(ITenantAccessService<T, TKey>)) is ITenantAccessService<T, TKey> tenantService)
            {
                context.Items.Add(Constants.HTTP_CONTEXT_TENANT_KEY, await tenantService.GetTenantAsync());
            }

            if (next != null)
            {
                await next(context);
            }
        }
    }
}
