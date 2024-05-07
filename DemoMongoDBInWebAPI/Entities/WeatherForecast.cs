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
