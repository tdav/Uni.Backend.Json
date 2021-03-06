using App.Database;
using Arch.EntityFrameworkCore.UnitOfWork;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace App.Extensions
{

    public class BaseController<T> : ControllerBase where T : class, IBaseModel
    {
        private readonly IUnitOfWork uow;
        private readonly IMemoryCache cache;

        public BaseController(IUnitOfWork unitOfWork, IMemoryCache _cache)
        {
            uow = unitOfWork;
            cache = _cache;
        }

        [HttpGet()]
        public async Task<ActionResult<IList<T>>> Get()
        {
            if (cache.TryGetValue(typeof(T).FullName, out T value))
            {
                return Ok(value);
            }
            else
            {
                var _storage = uow.GetRepository<T>();
                var res = await _storage.GetAllAsync(x => x.Status == 1);

                var cacheEntryOptions = new MemoryCacheEntryOptions();
                cacheEntryOptions.SetSlidingExpiration(TimeSpan.FromMinutes(5));

                cache.Set(typeof(T).FullName, res, cacheEntryOptions);
                return Ok(res);
            }
        }

        [HttpGet("GetAll")]
        public async Task<ActionResult<IList<T>>> GetAll()
        {
            if (cache.TryGetValue(typeof(T).FullName, out T value))
            {
                return Ok(value);
            }
            else
            {
                var _storage = uow.GetRepository<T>();
                var res = await _storage.GetAllAsync();

                var cacheEntryOptions = new MemoryCacheEntryOptions();
                cacheEntryOptions.SetSlidingExpiration(TimeSpan.FromMinutes(5));

                cache.Set(typeof(T).FullName, res, cacheEntryOptions);
                return Ok(res);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<T>> Get(int id)
        {
            if (cache.TryGetValue(typeof(T).FullName + id.ToString(), out T value))
            {
                return Ok(value);
            }
            else
            {
                var _storage = uow.GetRepository<T>();
                var res = await _storage.FindAsync(id);

                if (res != null)
                {
                    var cacheEntryOptions = new MemoryCacheEntryOptions();
                    cacheEntryOptions.SetSlidingExpiration(TimeSpan.FromMinutes(3));
                    cache.Set(typeof(T).FullName + id.ToString(), res, cacheEntryOptions);
                    return Ok(res);
                }
                else
                {
                    return NotFound();
                }
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] T value)
        {
            var _storage = uow.GetRepository<T>();
            await _storage.InsertAsync(value);
            await uow.SaveChangesAsync();
            return Ok(value);
        }

        [HttpPut]
        public async Task<IActionResult> Put([FromBody] T value)
        {
            var _storage = uow.GetRepository<T>();
            _storage.Update(value);
            await uow.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var _storage = uow.GetRepository<T>();
            _storage.Delete(id);
            await uow.SaveChangesAsync();
            return Ok();
        }
    }
}


