using Catty.Api.Interfaces;
using Catty.Api.Options;
using Catty.Api.Providers;
using Catty.Core;
using Catty.Core.Extensions;
using Catty.Core.Options;
using MongoDB.Cats.Api.Models;
using MongoDB.Driver;

namespace Catty.Api.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddMongoDb(this IServiceCollection services, Action<MongoDbConfig> configure)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));

            services.Configure(configure);

            var config = new MongoDbConfig();

            configure.Invoke(config);

            var settings = MongoClientSettings.FromConnectionString(config.Url);
            settings.ServerApi = new ServerApi(ServerApiVersion.V1);

            IMongoDatabase catsDatabase = new MongoClient(settings).GetDatabase(config.Database);

            IMongoCollection<Cat> catsCollection = catsDatabase.GetCollection<Cat>(config.CatsCollection);

            services.AddSingleton(catsDatabase);

            services.AddSingleton(catsCollection);

            services.AddSingleton<ICatsCollectionRepository, CatsCollectionRepository>();

            return services;
        }

        public static IServiceCollection AddDefaultWebAppService(this IServiceCollection services)
        {
            services.AddCors();

            services.AddControllers();

            return services;
        }

        public static IServiceCollection AddExternalWebAppService(this IServiceCollection services)
        {
            services.AddEndpointsApiExplorer();

            services.AddAppSwaggerGen(CoreConstants.AuthenticationScheme.PrivateKey);

            return services;
        }

        public static IServiceCollection AddInternalWebAppService(this IServiceCollection services, IConfiguration config)
        {
            services.AddMongoDb(c => config.GetSection("MongoDbConfig").Bind(c));

            services.Configure<PrivateAuthConfig>(c => config.GetSection(nameof(PrivateAuthConfig)).Bind(c));

            services.AddPrivateKeyAuthentication(true);

            services.AddScoped<ICatsCollectionService, CatsCollectionService>();

            return services;
        }
    }
}