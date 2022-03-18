using App.Database;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using System;
using System.Threading.Tasks;

namespace App.Services
{
    public interface IDataService
    {
        Task<int> AddAsync(string name, string dataModel);
        Task UpdateAsync(int id, string name, string dataModel);
        Task RemoveAsync(int id, string name);
        Task<JArray> GetAllAsync(int id, string name);
        Task<JObject> GetByIdAsync(int id, string name);
        Task<JArray> SearchAsync(int id);
    }


    public class DataService : IDataService
    {
        private MyDbContext db;

        public DataService(MyDbContext db) 
        {
            this.db = db;
        }

        public async Task<int> AddAsync(string name, string dataModel)
        {
            var data = new tbData()
            {
                Name = name,
                DataModel = dataModel,
                Status = 1,
                //CreateUser = accessor.GetId(),
                CreateDate = DateTime.Now
            };

            await db.tbDatas.AddAsync(data);
            await db.SaveChangesAsync();

            return data.Id;
        }

        public async Task UpdateAsync(int id, string name, string dataModel)
        {
            var data = await db.tbDatas.FirstOrDefaultAsync(x => x.Id == id && x.Name == name);
            if (data == null) return;

            data.DataModel = dataModel;
            data.UpdateDate = DateTime.Now;
            //data.UpdateUser = accessor.GetId();

            await db.SaveChangesAsync();
        }

        public async Task RemoveAsync(int id, string name)
        {
            var data = await db.tbDatas.FirstOrDefaultAsync(x => x.Id == id && x.Name == name);
            if (data == null) return;

            db.Remove(data);
            await db.SaveChangesAsync();
        }

        public async Task<JArray> GetAllAsync(int id, string name)
        {
            var data = await db.tbDatas
                               .AsNoTracking()
                               .AllAsync(x => x.Id == id && x.Name == name);
            if (data == null) return null;

            var res = JObject.Parse(data.DataModel);
            if (!res.ContainsKey("id")) res.Add("id", id);

            return res;
        }


        public async Task<JObject> GetByIdAsync(int id, string name)
        {
            var data = await db.tbDatas.FirstOrDefaultAsync(x => x.Id == id && x.Name == name);
            if (data == null) return null;

            var res = JObject.Parse(data.DataModel);
            if (!res.ContainsKey("id")) res.Add("id", id);

            return res;
        }

        public async Task<JArray> SearchAsync(int id)
        {
            var data = await db.tbDatas.FirstOrDefaultAsync(x => x.Id == id);
            if (data == null) return null;

            return JArray.Parse(data.DataModel);
        }
    }
}


//https://www.npgsql.org/doc/types/jsonnet.html
//https://github.com/npgsql/npgsql/blob/main/test/Npgsql.PluginTests/JsonNetTests.cs

//https://scalegrid.io/blog/using-jsonb-in-postgresql-how-to-effectively-store-index-json-data-in-postgresql/