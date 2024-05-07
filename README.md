# Mastering MongoDB: Seamless Integration in .NET 8 Web API for Effortless CRUD Operations

# Create .NET 8 Web API project
![image](https://github.com/Netcode-Hub/DemoMongoDBInWebAPI/assets/110794348/79822156-df84-4834-a85b-1b1afb37cca1)

# Install MongoDB Driver Nugget package
    <PackageReference Include="MongoDB.Driver" Version="x.x.x" />

# Create Example Model
    using MongoDB.Bson.Serialization.Attributes;
     namespace DemoMongoDBInWebAPI.Entities
     {
         public class WeatherForecast
         {
            public Guid Id { get; set; }

            [BsonElement("Date")]
            public DateTime Date { get; set; }

            [BsonElement("Temperature_C")]
            public int TemperatureC { get; set; }

            [BsonElement("Temperature_F")]
            public int TemperatureF { get; set; }

            [BsonElement("Summary")]
            public string? Summary { get; set; }
        }
    }

# Create MongoDB section in Config file
    "ConnectionStrings": {
      "MongoDB": "mongodb://localhost:27017",
      "DatabaseName": "WeatherDB"
    }
  
# Create Context Class
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

# Register the context class in Program file
    builder.Services.AddSingleton<AppDbContext>();

# Use the Context in Repo / Controller
    using DemoMongoDBInWebAPI.Data;
    using DemoMongoDBInWebAPI.Entities;
    using Microsoft.AspNetCore.Mvc;
    using MongoDB.Driver;

    namespace DemoMongoDBInWebAPI.Controllers
    {
        [ApiController]
        [Route("[controller]")]
        public class WeatherForecastController(AppDbContext context) : ControllerBase
        {
            [HttpGet("list")]
            public async Task<IActionResult> GetList()
            {
                var myEntities = await context.WeatherForecast.Find(Builders<WeatherForecast>.Filter.Empty).ToListAsync();
                return Ok(myEntities);
            }

            [HttpGet("single")]
            public async Task<IActionResult> GetSingleG(Guid id)
            {
                var entities = await context.WeatherForecast.Find(Builders<WeatherForecast>.Filter.Empty).ToListAsync();
                var myEntity = entities.FirstOrDefault(w => w.Id == id);
                return Ok(myEntity);
            }

            [HttpPost]
            public async Task<IActionResult> Add(WeatherForecast weather)
            {
                weather.Id = Guid.NewGuid();
                await context.WeatherForecast.InsertOneAsync(weather);
                return CreatedAtAction(nameof(Add), weather);
            }

            [HttpPut]
            public async Task<IActionResult> Update(WeatherForecast weather)
            {
                await context.WeatherForecast.ReplaceOneAsync(w => w.Id == weather.Id, weather);
                return Ok(weather);
            }

            [HttpDelete]
            public async Task<IActionResult> Delete(Guid id)
            {
                await context.WeatherForecast.DeleteOneAsync(w => w.Id == id);
                return NoContent();
            }
        }
    }

# UI - Endpoints
![image](https://github.com/Netcode-Hub/DemoMongoDBInWebAPI/assets/110794348/9d5365a7-a4f6-48fe-a6ad-313d39b67d32)

# MongoDB compass Server
![Screenshot 2024-05-07 041546](https://github.com/Netcode-Hub/DemoMongoDBInWebAPI/assets/110794348/df95a312-2fe3-48cb-afea-2b8bf5fe249b)


# Here's a follow-up section to encourage engagement and support for Netcode-Hub:
🌟 Get in touch with Netcode-Hub! 📫
1. GitHub: [Explore Repositories](https://github.com/Netcode-Hub/Netcode-Hub) 🌐
2. Twitter: [Stay Updated](https://twitter.com/NetcodeHub) 🐦
3. Facebook: [Connect Here](https://web.facebook.com/NetcodeHub) 📘
4. LinkedIn: [Professional Network](https://www.linkedin.com/in/netcode-hub-90b188258/) 🔗
5. Email: Email: [business.netcodehub@gmail.com](mailto:business.netcodehub@gmail.com) 📧
   
# ☕️ If you've found value in Netcode-Hub's work, consider supporting the channel with a coffee!
1. Buy Me a Coffee: [Support Netcode-Hub](https://www.buymeacoffee.com/NetcodeHub) ☕️
2. Patreon: [Support on Patreon](https://patreon.com/user?u=113091185&utm_medium=unknown&utm_source=join_link&utm_campaign=creatorshare_creator&utm_content=copyLink) 🌟
