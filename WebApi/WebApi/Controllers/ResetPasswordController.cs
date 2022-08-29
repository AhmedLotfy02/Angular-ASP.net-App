using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;
using System.Data;
using WebApi.Models;
using Microsoft.Extensions.Logging;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ResetPasswordController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<ResetPasswordController> _logger;

        public ResetPasswordController(IConfiguration configuration, ILogger<ResetPasswordController> logger)
        {
            _configuration = configuration;
            _logger = logger;
        }
        [HttpPost]
        public JsonResult Post(ResetData user)
        {
            string sql = @"SELECT *
                FROM dbo.Users
                WHERE username ='" + user.username + "';";
            try
            {
                string sqlDataSource = _configuration.GetConnectionString("WebApiConn");
                using (var connection = new SqlConnection(sqlDataSource))
                {
                    connection.Open();
                    DataTable d = new DataTable();
                    using (var command = new SqlCommand(sql, connection))
                    {
                       
                        using (var reader = command.ExecuteReader())
                        {

                            if (!reader.HasRows)
                            {

                                _logger.LogInformation("Reseting password operation :The username entered is used");

                                connection.Close();
                                return new JsonResult("username not found");
                            }
                            else
                            {
                                DataTable table = new DataTable();
                                table.Load(reader);
                                string hashedPassFromDB = table.Rows[0][6].ToString();
                                string comparedHashPassword = Hashing.ToSHA512(user.oldPassword);
                                if (hashedPassFromDB.Equals(comparedHashPassword))
                                {
                                    string NewHashedPassword = Hashing.ToSHA512(user.newPassword);
                                    string sql2  = @"
                                    update dbo.Users set 
                                    password = '" + NewHashedPassword + @"'
                                    where username = '" + user.username + @"'; 
                                    ";
                                    reader.Close();
                                    using (var command2 = new SqlCommand(sql2, connection))
                                    {
                                        command2.ExecuteReader();
                                    }
                                    _logger.LogInformation("Done Reseting Password of user :"+user.username);

                                    connection.Close();
                                    return new JsonResult("Done Reseting Password");
                                }
                                else
                                {
                                    _logger.LogInformation("Reseting password operation: "+user.username+" has entered a wrong password");

                                    connection.Close();
                                    return new JsonResult("Wrong Password");
                                }
                                
                            }

                        }
                    }
                }
            }
            catch (Exception e)
            {
                _logger.LogInformation("Reseting password is failed due to an exception");

                return new JsonResult("Failed Process");
            }


        }


    }

}

