using App.Database;
using App.Models;
using AsbtCore.UtilsV2;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace App.Services
{
    public interface IUserService
    {
        Task<viUser> CreateUserAsync(viUserRegister value);
        Task<viUser> AuthenticateAsync(viAuthenticateModel model);
        Task<tbUser> GetByIdAsync(int id);
        Task<List<tbUser>> GetAllUsersAsync();
    }


    public class UserService : IUserService
    {
        private MyDbContext db;
        private IConfiguration config;

        public UserService(MyDbContext _db, IConfiguration _conf) 
        {
            db = _db;
            config = _conf;
        }

        public async Task<viUser> CreateUserAsync(viUserRegister value)
        {
            tbUser res = await db.tbUsers.AsNoTracking()
                                         .FirstOrDefaultAsync(x => x.Login == value.Login);

            if (res == null)
            {
                res = new tbUser
                {
                    SurName = value.SurName,
                    Name = value.Name,
                    Patronymic = value.Patronymic,  
                    Login = value.Login,
                    Password = CHash.EncryptMD5(value.Password),
                    Phone = value.Phone,
                    CreateDate = DateTime.UtcNow,
                    
                    //TODO get cur user
                    CreateUser = 1,
                    Status = 1,
                    RoleId = 1
                };

                await db.tbUsers.AddAsync(res);
                await db.SaveChangesAsync();
            }
            else
            {
                if (res.Password != value.Password)
                    return new viUser() { Status = 0, StatusMessage = "User already exists" };
            }

            return GetToken(res);
        }

        public async Task<viUser> AuthenticateAsync(viAuthenticateModel model)
        {
            var hash = CHash.EncryptMD5(model.Password);
            var res = await db.tbUsers
                              .AsNoTracking()
                              .Where(x => x.Login == model.Login && x.Password == hash)
                              .FirstOrDefaultAsync();

            if (res == null) return null;

            return GetToken(res);
        }

        public async Task<tbUser> GetByIdAsync(int id)
        {
            var res = await db.tbUsers.AsNoTracking().Where(x => x.Id == id).FirstOrDefaultAsync();
            res.Password = null;
            return res;
        }

        public async Task<List<tbUser>> GetAllUsersAsync()
        {
            var ls = await db.tbUsers.AsNoTracking().ToListAsync();
            foreach (var it in ls)
            {
                it.Password = null;
            }

            return ls;
        }

        private viUser GetToken(tbUser res)
        {
            var SecretStr = config["SystemParams:PrivateKeyString"];
            var key = Encoding.ASCII.GetBytes(SecretStr);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                           {
                               new Claim(ClaimTypes.Sid, res.Id.ToString()),
                               new Claim(ClaimTypes.Name, $"{res.SurName} {res.Name} {res.Patronymic}"),
                               new Claim(ClaimTypes.MobilePhone, res.Phone),
                               new Claim(ClaimTypes.Role, res.RoleId.ToString()),
                               new Claim(ClaimTypes.NameIdentifier, res.Login.ToString()),
                           }),
                Expires = DateTime.Now.AddYears(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);

            var usr = new viUser();
            usr.Token = tokenHandler.WriteToken(token);
            usr.Login = res.Login;
            usr.FIO = res.ToString();
            usr.Id = res.Id;
            usr.Status = res.Status;

            return usr;
        }
    }
}
