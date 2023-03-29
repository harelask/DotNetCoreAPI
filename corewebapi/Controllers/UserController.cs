using corewebapi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace corewebapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly DatabaseContext _databaseContext;
        private readonly ILogger<UserController> _logger;
        public UserController(DatabaseContext databaseContext, ILogger<UserController> logger)
        {
            _databaseContext = databaseContext;
            _logger = logger;
        }


        // User Login

        [HttpGet(Name = "GetUserdAll/{username}/{pass}")]
        public IEnumerable<object> GetUserdAll(string username, string pass)
        {
            IEnumerable<object> getalluser = (from user in _databaseContext.Usermaster
                                              where (user.username == username && user.pass == pass)
                                              select new
                                              {
                                                  user.userid,
                                                  user.usercode,
                                                  user.username,
                                                  user.pass,
                                                  user.email,
                                                  user.active
                                              });
            return getalluser;
        }

        [HttpPost(Name = "InsertUser")]
        public void InsertUser(Usermaster usermaster)
        {
            _databaseContext.Usermaster.Add(usermaster);
            _logger.Log(LogLevel.Trace, (usermaster.usercode + usermaster.username + usermaster.email + usermaster.pass + usermaster.active + "Inserted Successfully"));
            _databaseContext.SaveChanges();
        }

        [HttpPut(Name = "UpdateUser")]
        public void UpdateUser(Usermaster usermaster)
        {
            _databaseContext.Usermaster.Update(usermaster);
            _databaseContext.SaveChanges();
        }

        [HttpDelete(Name = "DeleteUser/{userid}")]
        public void DeleteUser(int userid)
        {
            var userdelete = _databaseContext.Usermaster.Where(u => u.userid == userid).FirstOrDefault();
            _databaseContext.Usermaster.Remove(userdelete);
            _databaseContext.SaveChanges();
        }
    }
}
