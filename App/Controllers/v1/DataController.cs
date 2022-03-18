using App.Database;
using App.Extensions;
using App.Services;
using Arch.EntityFrameworkCore.UnitOfWork;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using System.Threading.Tasks;

namespace App.Controllers.v1
{
    //[Authorize]
    [ApiController]
    [ApiVersion("1.0")]
    [Route("[controller]")]

    public class DataController : ControllerBase
    {
        private readonly IUnitOfWork db;
        private readonly IMemoryCache cache;
        public DataController(IUnitOfWork db, IMemoryCache cache)
        {
            this.db = db;
            this.cache = cache;
        }

        [HttpPost("{name}")]
        public async Task<IActionResult> AddAsync([FromBody] object data, string name)
        {
            var all_str = data.ToString();
            var rpData = db.GetRepository<tbData>(true) as DataService;
            var res = await rpData.AddAsync(name, all_str);
            return Ok(res);
        }

        [HttpPut("{name}/{id}")]
        public async Task<IActionResult> UpdateAsync([FromBody] object data, string name, int id)
        {
            var all_str = data.ToString();
            var rpData = db.GetRepository<tbData>(true) as DataService;
            await rpData.UpdateAsync(id, name, all_str);
            return Ok(true);
        }

        [HttpDelete("{name}/{id}")]
        public async Task<IActionResult> RemoveAsync(string name, int id)
        {
            var rpData = db.GetRepository<tbData>(true) as DataService;
            await rpData.RemoveAsync(id, name);
            return Ok(true);
        }

        [HttpGet("{name}/{id}")]
        public async Task<IActionResult> GetByIdAsync(string name, int id)
        {
            var rpData = db.GetRepository<tbData>(true) as DataService;
            var res = await rpData.GetByIdAsync(id, name);
            return Ok(res);
        }

        [HttpPost("{name}/search")]
        public async Task<IActionResult> SearchAsync(int id)
        {
            var rpData = db.GetRepository<tbData>(true) as DataService;
            var res = await rpData.SearchAsync(id);
            return Ok(res);
        }
    }
}
