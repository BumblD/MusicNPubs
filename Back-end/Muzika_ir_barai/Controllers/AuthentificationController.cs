using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Muzika_ir_barai.Models;

namespace Muzika_ir_barai.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthentificationController : ControllerBase
    {
        private AppDb Db { get; set; }

        public AuthentificationController(AppDb db)
        {
            Db = db;
        }

        [HttpGet]
        [Route("GetUser/{id}")]
        public IActionResult GetUser([FromRoute] int id)
        {
            UserModel user = new UserModel();

            try
            {
                Db.Connection.Open();
                var cmd = Db.Connection.CreateCommand();
                cmd.CommandText = $"SELECT TOP 1 * FROM Naudotojai WHERE id = {id}";

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        user = new UserModel()
                        {
                            Id = Convert.ToInt32(reader["id"]),
                            Username = Convert.ToString(reader["Prisijungimo_vardas"]),
                            Password = Convert.ToString(reader["Slaptazodis"]),
                            Email = Convert.ToString(reader["El_pastas"]),
                            IsAdmin = Convert.ToBoolean(reader["Administratorius"]),
                        };
                    }
                }
                return Ok(user);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }
    }
}