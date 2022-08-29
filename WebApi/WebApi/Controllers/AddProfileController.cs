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
using System.Data.SqlTypes;
using System.Text;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AddProfileController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<AddProfileController> _logger;

        public AddProfileController(IConfiguration configuration, ILogger<AddProfileController> logger)
        {
            _configuration = configuration;
            _logger = logger;
        }

       
        [HttpPost]
        public JsonResult Post(User user)
        {
            string sql = @"SELECT username
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

                                string hashedPassword = Hashing.ToSHA512(user.password);
                                //encryption
                                byte[] EncryptedBirthdate = Encryption.EncryptionM(hashedPassword, user.UserPersonalData.BirthDate, 0);
                                byte[] EncryptedOccupation = Encryption.EncryptionM(hashedPassword, user.UserPersonalData.Occupation, 0);
                                byte[] EncryptedAddress = Encryption.EncryptionM(hashedPassword, user.UserPersonalData.Address, 0);

                                StringBuilder AddressBuilder = new StringBuilder();
                                StringBuilder BirthDateBuilder = new StringBuilder();
                                StringBuilder OccupationBuilder = new StringBuilder();

                               
                                for (int i = 0; i < EncryptedAddress.Length; i++)
                                {

                                    AddressBuilder.AppendLine(EncryptedAddress[i].ToString());
                                }
                                for (int i = 0; i < EncryptedBirthdate.Length; i++)
                                {

                                    BirthDateBuilder.AppendLine(EncryptedBirthdate[i].ToString());
                                }
                                for (int i = 0; i < EncryptedOccupation.Length; i++)
                                {

                                    OccupationBuilder.AppendLine(EncryptedOccupation[i].ToString());
                                }
                               
                                string sql2 = @"insert into dbo.Users 
                        (firstName,fatherName,familyName,birthdate,occupation,address,password,username)
                            values 
                        (
                            '" + user.nameData.FirstName + @"'
                            ,'" + user.nameData.FatherName + @"'
                            ,'" + user.nameData.FamilyName + @"'
                            ,'" + BirthDateBuilder + @"'
                             ,'" + OccupationBuilder + @"'
                             ,'" + AddressBuilder + @"'
                                ,'" + hashedPassword + @"'
                                 ,'" + user.username + @"'
                            );
                            ";

                                reader.Close();
                                using (var command2 = new SqlCommand(sql2, connection))
                                {
                                    command2.ExecuteReader();
                                }
                                _logger.LogInformation("Done Adding New user profile");
                                connection.Close();
                                return new JsonResult("Done Adding new user profile");
                            }
                            else
                            {
                                _logger.LogInformation("The username entered is used");

                                connection.Close();
                                return new JsonResult("Duplicate Username");
                            }
                            
                        }
                    }
                }
            }
            catch(Exception e)
            {
                _logger.LogInformation("The Process: Add new user is failed");

                return new JsonResult("Failed Process");
            }

            
        }


    }
}
