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
    public class RetrieveUserDataController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<RetrieveUserDataController> _logger;

        public RetrieveUserDataController(IConfiguration configuration, ILogger<RetrieveUserDataController> logger)
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

                                    string sAddress = "";
                                    string sOccupation = "";
                                    string sBirthdate = "";
                                  
                                    foreach (DataRow row in table.Rows)
                                    {
                                        sAddress = row["Address"].ToString();
                                        sOccupation = row["occupation"].ToString();
                                        sBirthdate = row["birthdate"].ToString();
                                    }
                                    List<byte> eAddress = new List<byte>();
                                   
                                    int count = 0;
                                    
                                    int number = 0;
                                    for(int i = 0; i < sAddress.Length; i++)
                                    {
                                       if (!(sAddress[i]=='@'|| sAddress[i]=='\r'|| sAddress[i]=='\n'))
                                        {
                                            if (count == 0)
                                                number = (int)Char.GetNumericValue(sAddress[i]);
                                            else if (count == 1)
                                            {
                                                number *= 10;
                                                number+= (int)Char.GetNumericValue(sAddress[i]); 
                                            }
                                            else if (count == 2)
                                            {
                                                number *= 10;
                                                number += (int)Char.GetNumericValue(sAddress[i]);
                                            }

                                            count++;
                                        }
                                        else
                                        {
                                            if(sAddress[i]=='\n')
                                            {
                                                eAddress.Add((byte)number);
                                                number = 0;
                                                count = 0;
                                            }
                                              
                                        }
                                       
                                            
                                    }
                                    byte[] passedAddress = new byte[eAddress.Count];
                                    for(int i = 0; i < eAddress.Count; i++)
                                    {
                                        passedAddress[i] = eAddress.ElementAt(i);
                                    }
                                    /////////
                                    count = 0;
                                    number = 0;
                                    List<byte> eBirthDate = new List<byte>();

                                    
                                    for (int i = 0; i < sBirthdate.Length; i++)
                                    {
                                          if (!(sBirthdate[i] == '@' || sBirthdate[i] == '\r' || sBirthdate[i] == '\n'))
                                        {
                                            if (count == 0)
                                                number = (int)Char.GetNumericValue(sBirthdate[i]);
                                            else if (count == 1)
                                            {
                                                number *= 10;
                                                number += (int)Char.GetNumericValue(sBirthdate[i]);
                                            }
                                            else if (count == 2)
                                            {
                                                number *= 10;
                                                number += (int)Char.GetNumericValue(sBirthdate[i]);
                                            }

                                            count++;
                                        }
                                        else
                                        {
                                            if (sBirthdate[i] == '\n')
                                            {
                                                eBirthDate.Add((byte)number);
                                                number = 0;
                                                count = 0;
                                            }

                                        }
                                       
                                    }
                                    byte[] passedBirthDate = new byte[eBirthDate.Count];
                                    for (int i = 0; i < eBirthDate.Count; i++)
                                    {
                                        passedBirthDate[i] = eBirthDate.ElementAt(i);
                                    }
                                    ////////
                                    count = 0;
                                    number = 0;
                                    List<byte> eOccupation = new List<byte>();


                                    for (int i = 0; i < sOccupation.Length; i++)
                                    {
                                        // byte c = new byte();
                                        //ss = byte.Parse(s[i], System.Globalization.NumberStyles.AllowHexSpecifier);
                                        if (!(sOccupation[i] == '@' || sOccupation[i] == '\r' || sOccupation[i] == '\n'))
                                        {
                                            if (count == 0)
                                                number = (int)Char.GetNumericValue(sOccupation[i]);
                                            else if (count == 1)
                                            {
                                                number *= 10;
                                                number += (int)Char.GetNumericValue(sOccupation[i]);
                                            }
                                            else if (count == 2)
                                            {
                                                number *= 10;
                                                number += (int)Char.GetNumericValue(sOccupation[i]);
                                            }

                                            count++;
                                        }
                                        else
                                        {
                                            if (sOccupation[i] == '\n')
                                            {
                                                eOccupation.Add((byte)number);
                                                number = 0;
                                                count = 0;
                                            }

                                        }
                                     

                                    }
                                    byte[] passedOccupation = new byte[eOccupation.Count];
                                    for (int i = 0; i < eOccupation.Count; i++)
                                    {
                                        passedOccupation[i] = eOccupation.ElementAt(i);
                                    }



                                    string origAddress = Encryption.DecryptionM(hashedPassFromDB, passedAddress);
                                    string origBirthDate= Encryption.DecryptionM(hashedPassFromDB,passedBirthDate);
                                    string orgOccupation = Encryption.DecryptionM(hashedPassFromDB, passedOccupation);
                                    _logger.LogInformation("User: "+user.username+" has retrieved his data");
                                    reader.Close();
                                    connection.Close();
                                    UserPersonalData u = new UserPersonalData(origBirthDate, orgOccupation, origAddress);
                                    Names names = new Names(table.Rows[0][0].ToString(), table.Rows[0][1].ToString(), table.Rows[0][2].ToString());
                                    User userSent = new User(names, u, user.username, hashedPassFromDB);
                                    
                                    return new JsonResult(userSent);
                                }
                                else
                                {
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
                return new JsonResult("Failed Process");
            }


        }
    }
}
