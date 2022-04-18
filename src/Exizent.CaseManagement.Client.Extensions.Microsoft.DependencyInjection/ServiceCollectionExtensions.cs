using Exizent.CaseManagement.Client;

// ReSharper disable once CheckNamespace
namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    /// Extensions to adding exizent case management client to the service collection
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Registers the exizent case management client
        /// </summary>
        /// <param name="services">Service collection</param>
        /// <param name="configure">Settings configuration for the case management client</param>     
        /// <returns>Service collection</returns>
        public static IServiceCollection AddExizentCaseManagementClient(this IServiceCollection services,
            Func<IServiceProvider, ExizentCaseManagementClientSettings> configure)
        {
            services.AddSingleton(configure);

            return AddExizentCaseManagementClient(services);
        }
        
        /// <summary>
        /// Registers the exizent case management client
        /// </summary>
        /// <param name="services">Service collection</param>
        /// <param name="configure">Settings configuration for the case management client</param>     
        /// <returns>Service collection</returns>
        public static IServiceCollection AddExizentCaseManagementClient(this IServiceCollection services,
            Func<ExizentCaseManagementClientSettings> configure)
        {
            return services.AddExizentCaseManagementClient(_ => configure());
        }
        
        /// <summary>
        /// Registers the exizent case management client
        /// </summary>
        /// <param name="services">Service collection</param>
        /// <param name="settings">Settings configuration for the case management client</param>     
        /// <returns>Service collection</returns>
        public static IServiceCollection AddExizentCaseManagementClient(this IServiceCollection services,
            ExizentCaseManagementClientSettings settings)
        {
            return services.AddExizentCaseManagementClient(() => settings);
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
            Action<ExizentCaseManagementClientSettings>? configure = null)
        {
            return services.AddExizentCaseManagementClient(clientId, clientSecret, (_, settings) => configure?.Invoke(settings));
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
            return services.AddExizentCaseManagementClient((provider) =>
            {
                var settings = new ExizentCaseManagementClientSettings(clientId, clientSecret);
                configure?.Invoke(provider, settings);
                return settings;
            });
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