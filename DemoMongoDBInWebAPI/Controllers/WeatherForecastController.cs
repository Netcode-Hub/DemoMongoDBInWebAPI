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
