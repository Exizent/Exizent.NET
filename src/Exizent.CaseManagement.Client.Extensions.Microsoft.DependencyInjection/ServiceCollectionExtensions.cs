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
        /// <param name="baseUri">The Base Uri of the API</param>        
        /// <param name="clientId">The client id</param>        
        /// <param name="clientSecret">The client secret</param>     
        /// <returns>Service collection</returns>
        public static IServiceCollection AddExizentCaseManagementClient(this IServiceCollection services,
            Uri baseUri,
            Uri baseAuthorizationUri,
            string clientId,
            string clientSecret,
            string scopes)
        {
            services.AddSingleton<IExizentAuthorizationTokenCache, InMemoryExizentAuthorizationTokenCache>();
            services.AddTransient<IExizentAuthorizationTokenStore, ExizentAuthorizationTokenStore>(provider =>
                new ExizentAuthorizationTokenStore(
                    provider.GetService<IExizentAuthorizationTokenCache>(),
                    provider.GetService<IExizentAuthorizationClient>(),
                    clientId, clientSecret, scopes));
            
            services.AddHttpClient<IExizentAuthorizationClient, ExizentAuthorizationClient>(cfg => cfg.BaseAddress = baseAuthorizationUri);

            services.AddTransient<CaseManagementAuthorizationHandler>();

            services.AddHttpClient<ICaseManagementApiClient, CaseManagementApiClient>(cfg => cfg.BaseAddress = baseUri)
                .AddHttpMessageHandler<CaseManagementAuthorizationHandler>();

            return services;
        }

        /// <summary>
        /// Registers the exizent case management client
        /// </summary>
        /// <param name="services">Service collection</param>
        /// <param name="clientId">The client id</param>        
        /// <param name="clientSecret">The client secret</param>        
        /// <returns>Service collection</returns>
        public static IServiceCollection AddExizentCaseManagementClient(this IServiceCollection services,
            string clientId, string clientSecret)
        {
            return services.AddExizentCaseManagementClient(ExizentUris.CaseManagementProduction, ExizentUris.AuthorizationProduction,
                clientId,
                clientSecret,
                ExizentScopes.All);
        }
    }
}