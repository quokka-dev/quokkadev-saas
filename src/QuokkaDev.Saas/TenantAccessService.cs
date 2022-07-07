using QuokkaDev.Saas.Abstractions;

namespace QuokkaDev.Saas
{
    /// <summary>
    /// Tenant access service
    /// </summary>
    /// <typeparam name="TTenant">Type of tenant</typeparam>
    /// <typeparam name="TKey">Type of tenant key</typeparam>
    public class TenantAccessService<TTenant, TKey> : ITenantAccessService<TTenant, TKey> where TTenant : Tenant<TKey>
    {
        private readonly ITenantResolutionStrategy _tenantResolutionStrategy;
        private readonly ITenantStore<TTenant, TKey> _tenantStore;

        public TenantAccessService(ITenantResolutionStrategy tenantResolutionStrategy, ITenantStore<TTenant, TKey> tenantStore)
        {
            _tenantResolutionStrategy = tenantResolutionStrategy;
            _tenantStore = tenantStore;
        }

        /// <summary>
        /// Get the current tenant
        /// </summary>
        /// <returns>The current tenant</returns>
        /// <exception cref="NotImplementedException"></exception>
        public TTenant GetTenant()
        {
            var tenantIdentifier = _tenantResolutionStrategy.GetTenantIdentifier();
            return _tenantStore.GetTenant(tenantIdentifier);
        }

        /// <summary>
        /// Get the current tenant
        /// </summary>
        /// <returns>The current tenant</returns>
        public async Task<TTenant> GetTenantAsync()
        {
            var tenantIdentifier = await _tenantResolutionStrategy.GetTenantIdentifierAsync();
            return await _tenantStore.GetTenantAsync(tenantIdentifier);
        }
    }
}
