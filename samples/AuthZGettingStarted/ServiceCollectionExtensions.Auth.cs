namespace Microsoft.Extensions.DependencyInjection;

using Keycloak.AuthServices.Authentication;
using Keycloak.AuthServices.Authorization;
using static AuthorizationConstants;

public static partial class ServiceCollectionExtensions
{
    public static IServiceCollection AddAuth(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddKeycloakAuthentication(configuration);

        services.AddAuthorization(options =>
        {
            options.AddPolicy(
                Policies.RequireAspNetCoreRole,
                builder => builder.RequireRole(Roles.AspNetCoreRole));

            options.AddPolicy(
                Policies.RequireRealmRole,
                builder => builder.RequireRealmRoles(Roles.RealmRole));

            options.AddPolicy(
                Policies.RequireRealmRole,
                builder => builder.RequireResourceRoles(Roles.ClientRole));

            options.AddPolicy(
                Policies.RequireToBeInKeycloakGroupAsReader,
                builder => builder.RequireProtectedResource("workspace", "workspace:read"));

        }).AddKeycloakAuthorization(configuration);

        return services;
    }
}

public static class AuthorizationConstants
{
    public static class Roles
    {
        public const string AspNetCoreRole = nameof(AspNetCoreRole);

        public const string RealmRole = nameof(RealmRole);

        public const string ClientRole = nameof(ClientRole);
    }

    public static class Policies
    {
        public const string RequireAspNetCoreRole = nameof(RequireAspNetCoreRole);

        public const string RequireRealmRole = nameof(RequireRealmRole);

        public const string RequireClientRole = nameof(RequireClientRole);

        public const string RequireToBeInKeycloakGroupAsReader = nameof(RequireToBeInKeycloakGroupAsReader);
    }
}
