using System;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;

namespace RedisCacheDemo.Controllers
{
    [Route("api/[controller]")]
    public class ValuesController : ControllerBase
    {
        private readonly IDistributedCache _cache;
        public ValuesController(IDistributedCache cache)
        {
            _cache = cache;
        }

        [HttpGet("Set")]
        public void Set()
        {
            var encodedUserName = Encoding.UTF8.GetBytes(MyUser.UserName);
            var options = new DistributedCacheEntryOptions()
                .SetSlidingExpiration(TimeSpan.FromSeconds(20));
            _cache.SetAsync(MyUser.Id, encodedUserName, options);
        }

        [HttpGet("Get")]
        public string Get()
        {
            var username = _cache.GetString(MyUser.Id);

            return username;
        }
    }
    public static class MyUser
    {
        public static string Id = "b28fa919-f25d-4328-a441-782b35f58884";
        public static string UserName = "user.name";
    }

}