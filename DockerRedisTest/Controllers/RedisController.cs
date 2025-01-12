using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StackExchange.Redis;

namespace DockerRedisTest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RedisController : ControllerBase
    {
        private readonly IConnectionMultiplexer _redis;
        public RedisController(IConnectionMultiplexer redis)
        {
            _redis = redis;
        }

        [HttpGet]
        public async Task<IActionResult> GetValue(string key) 
        {
            var db = _redis.GetDatabase();
            var value = await db.StringGetAsync(key);
            return Ok(value.ToString());
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] KeyValuePair<string,string> data)
        {
            var db = _redis.GetDatabase();
            await db.StringSetAsync(data.Key,data.Value);
            return Ok("Key set Succesfully");
        }
    }
}
