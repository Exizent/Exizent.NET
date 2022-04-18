using Exizent.CaseManagement.Client;

// ReSharper disable once CheckNamespace
namespace Microsoft.Extensions.DependencyInjection
{
    public class ExizentCaseManagementClientSettings
    {
        public ExizentCaseManagementClientSettings(string clientId, string clientSecret)
        {
            ClientId = clientId;
            ClientSecret = clientSecret;
        }

        /// <summary>
        /// The client id for the application.
        /// </summary>
        public string ClientId { get; }
        /// <summary>
        /// The client secret for the application.
        /// </summary>
        public string ClientSecret { get; }
        
        /// <summary>
        /// The scope for the application.
        /// </summary>
        public string Scope { get; set; } = ExizentScopes.All;
        
        /// <summary>
        /// The base url for the api.
        /// </summary>
        public Uri BaseUri { get; set; } = ExizentUris.CaseManagementProduction;
        
        /// <summary>
        /// The base authorization url for the api.
        /// </summary>
        public Uri BaseAuthorizationUri { get; set; } = ExizentUris.AuthorizationProduction;
    }

    /// <summary>
    /// Extensions to adding exizent case management client to the service collection
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Registers the exizent case management client
        /// </summary>
        /// <param name="services">Service collection</param>
        /// <param name="clientId">The client id</param>        
        /// <param name="clientSecret">The client secret</param>     
        /// <param name="configure">Extra settings configuration for the case management client</param>     
        /// <returns>Service collection</returns>
        public static IServiceCollection AddExizentCaseManagementClient(this IServiceCollection services,
            string clientId,
            string clientSecret,
            Action<ExizentCaseManagementClientSettings>? configure = null)
        {
            services.AddSingleton(provider =>
            {
                var settings = new ExizentCaseManagementClientSettings(clientId, clientSecret);
                configure?.Invoke(settings);
                return settings;
            });

            return AddExizentCaseManagementClient(services);
        }
        
        /// <summary>
        /// Registers the exizent case management client
        /// </summary>
        /// <param name="services">Service collection</param>
        /// <param name="clientId">The client id</param>        
        /// <param name="clientSecret">The client secret</param>     
        /// <param name="configure">Extra settings configuration for the case management client</param>     
        /// <returns>Service collection</returns>
        public static IServiceCollection AddExizentCaseManagementClient(this IServiceCollection services,
            string clientId,
            string clientSecret,
            Action<IServiceProvider, ExizentCaseManagementClientSettings>? configure = null)
        {
            services.AddSingleton(provider =>
            {
                var settings = new ExizentCaseManagementClientSettings(clientId, clientSecret);
                configure?.Invoke(provider, settings);
                return settings;
            });

            return AddExizentCaseManagementClient(services);
        }

        private static IServiceCollection AddExizentCaseManagementClient(this IServiceCollection services)
        {
            services.AddSingleton<IAuthorizationTokenCache, InMemoryAuthorizationTokenCache>();
            services.AddTransient<IAuthorizationTokenStore, AuthorizationTokenStore>(provider =>
            {
                var settings = provider.GetService<ExizentCaseManagementClientSettings>();
                return new AuthorizationTokenStore(
                    provider.GetService<IAuthorizationTokenCache>(),
                    provider.GetService<IExizentAuthorizationClient>(),
                    settings.ClientId, settings.ClientSecret, settings.Scope);
            });

            services.AddHttpClient<IExizentAuthorizationClient, ExizentAuthorizationClient>((provider, cfg) =>
                cfg.BaseAddress = provider.GetRequiredService<ExizentCaseManagementClientSettings>().BaseAuthorizationUri);

            services.AddTransient<CaseManagementAuthorizationHandler>();

            services.AddHttpClient<ICaseManagementApiClient, CaseManagementApiClient>((provider, cfg) =>
                    cfg.BaseAddress = provider.GetRequiredService<ExizentCaseManagementClientSettings>().BaseAuthorizationUri)
                .AddHttpMessageHandler<CaseManagementAuthorizationHandler>();

            return services;
        }
    }
}