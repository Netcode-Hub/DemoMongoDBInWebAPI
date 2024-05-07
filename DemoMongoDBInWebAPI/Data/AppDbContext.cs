using DemoMongoDBInWebAPI.Entities;
using MongoDB.Driver;

namespace DemoMongoDBInWebAPI.Data
{
    public class AppDbContext
    {
        private readonly IMongoDatabase _database;

        public AppDbContext(IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("MongoDB");
            var databaseName = configuration.GetConnectionString("DatabaseName");

            var client = new MongoClient(connectionString);
            _database = client.GetDatabase(databaseName);
        }

        public IMongoCollection<WeatherForecast> WeatherForecast =>
            _database.GetCollection<WeatherForecast>("WeatherForecastList");
    }
}
