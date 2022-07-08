using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

namespace QuokkaDev.Saas
{
    public static class Constants
    {
        public const string HTTP_CONTEXT_TENANT_KEY = "HttpContextTenantKey";

        public const IOptions<string> fake1 = null;

        public const HttpContextAccessor fake2 = null;
    }
}
