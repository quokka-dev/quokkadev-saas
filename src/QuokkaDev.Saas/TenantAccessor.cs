using Microsoft.AspNetCore.Http;
using QuokkaDev.Saas.Abstractions;

namespace QuokkaDev.Saas
{
    /// <summary>
    /// Give access to the current tenant
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class TenantAccessor<T, TKey> : ITenantAccessor<T, TKey> where T : Tenant<TKey>
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public TenantAccessor(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public T? Tenant => _httpContextAccessor?.HttpContext?.GetTenant<T, TKey>();
    }
}
